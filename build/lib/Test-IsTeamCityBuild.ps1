function Test-IsTeamCityBuild()
{

    $filename = $env:TEAMCITY_BUILD_PROPERTIES_FILE;

    return (-not [string]::IsNullOrEmpty($filename));

}