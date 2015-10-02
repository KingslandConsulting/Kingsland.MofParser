function Invoke-NuGetPush
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $NuGet,

        [Parameter(Mandatory=$true)]
        [string] $PackagePath,

        [Parameter(Mandatory=$true)]
        [string] $Source,

        [Parameter(Mandatory=$true)]
        [string] $ApiKey

    )

    write-host "****************";
    write-host "Invoke-NuGetPush";
    write-host "****************";
    write-host "nuget        = $NuGet";
    write-host "package path = $PackagePath";
    write-host "source       = $Source";
    write-host "api key      = ******";

    # check nuget.exe exists
    if( -not [System.IO.File]::Exists($NuGet) )
    {
        throw new-object System.IO.FileNotFoundException("NuGet.exe not found.");
    }


    $cmdLine = $NuGet;

    $cmdArgs = @(
        "push",
        "`"$PackagePath`"",
        "-Source", $source,
        "-ApiKey", $ApiKey
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