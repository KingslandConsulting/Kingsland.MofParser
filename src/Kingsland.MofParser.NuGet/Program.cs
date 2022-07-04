namespace Kingsland.MofParser.NuGet;

static class Program
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
                Console.WriteLine($"instance of {instance.ClassName}");
            }
            else
            {
                Console.WriteLine($"instance of {instance.ClassName} as ${instance.Alias}");
            }
            foreach (var property in instance.Properties)
            {
                Console.WriteLine($"    {property.Key,-14} = {property.Value}");
            }
            Console.WriteLine("--------------------------");
        }

    }

}
