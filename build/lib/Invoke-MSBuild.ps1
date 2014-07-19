function Invoke-MsBuild
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $solution,

        [Parameter(Mandatory=$false)]
        [string[]] $targets = @(),

        [Parameter(Mandatory=$false)]
        [hashtable] $properties = @(),

        [Parameter(Mandatory=$false)]
        [ValidateSet("quiet", "minimal", "normal", "detailed", "diagnostic")]
        [string] $verbosity = "minimal"

    )

    write-host "--------------";
    write-host "Invoke-MsBuild";
    write-host "--------------";

    $msbuild = "$($env:windir)\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe";
    write-host "msbuild path = $msbuild";
    if( -not [System.IO.File]::Exists($msbuild) )
    {
        throw new-object System.IO.FileNotFoundException($solution);
    }

    $version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($msbuild);
    write-host "msbuild version info = ";
    write-host ($version | fl * | out-string);

    $arguments = @();

    # solution
    write-host "solution = $solution";
    if( -not [System.IO.File]::Exists($solution) )
    {
        throw new-object System.IO.FileNotFoundException($solution);
    }
    $arguments += $solution;    

    # targets
    write-host "targets  = ";
    write-host ($targets | ft | out-string);
    if( $targets -ne $null )
    {
        $arguments += "/target:" + [string]::Join(";", $targets);
    }

    # properties
    $propertiesString = [string]::Empty;
    if( $properties -ne $null )
    {
        $propertiesStrings = @();
        foreach( $key in $properties.Keys )
        {
            $arguments += [string]::Format("/property:{0}={1}", $key, $properties[$key]);
        }
    }

    # verbosity
    $arguments += "/verbosity:$verbosity";

    # maxcpucount
    $arguments += "/maxcpucount";

    write-host "arguments = ";
    write-host ($arguments | fl | out-string);

    $process = Start-Process -FilePath $msbuild -ArgumentList $arguments -NoNewWindow -Wait -PassThru;

    if( $process.ExitCode -ne 0 )
    {
        throw new-object System.InvalidOperationException("MSBuild failed with exit code $($process.ExitCode).");
    }

    write-host "--------------";

}