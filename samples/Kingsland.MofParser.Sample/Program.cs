using Kingsland.MofParser;
using System;
using System.IO;

namespace Kingsland.FileFormat.Mof.Tests
{

    class Program
    {

        static void Main(string[] args)
        {

            //const string filename = "dsc\\MyServer.mof";

            const string rootFolder = "..\\..\\..\\..\\src\\Kingsland.MofParser.UnitTests\\Parsing\\TestCases";
            string filename = Path.Combine(rootFolder, "issue_7_aliasidentifier_array.mof");
            //string filename = Path.Combine(rootFolder, "issue_7_aliasidentifier_single.mof";
            //string filename = Path.Combine(rootFolder, "issue_7_literalvaluearray.mof");

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

        }

    }

}