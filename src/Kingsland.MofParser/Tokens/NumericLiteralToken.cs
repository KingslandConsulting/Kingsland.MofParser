using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Interfaces;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Tokens
{
    public abstract class NumericLiteralToken : Token
    {
        internal NumericLiteralToken(SourceExtent extent) : base(extent)
        {
        }
    }
}