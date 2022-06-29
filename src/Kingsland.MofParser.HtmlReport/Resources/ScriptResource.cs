using Kingsland.MofParser.Objects;

namespace Kingsland.MofParser.HtmlReport.Resources;

public sealed class ScriptResource : DscResource
{

    public ScriptResource(string filename, string computerName, Instance instance)
        : base(filename, computerName, instance)
    {
    }

    public string GetScript
    {
        get
        {
            return base.GetStringProperty(nameof(this.GetScript));
        }
    }

    public string TestScript
    {
        get
        {
            return base.GetStringProperty(nameof(this.TestScript));
        }
    }

    public string SetScript
    {
        get
        {
            return base.GetStringProperty(nameof(this.SetScript));
        }
    }

}
