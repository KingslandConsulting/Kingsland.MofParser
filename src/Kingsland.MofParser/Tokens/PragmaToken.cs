using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class PragmaToken : SyntaxToken
    {

        #region Constructors

        public PragmaToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

    }

}
