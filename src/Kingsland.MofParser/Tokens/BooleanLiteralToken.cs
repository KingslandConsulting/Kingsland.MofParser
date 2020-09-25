using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BooleanLiteralToken : SyntaxToken
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


        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ??
                 (this.Value ? Constants.TRUE : Constants.FALSE);
        }

        #endregion

    }

}
