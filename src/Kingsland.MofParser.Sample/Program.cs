using Kingsland.MofParser;
using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Model;
using Kingsland.MofParser.Parsing;
using System;
using System.Linq;

namespace Kingsland.FileFormat.Mof.Tests
{

    class Program
    {

        static void Main(string[] args)
        {

            const string mof = @"
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
            var module = Parser.ParseText(mof);

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

        }

    }

}