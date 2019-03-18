function Invoke-NuGetInstall
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $NuGet

    )

    write-host "****************";
    write-host "Invoke-NuGetPack";
    write-host "****************";
    write-host "nuget  = $NuGet";

    # check nuget.exe exists
    if( -not [System.IO.File]::Exists($NuGet) )
    {
        throw new-object System.IO.FileNotFoundException("NuGet.exe not found.");
    }

    $cmdLine = $NuGet;

throw "aaa"

    $cmdArgs = @( "pack", "`"$NuSpec`"" );

    if( -not [string]::IsNullOrEmpty($OutputDirectory) )
    {
        $cmdArgs += @( "-OutputDirectory", "`"$OutputDirectory`"" );
    }

    $cmdArgs += @( "-Verbosity", "detailed" );

    # execute nuget install
    $null = Invoke-CommandLine -Command $cmdLine -Arguments $cmdArgs;

}