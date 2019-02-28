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

    write-host "cmdLine = $cmdLine";
    write-host "cmdArgs = ";
    write-host ($cmdArgs | fl * | out-string);
    $process = Start-Process -FilePath $cmdLine -ArgumentList $cmdArgs -Wait -NoNewWindow -PassThru;
    if( $process.ExitCode -ne 0 )
    {
        throw new-object System.InvalidOperationException("NuGet.exe failed with exit code $($process.ExitCode)");
    }

}