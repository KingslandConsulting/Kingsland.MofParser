using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Source;

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
