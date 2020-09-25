using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BlockOpenToken : SyntaxToken
    {

        public BlockOpenToken(SourceExtent extent)
            : base(extent)
        {
        }

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ?? "{";
        }

        #endregion

    }

}
