using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class WhitespaceToken : SyntaxToken
    {

        public WhitespaceToken(SourceExtent extent, string value)
            : base(extent)
        {
            this.Value = value;
        }

        #region Properties

        public string Value
        {
            get;
            private set;
        }

        #endregion

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ?? this.Value;
        }

        #endregion

    }

}
