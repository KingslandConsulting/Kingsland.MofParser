using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class CommentToken : SyntaxToken
    {

        public CommentToken(SourceExtent extent)
            : base(extent)
        {
        }

    }

}
