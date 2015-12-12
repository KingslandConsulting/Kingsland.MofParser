using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IntegerLiteralToken : Token
    {

        internal IntegerLiteralToken(SourceExtent extent, int value)
            : base(extent)
        {
            this.Value = value;
        }

        public int Value
        {
            get;
            private set;
        }

    }

}
