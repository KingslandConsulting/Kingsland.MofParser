using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{
    public abstract class NumericLiteralToken : Token
    {
        internal NumericLiteralToken(SourceExtent extent) : base(extent)
        {
        }
    }
}