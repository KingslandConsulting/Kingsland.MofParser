Kingsland.MofParser
===================

Overview
--------

Kingsland.MofParser is a C# library for parsing the contents of Managed Object Format (MOF) files.

The library was initially created with the goal of producing documentation from Managed Object Format files produced by PowerShell Desired State Configuration scripts, but now supports nearly all of the [MOF 3.0.1 specification ](https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf) including the following:

+ Compiler directives (```#pragma include```)
+ Instance declarations (```instance of xyz { ... }```)
+ Structure declarations (```value of xyz { ... }```)
+ Class declarations (```class xyz { ... }```)
+ Association declarations (```association xyz { ... }```)
+ Enumeration declarations (```enumeration xyz { ... }```)

For the full list of implemented language elements, see [Supported Syntax](supported_syntax.md).


Quick Start
-----------

To process a MOF file produced by PowerShell DSC, simply pass the filename to the PowerShellDscHelper.ParseMofFile method.

This will read the contents of the file and extract a list of "instance" declarations that are defined in the file. You can use this list to generate html documentation, or perform any other downstream processing.

See the Kingsland.MofParser.Sample project for the full source.

Sample Code
-----------

```c#
const string filename = "..\\..\\dsc\\MyServer.mof";

// parse the mof file
var instances = PowerShellDscHelper.ParseMofFileInstances(filename);

// display the instances
foreach (var instance in instances)
{
    Console.WriteLine("--------------------------");
    if (string.IsNullOrEmpty(instance.Alias))
    {
        Console.WriteLine(string.Format("instance of {0}", instance.ClassName));
    }
    else
    {
        Console.WriteLine(string.Format("instance of {0} as ${1}", instance.ClassName, instance.Alias));
    }
    foreach(var property in instance.Properties)
    {
        Console.WriteLine("    {0} = {1}", property.Key.PadRight(14), property.Value.ToString());
    }
    Console.WriteLine("--------------------------");
}
```

Sample MOF
----------

```
/*
@TargetNode='MyServer'
@GeneratedBy=mike.clayton
@GenerationDate=07/19/2014 10:37:04
@GenerationHost=MyDesktop
*/

instance of MSFT_RoleResource as $MSFT_RoleResource1ref
{
ResourceID = "[WindowsFeature]IIS";
 Ensure = "Present";
 SourceInfo = "E:\\MofParser\\src\\Kingsland.MofParser.Sample\\dsc\\MyServerConfig.ps1::6::9::WindowsFeature";
 Name = "Web-Server";
 ModuleName = "PSDesiredStateConfiguration";
 ModuleVersion = "1.0";

};

instance of MSFT_RoleResource as $MSFT_RoleResource2ref
{
ResourceID = "[WindowsFeature]ASP";
 Ensure = "Present";
 SourceInfo = "E:\\MofParser\\src\\Kingsland.MofParser.Sample\\dsc\\MyServerConfig.ps1::12::9::WindowsFeature";
 Name = "Web-Asp-Net45";
 ModuleName = "PSDesiredStateConfiguration";
 ModuleVersion = "1.0";

};

instance of MSFT_PackageResource as $MSFT_PackageResource1ref
{
ResourceID = "[Package]7Zip";
 Path = "E:\\Installers\\Desktop Software\\7-Zip\\7z920-x64.msi";
 Ensure = "Present";
 ProductId = "23170F69-40C1-2702-0920-000001000000";
 SourceInfo = "E:\\MofParser\\src\\Kingsland.MofParser.Sample\\dsc\\MyServerConfig.ps1::18::9::Package";
 Name = "7-Zip 9.20 (x64 edition)";
 ModuleName = "PSDesiredStateConfiguration";
 ModuleVersion = "1.0";

};

instance of OMI_ConfigurationDocument
{
 Version="1.0.0";
 Author="mike.clayton";
 GenerationDate="07/19/2014 10:37:04";
 GenerationHost="MyDesktop";
};
```

Sample Output
-------------

```text
--------------------------
instance of MSFT_RoleResource as $MSFT_RoleResource1ref
    ResourceID     = [WindowsFeature]IIS
    Ensure         = Present
    SourceInfo     = E:\MofParser\src\Kingsland.MofParser.Sample\dsc\MyServerConfig.ps1::6::9::WindowsFeature
    Name           = Web-Server
    ModuleName     = PSDesiredStateConfiguration
    ModuleVersion  = 1.0
--------------------------
--------------------------
instance of MSFT_RoleResource as $MSFT_RoleResource2ref
    ResourceID     = [WindowsFeature]ASP
    Ensure         = Present
    SourceInfo     = E:\MofParser\src\Kingsland.MofParser.Sample\dsc\MyServerConfig.ps1::12::9::WindowsFeature
    Name           = Web-Asp-Net45
    ModuleName     = PSDesiredStateConfiguration
    ModuleVersion  = 1.0
--------------------------
--------------------------
instance of MSFT_PackageResource as $MSFT_PackageResource1ref
    ResourceID     = [Package]7Zip
    Path           = E:\Installers\Desktop Software\7-Zip\7z920-x64.msi
    Ensure         = Present
    ProductId      = 23170F69-40C1-2702-0920-000001000000
    SourceInfo     = E:\MofParser\src\Kingsland.MofParser.Sample\dsc\MyServerConfig.ps1::18::9::Package
    Name           = 7-Zip 9.20 (x64 edition)
    ModuleName     = PSDesiredStateConfiguration
    ModuleVersion  = 1.0
--------------------------
--------------------------
instance of OMI_ConfigurationDocument
    Version        = 1.0.0
    Author         = mike.clayton
    GenerationDate = 07/19/2014 10:37:04
    GenerationHost = MyDesktop
--------------------------
```


Issues
------

Post a bug report (and / or send a Pull Request) on the Issues page if you encounter a MOF file that doesn't get processed properly and I'll have a look.


License
-------

This project is licensed under the GNU Lesser General Public License (LGPL) v3. See LICENSE.txt for details.
