using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IntegerLiteralToken : Token
    {

        internal IntegerLiteralToken(SourceExtent extent, long value)
            : base(extent)
        {
            this.Value = value;
        }

        public long Value
        {
            get;
            private set;
        }

    }

}
