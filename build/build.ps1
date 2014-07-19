param
(
    [string] $targets = "Build"
)


$ErrorActionPreference = "Stop";
Set-StrictMode -Version "Latest";


$thisScript = $MyInvocation.MyCommand.Path;
$thisFolder = [System.IO.Path]::GetDirectoryName($thisScript);
$rootFolder = [System.IO.Path]::GetFullPath([System.IO.Path]::Combine($thisFolder, "..")); 


# import all library functions
$libFolder  = [System.IO.Path]::Combine($thisFolder, "lib");
$filenames = [System.IO.Directory]::GetFiles($libFolder, "*.ps1");
foreach( $filename in $filenames )
{
    . $filename;
}


Set-PowerShellHostWidth 500;


# build the solution
$solution     = [System.IO.Path]::Combine($rootFolder, "src\Kingsland.MofParser.sln");
Invoke-MsBuild -solution $solution `
               -targets @( "Clean", "Build") `
               -properties @{ };


# copy teamcity addins for nunit into build folder
$nunitRunners = [System.IO.Path]::Combine($rootFolder, "Packages\NUnit.Runners.2.6.3");
$properties = Read-TeamCityBuildProperties;
if( $properties -ne $null )
{
    Install-TeamCityNUnitAddIn -teamcityNUnitAddin $properties["system.teamcity.dotnet.nunitaddin"] `
                               -nunitRunnersFolder $nunitRunners;
}

# execute unit tests
$testAssemblies = @(
                      [System.IO.Path]::Combine($rootFolder, "src\Kingsland.MofParser.UnitTests\bin\Debug\Kingsland.MofParser.UnitTests.dll")
                  );
foreach( $assembly in $testAssemblies)
{
    Invoke-NUnit -nunitRunnersFolder $nunitRunners -assembly $assembly;
}
