using Kingsland.MofParser.Objects;

namespace Kingsland.MofParser.HtmlReport.Resources;

public sealed class ScriptResource : DscResource
{

    public ScriptResource(string filename, string computerName, Instance instance)
        : base(filename, computerName, instance)
    {
    }

    public string? GetScript =>
        this.GetStringProperty(nameof(this.GetScript));

    public string? TestScript =>
        this.GetStringProperty(nameof(this.TestScript));

    public string? SetScript =>
        this.GetStringProperty(nameof(this.SetScript));

}
