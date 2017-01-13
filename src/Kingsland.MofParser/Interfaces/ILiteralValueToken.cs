using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Interfaces
{
    internal interface ILiteralValueToken
    {
        LiteralValueAst ToLiteralValueAst(ParserStream stream);
    }
}