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

    # execute nuget push
    $null = Invoke-CommandLine -Command $cmdLine -Arguments $cmdArgs;

}