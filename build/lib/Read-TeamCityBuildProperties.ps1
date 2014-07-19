function Read-TeamCityBuildProperties()
{

    # private function to read a teamcity properties xml file
    function Read-TeamCityBuildPropertiesXmlFile()
    {
        param
        (
            [Parameter(Mandatory=$true)]
            [string] $Filename
        )
        write-host "reading properties file";
        write-host "filename = $filename";
        # check the file exists
        if( -not [System.IO.File]::Exists($filename) )
        {
            throw new-object System.IO.FileNotFoundException($filename);
        }
        # load the file contents into an xml document
        write-host ("file content = " + [System.IO.File]::ReadAllText($filename));
        $xml = new-object System.Xml.XmlDocument;
        $xml.Load($filename);
        # convert the entry nodes into hashtable entries
        $properties = @{};
        $entryNodes = $xml.SelectNodes("properties/entry");
        foreach( $entryNode in $entryNodes )
        {
            $properties[$entryNode.key] = $entryNode.InnerText;
        }
        # return the results
        return $properties;
    };

    $properties = @{};

    write-host "----------------------------";
    write-host "Read-TeamCityBuildProperties";
    write-host "----------------------------";

    # check if the environment variable is defined
    write-host "checking environment variable";
    $filename = $env:TEAMCITY_BUILD_PROPERTIES_FILE;
    write-host "filename = $filename";
    if( [string]::IsNullOrEmpty($filename) )
    {
        write-host "environment variable not defined";
        write-host "----------------------------";
        return $null;
    }

    write-host "reading system properties file";
    $filename = $filename + ".xml";
    $entries = Read-TeamCityBuildPropertiesXmlFile -Filename $filename;
    write-host "entries = ";
    write-host ($entries | ft -AutoSize | out-string);
    foreach( $key in $entries.Keys )
    {
        $properties.Add("system." + $key, $entries[$key]);
    }

    write-host "reading configuration properties file";
    $filename = $properties["system.teamcity.configuration.properties.file"] + ".xml";
    $entries = Read-TeamCityBuildPropertiesXmlFile -Filename $filename;
    write-host "entries = ";
    write-host ($entries | ft -AutoSize | out-string);
    foreach( $key in $entries.Keys )
    {
        if( -not $properties.ContainsKey($key) )
        {
            $properties.Add($key, $entries[$key]);
        }
    }

    write-host "reading runner properties file";
    $filename = $properties["system.teamcity.runner.properties.file"] + ".xml";
    $entries = Read-TeamCityBuildPropertiesXmlFile -Filename $filename;
    write-host "entries = ";
    write-host ($entries | ft -AutoSize | out-string);
    foreach( $key in $entries.Keys )
    {
        if( -not $properties.ContainsKey($key) )
        {
            $properties.Add($key, $entries[$key]);
        }
    }

    write-host "----------------------------";
    return $properties;

}