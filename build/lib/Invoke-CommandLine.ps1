function Invoke-CommandLine
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $Command,

        [Parameter(Mandatory=$true)]
        [string[]] $Arguments

    )

    write-host "******************";
    write-host "Invoke-CommandLine";
    write-host "******************";

    write-host "command   = '$Command'";
    write-host "arguments = ";
    write-host ($Arguments | fl * | out-string);

    $process = Start-Process -FilePath $Command -ArgumentList $Arguments -Wait -NoNewWindow -PassThru;

    if( $process.ExitCode -ne 0 )
    {
        throw new-object System.InvalidOperationException("Command failed with exit code $($process.ExitCode)");
    }

    return $process.ExitCode;

}

