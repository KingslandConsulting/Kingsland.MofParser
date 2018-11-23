using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class PragmaToken : Token
    {

        internal PragmaToken(SourceExtent extent)
            : base(extent)
        {
        }

    }

}
