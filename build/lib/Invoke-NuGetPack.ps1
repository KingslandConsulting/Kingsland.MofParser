function Invoke-NuGetPack
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $NuGet,

        [Parameter(Mandatory=$true)]
        [string] $NuSpec,

        [Parameter(Mandatory=$false)]
        [string] $OutputDirectory

    )

    write-host "****************";
    write-host "Invoke-NuGetPack";
    write-host "****************";
    write-host "nuget  = $NuGet";
    write-host "nuspec = $NuSpec";

    # check nuget.exe exists
    if( -not [System.IO.File]::Exists($NuGet) )
    {
        throw new-object System.IO.FileNotFoundException("NuGet.exe not found.");
    }

    # check the *.nuspec exists
    if( -not [System.IO.File]::Exists($NuSpec) )
    {
        throw new-object System.IO.FileNotFoundException("NuSpec file not found.");
    }

    $cmdLine = $NuGet;

    $cmdArgs = @( "pack", "`"$NuSpec`"" );

    if( -not [string]::IsNullOrEmpty($OutputDirectory) )
    {
        $cmdArgs += @( "-OutputDirectory", "`"$OutputDirectory`"" );
    }

    $cmdArgs += @( "-Verbosity", "detailed" );

    # execute nuget pack
    $null = Invoke-CommandLine -Command $cmdLine -Arguments $cmdArgs;

}