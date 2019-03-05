param
(

    [Parameter(Mandatory=$false)]
    [string] $NuGetApiKey

)


$ErrorActionPreference = "Stop";
Set-StrictMode -Version "Latest";


$thisScript = $MyInvocation.MyCommand.Path;
$thisFolder = [System.IO.Path]::GetDirectoryName($thisScript);
$rootFolder = [System.IO.Path]::GetDirectoryName($thisFolder);
write-host "this script = '$thisScript'";
write-host "this folder = '$thisFolder'";
write-host "root folder = '$rootFolder'";


# import all library functions
write-host "dot-sourcing script files";
$libFolder = [System.IO.Path]::Combine($thisFolder, "lib");
$filenames = [System.IO.Directory]::GetFiles($libFolder, "*.ps1");
foreach( $filename in $filenames )
{
    write-host "    $filename";
    . $filename;
}


Set-PowerShellHostWidth -Width 500;


$msbuild      = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin\MSBuild.exe";
#$msbuild     = "$($env:windir)\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe";
$solution     = [System.IO.Path]::Combine($rootFolder, "src\Kingsland.MofParser.sln");

$gitVersion   = [System.IO.Path]::Combine($rootFolder, "packages\gitversion.commandline\4.0.0\tools\GitVersion.exe");

$nunitConsole        = [System.IO.Path]::Combine($rootFolder, "packages\nunit.consolerunner\3.9.0\tools\nunit3-console.exe");
$nunitTestAssemblies = @(
    [System.IO.Path]::Combine($rootFolder, "src\Kingsland.MofParser.UnitTests\bin\Debug\Kingsland.MofParser.UnitTests.dll")
);

$nuget  = [System.IO.Path]::Combine($rootFolder, "packages\nuget.commandline\4.9.3\tools\NuGet.exe");
$nuspec = [System.IO.Path]::Combine($rootFolder, "src\Kingsland.MofParser.nuspec");
$pkgdir = [System.IO.Path]::Combine($rootFolder, "packages");


if( Test-IsTeamCityBuild )
{

    # read the build properties
    $properties = Read-TeamCityBuildProperties;
    $NuGetApiKey = $properties.NuGetApiKey;

    # copy teamcity addins for nunit into build folder
    Install-TeamCityNUnitAddIn -TeamCityNUnitAddin $properties["system.teamcity.dotnet.nunitaddin"] `
                               -NUnitRunnersFolder $nunitRunners;

}


# build the solution
$env:NUGET_PACKAGES         = $pkgdir;
$env:NUGET_HTTP_CACHE_PATH  = $pkgdir;
$msbuildParameters = @{
    "MsBuildExe"   = $msbuild
    "Solution"     = $solution
    "Targets"      = @( "Clean", "Restore", "Build" )
    "Properties"   = @{}
    "Verbosity"    =  "minimal"
    #"Verbosity"    =  "detailed"
};
Invoke-MsBuild @msbuildParameters;


# execute unit tests
foreach( $assembly in $nunitTestAssemblies )
{
    Invoke-NUnitConsole -NUnitConsole $nunitConsole -Assembly $assembly;
}


# determine build number for the nuget package
$versionInfo = Invoke-GitVersion -GitVersion $gitVersion;
#$buildNumber = $versionInfo.SemVer;
$buildNumber = $versionInfo.LegacySemVer;
write-host "version info = ";
write-host ($versionInfo | fl * | out-string);
write-host "build number = '$buildNumber'";


# configure nuspec package
$xml = new-object System.Xml.XmlDocument;
$xml.Load($nuspec);
$xml.package.metadata.version = $buildNumber;
$xml.Save($nuspec);


# pack nuget package
#$nupkg  = [System.IO.Path]::Combine($rootFolder, "Kingsland.MofParser." + $BuildNumber + ".nupkg");
Invoke-NuGetPack -NuGet $nuget -NuSpec $nuspec -OutputDirectory $rootFolder;


# push nuget package
if( $PSBoundParameters.ContainsKey("NuGetApiKey") )
{
    Invoke-NuGetPush -NuGet $nuget -PackagePath $nupkg -Source "https://nuget.org" -ApiKey $NuGetApiKey;
}
