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
const string sourceText = @"
instance of MSFT_RoleResource as $MSFT_RoleResource1ref
{
    ResourceID = ""[WindowsFeature]IIS"";
    Ensure = ""Present"";
    SourceInfo = ""D:\\dsc\\MyServerConfig.ps1::6::9::WindowsFeature"";
    Name = ""Web-Server"";
    ModuleName = ""PSDesiredStateConfiguration"";
    ModuleVersion = ""1.0"";
};";

// parse the mof file
var module = Parser.ParseText(sourceText);

// display the instances
foreach (var instance in module.Instances)
{
    Console.WriteLine($"----------------------------------");
    Console.WriteLine($"typename = {instance.TypeName}");
    Console.WriteLine($"alias    = {instance.Alias}");
    Console.WriteLine($"properties:");
    foreach (var property in instance.Properties)
    {
        Console.WriteLine("    {0} = {1}", property.Name.PadRight(13), property.Value);
    }
    Console.WriteLine($"----------------------------------");
}

// output:
// ----------------------------------
// typename = MSFT_RoleResource
// alias    = MSFT_RoleResource1ref
// properties:
//     ResourceID    = [WindowsFeature]IIS
//     Ensure        = Present
//     SourceInfo    = D:\dsc\MyServerConfig.ps1::6::9::WindowsFeature
//     Name          = Web-Server
//     ModuleName    = PSDesiredStateConfiguration
//     ModuleVersion = 1.0
// ----------------------------------
```


Issues
------

Post a bug report (and / or send a Pull Request) on the Issues page if you encounter a MOF file that doesn't get processed properly and I'll have a look.


License
-------

This project is licensed under the GNU Lesser General Public License (LGPL) v3. See LICENSE.txt for details.
