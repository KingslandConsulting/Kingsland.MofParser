using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class ParenthesisCloseToken : SyntaxToken
    {

        #region Constructors

        public ParenthesisCloseToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

    }

}
