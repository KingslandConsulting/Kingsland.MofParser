function Install-TeamCityNUnitAddIn()
{

    param
    (

        [Parameter(Mandatory=$true)]
        [string] $teamcityNUnitAddin,

        [Parameter(Mandatory=$true)]
        [string] $nunitRunnersFolder

    )

    write-host "--------------------------";
    write-host "Install-TeamCityNUnitAddIn";
    write-host "--------------------------";
    write-host "nunit addin = $teamcityNUnitAddin";
    write-host "runner dir  = $nunitRunnersFolder";

    # get the version number of the nunit runner
    $console = [System.IO.Path]::Combine($nunitRunnersFolder, "tools\nunit-console.exe");
    $version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($console);
    $versionString = [string]::Format("{0}.{1}.{2}", `
                                      $version.ProductMajorPart, `
                                      $version.ProductMinorPart, `
                                      $version.ProductBuildPart);
    write-host "nunit version = $versionString";

    write-host "checking for teamcity addin";
    $sourceFolder = [System.IO.Path]::GetDirectoryName($teamcityNUnitAddin);
    $sourceFilename = [System.IO.Path]::GetFileName($teamcityNUnitAddin) + "-" + $versionString + ".dll";
    write-host "source folder = $sourceFolder";
    write-host "source file   = $sourceFilename";
    if( -not [System.IO.File]::Exists([System.IO.Path]::Combine($sourceFolder, $sourceFilename)) )
    {
        throw new-object System.IO.FileNotFoundException("TeamCity addin folder doesn't contain an addin for NUnit version $versionString.", $sourceFilename);
    }

    write-host "creating addin directory";
    $targetFolder = [System.IO.Path]::Combine($nunitRunnersFolder, "tools\addins");
    write-host "target folder = $targetFolder";
    if( -not [System.IO.Directory]::Exists($targetFolder) )
    {
        [void] [System.IO.Directory]::CreateDirectory($targetFolder);
    }

    write-host "copying files = ";
    $searchPattern = [System.IO.Path]::GetFileName($teamcityNUnitAddin) + "-" + $versionString + ".*";
    foreach( $sourceFilename in [System.IO.Directory]::GetFiles($sourceFolder, $searchPattern) )
    {
        $targetFilename = [System.IO.Path]::GetFilename($sourceFilename);
        $targetFilename = [System.IO.Path]::Combine($targetFolder, $targetFilename);
        write-host "source = $sourceFilename";
        write-host "target = $targetFilename";
        [System.IO.File]::Copy($sourceFilename, $targetFilename, $true);
    }

    write-host "--------------------------";

}