using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class ParenthesisOpenToken : SyntaxToken
    {

        #region Constructors

        public ParenthesisOpenToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

    }

}
