using System;

namespace Kingsland.MofParser.NuGet
{

    class Program
    {

        static void Main()
        {

            const string filename = "dsc\\MyServer.mof";

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
                foreach (var property in instance.Properties)
                {
                    Console.WriteLine("    {0} = {1}", property.Key.PadRight(14), property.Value.ToString());
                }
                Console.WriteLine("--------------------------");
            }

        }

    }

}
