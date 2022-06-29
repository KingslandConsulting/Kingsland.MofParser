using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Model;

public sealed record Module
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.Instances = new List<Instance>();
        }

        public List<Instance> Instances
        {
            get;
            set;
        }

        public Module Build()
        {
            return new Module(
                this.Instances
            );
        }

    }

    #endregion

    #region Constructors

    internal Module(IEnumerable<Instance> instances)
    {
        this.Instances = new ReadOnlyCollection<Instance>(
            (instances ?? throw new ArgumentNullException(nameof(instances)))
                .ToList()
        );
    }

    #endregion

    #region Properties

    public ReadOnlyCollection<Instance> Instances
    {
        get;
    }

    #endregion

}
