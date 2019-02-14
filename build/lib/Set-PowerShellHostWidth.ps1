function Set-PowerShellHostWidth
{

    param
    (

        [Parameter(Mandatory=$true)]
        [uint32] $Width

    )

    # set powershell's internal output buffer width to prevent excessive word wrapping in TeamCity output log
    # see http://stackoverflow.com/questions/978777/powershell-output-column-width
    if( ($host -ne $null) -and ($host.UI -ne $null) -and ($host.UI.RawUI -ne $null) )
    {
         $rawUI = $host.UI.RawUI;
         $newSize = new-object System.Management.Automation.Host.Size($width, $rawUI.BufferSize.Height);
         $rawUI.BufferSize = $newSize;
     }

}