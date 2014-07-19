using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BooleanLiteralToken : Token
    {

        internal BooleanLiteralToken(SourceExtent extent, bool value)
            : base(extent)
        {
            this.Value = value;
        }

        public bool Value
        {
            get;
            private set;
        }

    }

}
