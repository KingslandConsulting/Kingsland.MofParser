using Kingsland.MofParser.HtmlReport.Wrappers;
using RazorEngine.Templating;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kingsland.MofParser.Sample
{

    class Program
    {

        static void Main(string[] args)
        {

            // get the list of mof filenames
            var mofRoot = System.Reflection.Assembly.GetExecutingAssembly().Location;
            mofRoot = Path.GetDirectoryName(mofRoot);
            mofRoot = Path.GetDirectoryName(mofRoot);
            mofRoot = Path.GetDirectoryName(mofRoot);
            mofRoot = Path.Combine(mofRoot, "dsc");
            var filenames = Directory.GetFiles(mofRoot, "*.mof", SearchOption.AllDirectories);

            // parse the mof files
            var wrappers = new List<InstanceWrapper>();
            foreach (var filename in filenames)
            {
                var instances = PowerShellDscHelper.ParseMofFileInstances(filename);
                wrappers.AddRange(
                        instances.Select(instance => InstanceWrapper.FromInstance(
                            Path.GetFileName(Path.GetDirectoryName(filename)),
                            Path.GetFileNameWithoutExtension(filename),
                            instance))
                );
            }

            var template = File.ReadAllText("MofFileSummary.cshtml");
            var result = RazorEngine.Engine.Razor.RunCompile(template, model: wrappers);
            File.WriteAllText("summary.htm", result);

        }

    }

}
