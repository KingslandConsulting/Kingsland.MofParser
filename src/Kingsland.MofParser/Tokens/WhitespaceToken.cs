using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class WhitespaceToken : SyntaxToken
    {

        public WhitespaceToken(SourceExtent extent)
            : base(extent)
        {
        }

    }

}
