using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BlockCloseToken : SyntaxToken
    {

        public BlockCloseToken(SourceExtent extent)
            : base(extent)
        {
        }

    }

}
