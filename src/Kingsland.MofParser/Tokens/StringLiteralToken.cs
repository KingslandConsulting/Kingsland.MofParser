using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class StringLiteralToken : SyntaxToken
    {

        #region Constructors

        public StringLiteralToken(SourceExtent extent, string value)
            : base(extent)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public string Value
        {
            get;
            private set;
        }

        #endregion

    }

}
