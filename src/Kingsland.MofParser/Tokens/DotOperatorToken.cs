using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class DotOperatorToken : SyntaxToken
    {

        #region Constructors

        public DotOperatorToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

    }

}
