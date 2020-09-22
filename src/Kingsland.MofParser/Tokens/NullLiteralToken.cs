using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class NullLiteralToken : SyntaxToken
    {

        #region Constructors

        public NullLiteralToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

    }

}
