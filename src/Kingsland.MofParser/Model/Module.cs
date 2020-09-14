using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Model
{

    public sealed class Module
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
                return new Module
                {
                    Instances = new ReadOnlyCollection<Instance>(
                        this.Instances ?? new List<Instance>()
                    )
                };
            }

        }

        #endregion

        #region Constructors

        private Module()
        {
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<Instance> Instances
        {
            get;
            private set;
        }

        #endregion

    }

}
