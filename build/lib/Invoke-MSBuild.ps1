function Invoke-MsBuild
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $MsBuildExe,

        [Parameter(Mandatory=$true)]
        [string] $Solution,

        [Parameter(Mandatory=$false)]
        [string[]] $Targets,

        [Parameter(Mandatory=$false)]
        [hashtable] $Properties,

        [Parameter(Mandatory=$false)]
        [string] $ToolsVersion,

        [Parameter(Mandatory=$false)]
        [ValidateSet("quiet", "minimal", "normal", "detailed", "diagnostic")]
        [string] $Verbosity = "minimal"

    )

    write-host "**************";
    write-host "Invoke-MsBuild";
    write-host "**************";
    write-host "solution = $Solution";

    $version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($msbuildExe);

    write-host "msbuild path = $MsBuildExe";
    write-host "msbuild version info = ";
    write-host ($version | fl * | out-string);

    $cmdArgs = @();

    # MSBuild.exe [options] [project file]
    $cmdArgs += "`"$solution`""

    # /target:<targets>
    if( $PSBoundParameters.ContainsKey("Targets") )
    {
        write-host "targets = ";
        write-host ($Targets | ft -AutoSize | out-string);
        foreach( $target in $Targets )
        {
            $cmdArgs += "/target:`"$target`"";
        }
    }

    # /property:<n>=<v>
    if( $PSBoundParameters.ContainsKey("Properties") )
    {
        write-host "properties  = ";
        write-host ($Properties | ft -AutoSize | out-string);
        foreach( $key in $Properties.Keys )
        {
            $cmdArgs += "/property:$key=$($Properties[$key])";
        }
    }

    # /toolsversion:<version>
    if( $PSBoundParameters.ContainsKey("ToolsVersion") )
    {
        write-host "ToolsVersion = '$ToolsVersion'";
        $cmdArgs += "/toolsversion:$ToolsVersion";
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
    # /validate
    # /validate:<schema>
    # /ignoreprojectextensions:<extensions>

    # /nodeReuse:<parameters>
    $cmdArgs += "/nodeReuse:false";

    # /preprocess[:file]
    # /detailedsummary
    # @<file>
    # /noautoresponse

    # /nologo
    $cmdArgs += "/nologo";

    # /version
    # /help

    # execute msbuild
    write-host "cmdLine = $MsBuildExe";
    write-host "cmdArgs = ";
    write-host ($cmdArgs | fl * | out-string);
    $process = Start-Process -FilePath $MsBuildExe -ArgumentList $cmdArgs -NoNewWindow -Wait -PassThru;
    if( $process.ExitCode -ne 0 )
    {
        throw new-object System.InvalidOperationException("MsBuild failed with exit code $($process.ExitCode).");
    }

}