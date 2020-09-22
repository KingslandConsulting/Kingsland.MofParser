using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BlockOpenToken : SyntaxToken
    {

        #region Constructors

        public BlockOpenToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

    }

}
