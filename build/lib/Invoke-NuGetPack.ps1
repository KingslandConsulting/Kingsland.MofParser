function Invoke-NuGetPack
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $NuGet,

        [Parameter(Mandatory=$true)]
        [string] $NuSpec

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

    $cmdArgs = @(
        "pack",
        "`"$NuSpec`"",
        "-Verbosity", "detailed"
    );

    write-host "cmdLine = $cmdLine";
    write-host "cmdArgs = ";
    write-host ($cmdArgs | fl * | out-string);
    $process = Start-Process -FilePath $cmdLine -ArgumentList $cmdArgs -Wait -NoNewWindow -PassThru;
    if( $process.ExitCode -ne 0 )
    {
        throw new-object System.InvalidOperationException("NuGet.exe failed with exit code $($process.ExitCode)");
    }

}