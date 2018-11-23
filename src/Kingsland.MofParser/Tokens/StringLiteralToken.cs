using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class StringLiteralToken : Token
    {

        internal StringLiteralToken(SourceExtent extent, string value)
            : base(extent)
        {
            this.Value = value;
        }

        public string Value
        {
            get;
            private set;
        }

    }

}
