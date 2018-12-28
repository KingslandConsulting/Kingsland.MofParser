using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BooleanLiteralToken : Token
    {

        public BooleanLiteralToken(SourceExtent extent, bool value)
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
