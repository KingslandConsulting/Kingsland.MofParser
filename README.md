MofParser
===========

MofParser is a C# library for parsing the contents of PowerShell DSC Managed Object Format (MOF) files.

The library was created with the goal of producing documentation from [Managed Object Format (MOF)](http://www.dmtf.org/standards/cim)
files produced by PowerShell Desired State Configuration scripts, and as a result it currently only understands
a small subset of the full Managed Object Format Specification, primarily "instance" declarations.


Quick Start
===========

To process a MOF file produced by PowerShell DSC, simply pass the filename to the PowerShellDscHelper.ParseMofFile method.
This will read the contents of the file and extract a list of "instance" declarations that are defined in the file. You
can use this list to generate html documentation, or perform any other downstream processing.

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

See the Kingsland.MofParser.Sample project for the full source.


Issues
======

Post a bug report on the Issues page if you encounter a PowerShell DSC MOF that doesn't get processed properly
and I'll have a look.

I don't plan to add support for the full Managed Object Format specification right now (e.g. classes, enumerations),
but I'll consider pull requests that extend the existing lexer and parser.


License
=======

This project is licensed under the GNU Lesser General Public License (LGPL) v3. See LICENSE.txt for details.

