function Invoke-NUnit
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $nunitRunnersFolder,

        [Parameter(Mandatory=$true)]
        [string] $assembly

    )

    write-host "------------";
    write-host "Invoke-NUnit";
    write-host "------------";
    write-host "runners dir = $nunitRunnersFolder";

    $console = [System.IO.Path]::Combine($nunitRunnersFolder, "tools\nunit-console.exe");
    write-host "console path = $console";
    if( -not [System.IO.File]::Exists($console) )
    {
        throw new-object System.IO.FileNotFoundException($solution);
    }

    write-host "assembly     = $assembly";
    if( -not [System.IO.File]::Exists($assembly) )
    {
        throw new-object System.IO.FileNotFoundException($assembly);
    }

    $process = Start-Process -FilePath $console -ArgumentList @( $assembly ) -NoNewWindow -Wait -PassThru;

    if( $process.ExitCode -ne 0 )
    {
        throw new-object System.InvalidOperationException("nunit-console failed with exit code $($process.ExitCode).");
    }

    write-host "------------";

}