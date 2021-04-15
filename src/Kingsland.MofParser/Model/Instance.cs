using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Model
{

    public sealed record Instance
    {

        #region Builder

        public sealed class Builder
        {

            public string TypeName
            {
                get;
                set;
            }

            public string Alias
            {
                get;
                set;
            }

            public List<Property> Properties
            {
                get;
                set;
            }

            public Instance Build()
            {
                return new Instance
                {
                    TypeName = this.TypeName,
                    Alias = this.Alias,
                    Properties = new ReadOnlyCollection<Property>(
                        this.Properties ?? new List<Property>()
                    )
                };
            }

        }

        #endregion

        #region Constructors

        private Instance()
        {
        }

        #endregion

        #region Properties

        public string TypeName
        {
            get;
            private init;
        }

        public string Alias
        {
            get;
            private init;
        }

        public ReadOnlyCollection<Property> Properties
        {
            get;
            private init;
        }

        #endregion

        #region Methods

        //public T GetValue<T>(string name)
        //{
        //    return (T)this.Properties.Single(p => p.Name == name).Value;
        //}

        //public bool TryGetValue<T>(string name, out T result)
        //{
        //    var property = this.Properties.SingleOrDefault(p => p.Name == name);
        //    if (property == null)
        //    {
        //        result = default;
        //        return false;
        //    }
        //    var value = property.Value;
        //    if (value is T typed)
        //    {
        //        result = typed;
        //        return true;
        //    }
        //    result = default;
        //    return false;
        //}

        #endregion

        #region Object Interface

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($"{Constants.INSTANCE} {Constants.OF} {this.TypeName}");
            if (!string.IsNullOrEmpty(this.Alias))
            {
                result.Append($" {Constants.AS} ${this.Alias}");
            }
            return result.ToString();
        }

        #endregion

    }

}
