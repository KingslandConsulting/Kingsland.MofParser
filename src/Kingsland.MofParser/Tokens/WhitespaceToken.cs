using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class WhitespaceToken : SyntaxToken
    {

        #region Constructors

        public WhitespaceToken(SourceExtent extent)
            : base(extent)
        {
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
