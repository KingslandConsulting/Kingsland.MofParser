using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class CommentToken : Token
    {

        internal CommentToken(SourceExtent extent)
            : base(extent)
        {
        }

    }

}
