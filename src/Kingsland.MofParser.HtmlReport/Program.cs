using Kingsland.MofParser.HtmlReport.Resources;
using Kingsland.MofParser.Parsing;
using RazorEngine.Templating;
using System.Reflection;

namespace Kingsland.MofParser.HtmlReport;

static class Program
{

    static void Main()
    {

        // get the list of mof filenames
        var mofRoot = Assembly.GetExecutingAssembly().Location;
        mofRoot = Path.GetDirectoryName(mofRoot);
        mofRoot = Path.GetDirectoryName(mofRoot);
        mofRoot = Path.GetDirectoryName(mofRoot);
        mofRoot = Path.GetDirectoryName(mofRoot);
        mofRoot = Path.Combine(
            mofRoot ?? throw new NullReferenceException(),
            "dsc"
        );
        var filenames = Directory.GetFiles(mofRoot, "*.mof", SearchOption.AllDirectories)
            ?? throw new NullReferenceException();

        // parse the mof files
        var resources = new List<DscResource>();
        foreach (var filename in filenames)
        {
            var module = Parser.ParseText(filename);
            resources.AddRange(
                module.Instances.Select(
                    instance => DscResource.FromInstance(
                        Path.GetFileName(
                            Path.GetDirectoryName(
                                filename
                            ) ?? throw new NullReferenceException()
                        ),
                        Path.GetFileNameWithoutExtension(filename),
                        instance
                    )
                )
            );
        }

        var template = File.ReadAllText("MofFileSummary.cshtml");
        var result = RazorEngine.Engine.Razor.RunCompile(template, "summary", null, resources);
        File.WriteAllText("summary.htm", result);

    }

}
