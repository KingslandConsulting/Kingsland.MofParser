param
(

    [Parameter(Mandatory=$false)]
    [string] $NuGetApiKey,

    [Parameter(Mandatory=$false)]
    [string] $BuildNumber

)


$ErrorActionPreference = "Stop";
Set-StrictMode -Version "Latest";


$thisScript = $MyInvocation.MyCommand.Path;
$thisFolder = [System.IO.Path]::GetDirectoryName($thisScript);
$rootFolder = [System.IO.Path]::GetDirectoryName($thisFolder);


# import all library functions
$libFolder = [System.IO.Path]::Combine($thisFolder, "lib");
$filenames = [System.IO.Directory]::GetFiles($libFolder, "*.ps1");
foreach( $filename in $filenames )
{
    . $filename;
}


Set-PowerShellHostWidth -Width 500;


if( Test-IsTeamCityBuild )
{
    $properties = Read-TeamCityBuildProperties;
    $NuGetApiKey = $properties.NuGetApiKey;
    $BuildNumber = $properties["build.number"];
}


# build the solution
$solution     = [System.IO.Path]::Combine($rootFolder, "src\Kingsland.MofParser.sln");
Invoke-MsBuild -solution $solution `
               -targets @( "Clean", "Build") `
               -properties @{ };


# copy teamcity addins for nunit into build folder
$nunitRunners = [System.IO.Path]::Combine($rootFolder, "Packages\NUnit.Runners.2.6.4");
if( Test-IsTeamCityBuild )
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
    Invoke-NUnitConsole -nunitRunnersFolder $nunitRunners -assembly $assembly;
}


# configure nuspec package
$nuspec = [System.IO.Path]::Combine($rootFolder, "Kingsland.MofParser.nuspec");
$xml = new-object System.Xml.XmlDocument;
$xml.Load($nuspec);
$xml.package.metadata.version = $BuildNumber;
$xml.Save($nuspec);


# pack nuget package
$nuget = [System.IO.Path]::Combine($rootFolder, "packages\NuGet.CommandLine.2.8.6\tools\NuGet.exe");
Invoke-NuGetPack -NuGet $nuget -NuSpec $nuspec;


# push nuget package
$nupkg = [System.IO.Path]::Combine($rootFolder, "Kingsland.MofParser." + $BuildNumber + ".nupkg");
Invoke-NuGetPush -NuGet $nuget -PackagePath $nupkg -Source "https://nuget.org" -ApiKey $NuGetApiKey;
