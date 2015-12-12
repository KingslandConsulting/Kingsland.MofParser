using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class NullLiteralToken : Token
    {

        internal NullLiteralToken(SourceExtent extent)
            : base(extent)
        {
        }

    }

}
