using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public class PragmaAst : MofProductionAst
    {

        public string Pragma
        {
            get;
            private set;
        }

        public string Argument
        {
            get;
            private set;
        }

        internal new static PragmaAst Parse(ParserStream stream)
        {
            var ast = new PragmaAst();

            stream.Read<PragmaToken>();
            ast.Pragma = stream.Read<IdentifierToken>().Name;
            stream.Read<ParenthesesOpenToken>();
            ast.Argument = stream.Read<StringLiteralToken>().Value;
            stream.Read<ParenthesesCloseToken>();

            return ast;
        }

    }

}
