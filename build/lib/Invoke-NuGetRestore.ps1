function Invoke-NuGetRestore
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $NuGet,

        [Parameter(Mandatory=$true)]
        [string] $Solution,

        [Parameter(Mandatory=$true)]
        [string] $PackagesDirectory

    )

    write-host "*******************";
    write-host "Invoke-NuGetRestore";
    write-host "*******************";
    write-host "nuget              = '$NuGet'";
    write-host "solution           = '$Solution'";
    write-host "packages directory = '$PackagesDirectory'";

    # check nuget.exe exists
    if( -not [System.IO.File]::Exists($NuGet) )
    {
        throw new-object System.IO.FileNotFoundException("NuGet.exe not found.");
    }

    $cmdLine = $NuGet;

    $cmdArgs = @( "restore" );

    if( $PSBoundParameters.ContainsKey("Solution") )
    {
        $cmdArgs += @( "`"$Solution`"" );
    }

    if( $PSBoundParameters.ContainsKey("PackagesDirectory") )
    {
        $cmdArgs += @( "-PackagesDirectory", "`"$PackagesDirectory`"" );
    }

    $cmdArgs += @( "-Verbosity", "detailed" );

    # execute nuget restore
    $null = Invoke-CommandLine -Command $cmdLine -Arguments $cmdArgs;

}