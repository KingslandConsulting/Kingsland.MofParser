param
(

    [Parameter(Mandatory=$false)]
    [string] $BuildNumber = "1.0.0",

    [Parameter(Mandatory=$false)]
    [string] $NuGetApiKey

)


$ErrorActionPreference = "Stop";
Set-StrictMode -Version "Latest";


$thisScript = $MyInvocation.MyCommand.Path;
$thisFolder = [System.IO.Path]::GetDirectoryName($thisScript);
$rootFolder = [System.IO.Path]::GetDirectoryName($thisFolder);


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


#$msbuild  = "$($env:windir)\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe";
$msbuild  = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin\MSBuild.exe";
$solution = [System.IO.Path]::Combine($rootFolder, "src\Kingsland.MofParser.sln");


$nunitConsole = [System.IO.Path]::Combine($rootFolder, "packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe");
$nunitTestAssemblies = @(
    [System.IO.Path]::Combine($rootFolder, "src\Kingsland.MofParser.UnitTests\bin\Debug\Kingsland.MofParser.UnitTests.dll")
);


$nuget  = [System.IO.Path]::Combine($rootFolder, "packages\NuGet.CommandLine.4.9.2\tools\NuGet.exe");
$nuspec = [System.IO.Path]::Combine($rootFolder, "Kingsland.MofParser.nuspec");
$nupkg  = [System.IO.Path]::Combine($rootFolder, "Kingsland.MofParser." + $BuildNumber + ".nupkg");


if( Test-IsTeamCityBuild )
{
    $properties = Read-TeamCityBuildProperties;
    $NuGetApiKey = $properties.NuGetApiKey;
    $BuildNumber = $properties["build.number"];
}


# build the solution
$msbuildParameters = @{
    "MsBuildExe"   = $msbuild;
    "Solution"     = $solution
    "Targets"      = @( "Clean", "Build" )
    "Properties"   = @{ }
    #"Verbosity"    =  "detailed"
};
Invoke-MsBuild @msbuildParameters;

# copy teamcity addins for nunit into build folder
if( Test-IsTeamCityBuild )
{
    Install-TeamCityNUnitAddIn -TeamCityNUnitAddin $properties["system.teamcity.dotnet.nunitaddin"] `
                               -NUnitRunnersFolder $nunitRunners;
}


# execute unit tests
foreach( $assembly in $nunitTestAssemblies )
{
    Invoke-NUnitConsole -NUnitConsole $nunitConsole -Assembly $assembly;
}


# configure nuspec package
$xml = new-object System.Xml.XmlDocument;
$xml.Load($nuspec);
$xml.package.metadata.version = $BuildNumber;
$xml.Save($nuspec);


# pack nuget package
Invoke-NuGetPack -NuGet $nuget -NuSpec $nuspec -OutputDirectory $rootFolder;


# push nuget package
if( $PSBoundParameters.ContainsKey("NuGetApiKey") )
{
    Invoke-NuGetPush -NuGet $nuget -PackagePath $nupkg -Source "https://nuget.org" -ApiKey $NuGetApiKey;
}
