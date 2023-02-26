using Kingsland.MofParser.HtmlReport.Resources;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.HtmlReport.Wrappers;

public sealed class ResourceGroup
{

    public ResourceGroup(
        string filename, string computerName, IEnumerable<DscResource> wrappers
    )
    {
        this.Filename = filename ?? throw new ArgumentNullException(nameof(filename));
        this.ComputerName = computerName ?? throw new ArgumentNullException(nameof(computerName));
        this.Wrappers = new(
            new List<DscResource>(
                wrappers ?? throw new ArgumentNullException(nameof(wrappers))
            )
        );
    }

    public string Filename
    {
        get;
    }

    public string ComputerName
    {
        get;
    }

    public ReadOnlyCollection<DscResource> Wrappers
    {
        get;
    }

}
