using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class RealLiteralToken : SyntaxToken
    {

        public RealLiteralToken(SourceExtent extent, double value)
            : base(extent)
        {
            this.Value = value;
        }

        public double Value
        {
            get;
            private set;
        }


        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ?? this.Value.ToString();
        }

        #endregion

    }

}
