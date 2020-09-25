using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BooleanLiteralToken : SyntaxToken
    {

        #region Constructors

        public BooleanLiteralToken(bool value)
            : this(SourceExtent.Empty, value)
        {
        }

        public BooleanLiteralToken(SourcePosition start, SourcePosition end, string text, bool value)
            : this(new SourceExtent(start, end, text), value)
        {
        }

        public BooleanLiteralToken(SourceExtent extent, bool value)
            : base(extent)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public bool Value
        {
            get;
            private set;
        }

        #endregion

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ??
                 (this.Value ? Constants.TRUE : Constants.FALSE);
        }

        #endregion

    }

}
