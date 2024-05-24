using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Sample;

static class Program
{

    static void Main()
    {

        const string sourceText = @"
            instance of MSFT_RoleResource as $MSFT_RoleResource1ref
            {
                ResourceID = ""[WindowsFeature]IIS"";
                Ensure = ""Present"";
                SourceInfo = ""D:\\dsc\\MyServerConfig.ps1::6::9::WindowsFeature"";
                Name = ""Web-Server"";
                ModuleName = ""PSDesiredStateConfiguration"";
                ModuleVersion = ""1.0"";
            };
        ";

        const string sourceText2 = @"
            instance of CustomObject1 as $CustomObject11ref
            {
                Bool1 = True;
                String1 = ""21Object1String1"";

            };

            instance of ClassBasedResource2 as $ClassBasedResource21ref
            {
                Key = ""2k1"";
                ModuleVersion = ""0.1.0"";
                Object1 = $CustomObject11ref;
                Ensure = ""Present"";
                SourceInfo = ""::3::1::ClassBasedResource2"";
                ResourceID = ""[ClassBasedResource2]2k1::[ClassBasedResources2]ClassBasedResources2"";
                ModuleName = ""ClassBased"";
                ConfigurationName = ""RootConfiguration"";
            };

            instance of OMI_ConfigurationDocument
            {
                Version = ""2.0.0"";
                MinimumCompatibleVersion = ""1.0.0"";
                CompatibleVersionAdditionalProperties= {""Omi_BaseResource:ConfigurationName""};
                Author=""randr"";
                GenerationDate=""05/23/2024 14:06:44"";
                GenerationHost=""RAANDREE0"";
                Name=""MOF__ NA"";
            };
        ";

        // parse the mof file
        var module = Parser.ParseText(sourceText2);

        // display the instances
        foreach (var instance in module.Instances)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"typename = {instance.TypeName}");
            Console.WriteLine($"alias    = {instance.Alias}");
            Console.WriteLine("properties:");
            foreach (var property in instance.Properties)
            {
                Console.WriteLine("    {0} = {1}", property.Name.PadRight(13), property.Value);
            }
            Console.WriteLine("----------------------------------");
        }

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

    }

}
