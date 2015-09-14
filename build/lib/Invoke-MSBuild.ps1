function Invoke-MsBuild
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $Solution,

        [Parameter(Mandatory=$false)]
        [string[]] $Targets,

        [Parameter(Mandatory=$false)]
        [hashtable] $Properties,

        [Parameter(Mandatory=$false)]
        [ValidateSet("quiet", "minimal", "normal", "detailed", "diagnostic")]
        [string] $Verbosity = "minimal"

    )

    write-host "**************";
    write-host "Invoke-MsBuild";
    write-host "**************";
    write-host "solution = $Solution";

    $msbuild = "$($env:windir)\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe";
    $version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($msbuild);

    write-host "msbuild path = $msbuild";
    write-host "msbuild version info = ";
    write-host ($version | fl * | out-string);

    $cmdArgs = @();

    # MSBuild.exe [options] [project file]
    $cmdArgs += "`"$solution`""

    # /target:<targets>
    write-host "targets = ";
    write-host ($Targets | ft -AutoSize | out-string);
    if( $targets -ne $null )
    {
        foreach( $target in $targets )
        {
            $cmdArgs += "/target:`"$target`"";
        }
    }

    # /property:<n>=<v>
    write-host "properties  = ";
    write-host ($Properties | ft -AutoSize | out-string);
    if( $properties -ne $null )
    {
        foreach( $key in $properties.Keys )
        {
            $cmdArgs += "/property:$key=$($properties[$key])";
        }
    }

    # /maxcpucount[:n]
    $cmdArgs += "/maxcpucount";

    # /toolsversion:<version>

    # /verbosity:<level>
    write-host "verbosity = $Verbosity";
    $cmdArgs += "/verbosity:$Verbosity";

    # /consoleloggerparameters:<parameters>
    # /noconsolelogger
    # /fileLogger[n]
    # /fileloggerparameters[n]:<parameters>
    # /distributedlogger:<central logger>*<forwarding logger>
    # /distributedFileLogger
    # /logger:<logger>
    # /validate:<schema>
    # /ignoreprojectextensions:<extensions>

    # /nodeReuse:<parameters>
    $cmdArgs += "/nodeReuse:false";

    # /preprocess[:file]
    # /detailedsummary
    # /noautoresponse

    # /nologo
    $cmdArgs += "/nologo";

    # /version
    # /help

    # execute msbuild
    write-host "cmdArgs = ";
    write-host ($cmdArgs | fl * | out-string);
    $process = Start-Process -FilePath $msbuild -ArgumentList $cmdArgs -NoNewWindow -Wait -PassThru;
    if( $process.ExitCode -ne 0 )
    {
        throw new-object System.InvalidOperationException("MsBuild failed with exit code $($process.ExitCode).");
    }

}