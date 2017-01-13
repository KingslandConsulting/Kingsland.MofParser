using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class DecimalLiteralToken : NumericLiteralToken
    {

        internal DecimalLiteralToken(SourceExtent extent, decimal value)
            : base(extent)
        {
            this.Value = value;
        }

        public decimal Value
        {
            get;
            private set;
        }

    }

}
