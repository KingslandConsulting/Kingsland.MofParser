using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Interfaces;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class NullLiteralToken : Token, ILiteralValueToken
    {

        internal NullLiteralToken(SourceExtent extent)
            : base(extent)
        {
        }

        LiteralValueAst ILiteralValueToken.ToLiteralValueAst(ParserStream stream)
        {
            return NullValueAst.Parse(stream);
        }

    }

}
