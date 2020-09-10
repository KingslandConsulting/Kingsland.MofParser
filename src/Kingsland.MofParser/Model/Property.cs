using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using System.Text;

namespace Kingsland.MofParser.Model
{

    public sealed class Property
    {

        #region Builder

        public sealed class Builder
        {

            public string Name
            {
                get;
                set;
            }

            public object Value
            {
                get;
                set;
            }

            public Property Build()
            {
                return new Property(this.Name, this.Value);
            }

        }

        #endregion

        #region Constructors

        private Property()
        {
        }

        public Property(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        #endregion

        #region Properties

        public string Name
        {
            get;
            private set;
        }

        public object Value
        {
            get;
            private set;
        }

        #endregion

        #region Object Interface

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($"{this.Name} = ");
            result.Append(
                this.Value switch
                {
                    null => Constants.NULL,
                    true => Constants.TRUE,
                    false => Constants.FALSE,
                    string s => $"\"{AstMofGenerator.EscapeString(s)}\"",
                    _ => $"!!!{this.Value.GetType().FullName}!!!"
                }
            );
            return result.ToString();
        }

        #endregion

    }

}
