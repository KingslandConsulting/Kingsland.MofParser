function Invoke-NUnitConsole
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $NUnitRunnersFolder,

        [Parameter(Mandatory=$true)]
        [string] $Assembly

    )

    write-host "-------------------";
    write-host "Invoke-NUnitConsole";
    write-host "-------------------";
    write-host "runners dir = $NUnitRunnersFolder";

    $console = [System.IO.Path]::Combine($NUnitRunnersFolder, "tools\nunit-console.exe");
    write-host "console path = $console";
    if( -not [System.IO.File]::Exists($console) )
    {
        throw new-object System.IO.FileNotFoundException($solution);
    }

    write-host "assembly    = $assembly";
    if( -not [System.IO.File]::Exists($Assembly) )
    {
        throw new-object System.IO.FileNotFoundException($assembly);
    }

    $cmdArgs = @( "`"$assembly`"");

    # execute nunit console
    write-host "cmdArgs = ";
    write-host ($cmdArgs | fl * | out-string);
    $process = Start-Process -FilePath $console -ArgumentList $cmdArgs -NoNewWindow -Wait -PassThru;
    if( $process.ExitCode -ne 0 )
    {
        throw new-object System.InvalidOperationException("nunit-console failed with exit code $($process.ExitCode).");
    }

    write-host "------------";

}