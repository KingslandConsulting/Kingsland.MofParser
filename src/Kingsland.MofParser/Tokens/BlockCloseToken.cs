using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BlockCloseToken : SyntaxToken
    {

        #region Constructors

        public BlockCloseToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

    }

}
