using Kingsland.MofParser.Lexing;

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
