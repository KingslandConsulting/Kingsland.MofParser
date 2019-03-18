function Set-DotNetFusionBinding
{

    param
    (

        [Parameter(Mandatory=$false)]
        [bool] $EnableLog,

        [Parameter(Mandatory=$false)]
        [bool] $ForceLog,

        [Parameter(Mandatory=$false)]
        [bool] $LogFailures,

        [Parameter(Mandatory=$false)]
        [bool] $LogResourceBinds,

        [Parameter(Mandatory=$false)]
        [string] $LogPath

    )

    # see https://stackoverflow.com/a/1527249/3156906

    $boolToDWORD = @{
        $true  = 1
        $false = 0
    };

    if( $PSBoundParameters.ContainsKey("EnableLog") )
    {
        Set-ItemProperty -Path "HKLM:\Software\Microsoft\Fusion" `
                         -Name "EnableLog" `
                         -Value $boolToDWORD[$EnableLog] `
                         -Type "DWord";
    }

    if( $PSBoundParameters.ContainsKey("ForceLog") )
    {
        Set-ItemProperty -Path "HKLM:\Software\Microsoft\Fusion" `
                         -Name "ForceLog" `
                         -Value $boolToDWORD[$EnableLog] `
                         -Type "DWord";
    }

    if( $PSBoundParameters.ContainsKey("LogFailures") )
    {
        Set-ItemProperty -Path "HKLM:\Software\Microsoft\Fusion" `
                         -Name "LogFailures" `
                         -Value $boolToDWORD[$EnableLog] `
                         -Type "DWord";
    }

    if( $PSBoundParameters.ContainsKey("LogResourceBinds") )
    {
        Set-ItemProperty -Path "HKLM:\Software\Microsoft\Fusion" `
                         -Name "LogResourceBinds" `
                         -Value $boolToDWORD[$EnableLog] `
                         -Type "DWord";
    }

    if( $PSBoundParameters.ContainsKey("LogPath") )
    {
        if( -not $LogPath.EndsWith("\") )
        {
            $LogPath += "\";
        }
        if( -not [System.IO.Directory]::Exists($LogPath) )
        {
            $null = [System.IO.Directory]::CreateDirectory($LogPath);
        }
        Set-ItemProperty -Path "HKLM:\Software\Microsoft\Fusion" `
                         -Name "LogPath" `
                         -Value $LogPath `
                         -Type "String";
    }

}