using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Models;

public sealed record Module
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.Enumerations = [];
            this.Instances = [];
        }

        public List<Enumeration> Enumerations
        {
            get;
            set;
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
        this.Instances = (instances ?? throw new ArgumentNullException(nameof(instances)))
            .ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    public ReadOnlyCollection<Instance> Instances
    {
        get;
    }

    #endregion

}
