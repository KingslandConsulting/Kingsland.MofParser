using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models.Types;

public sealed record Module
{

    internal Module(IEnumerable<Instance> instances)
    {
        this.Instances = (instances ?? throw new ArgumentNullException(nameof(instances)))
            .ToList().AsReadOnly();
    }

    public ReadOnlyCollection<Instance> Instances
    {
        get;
    }

}
