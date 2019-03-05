Configuration MyServerConfig
{

    Node MyServer {

        WindowsFeature IIS
        {
            Ensure = “Present”
            Name = “Web-Server”
        }

        WindowsFeature ASP
        {
            Ensure = “Present”
            Name = “Web-Asp-Net45”
        }

        Package 7Zip
        {
            Ensure    = "Present"
            Name      = "7-Zip 9.20 (x64 edition)"
            Path      = "E:\Installers\Desktop Software\7-Zip\7z920-x64.msi"
            ProductId = "23170F69-40C1-2702-0920-000001000000"
        }

    }

}


$thisScript = $MyInvocation.MyCommand.Path;
$thisFolder = [System.IO.Path]::GetDirectoryName($thisScript);

$mof = MyServerConfig -OutputPath "$thisFolder";
