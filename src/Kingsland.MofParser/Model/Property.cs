using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System;
using System.Text;

namespace Kingsland.MofParser.Model
{

    public sealed record Property
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

        internal Property(string name, object value)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion

        #region Properties

        public string Name
        {
            get;
            private init;
        }

        public object Value
        {
            get;
            private init;
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
                    string s => $"\"{StringLiteralToken.EscapeString(s)}\"",
                    _ => $"!!!{this.Value.GetType().FullName}!!!"
                }
            );
            return result.ToString();
        }

        #endregion

    }

}
