$ErrorActionPreference = "Stop";
Set-StrictMode -Version "Latest"


# list all classes
$wmiClasses = Get-WmiObject -List -NameSpace "root" -Recurse -ErrorAction "Stop";
foreach( $wmiClass in $wmiClasses )
{
    $mof = $wmiClass.GetText("Mof");
    if( $mof.Contains("{}") )
    {
        write-host $mof;
    }
}


# list win32 provider class
$wmiClass = Get-WmiObject -query "SELECT * FROM meta_class WHERE __class = '__Win32Provider'";
write-host $wmiClass.GetText("Mof");
