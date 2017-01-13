using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class RealLiteralToken : NumericLiteralToken
    {

        internal RealLiteralToken(SourceExtent extent, decimal value)
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
