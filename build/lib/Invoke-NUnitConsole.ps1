function Invoke-NUnitConsole
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $NUnitConsole,

        [Parameter(Mandatory=$true)]
        [string] $Assembly

    )

    write-host "-------------------";
    write-host "Invoke-NUnitConsole";
    write-host "-------------------";
    write-host "console path = $NUnitConsole";

    if( -not [System.IO.File]::Exists($NUnitConsole) )
    {
        throw new-object System.IO.FileNotFoundException($NUnitConsole);
    }

    write-host "assembly    = $assembly";
    if( -not [System.IO.File]::Exists($Assembly) )
    {
        throw new-object System.IO.FileNotFoundException($assembly);
    }

    $cmdLine = $NUnitConsole;

    $cmdArgs = @( "`"$assembly`"");

    # execute nunit console
    $null = Invoke-CommandLine -Command $cmdLine -Arguments $cmdArgs;

    write-host "------------";

}