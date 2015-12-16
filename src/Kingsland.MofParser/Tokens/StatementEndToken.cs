using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class StatementEndToken : Token
    {

        internal StatementEndToken(SourceExtent extent)
            : base(extent)
        {
        }

    }

}
