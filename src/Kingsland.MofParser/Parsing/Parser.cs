using System.Collections.Generic;
using System.Linq;
using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Parsing
{

    public static class Parser
    {

        public static AstNode Parse(List<Token> lexerTokens)
        {

            // remove all comments and whitespace
            var tokens = lexerTokens.Where(lt => !(lt is CommentToken) &&
                                                 !(lt is WhitespaceToken)).ToList();

            var stream = new ParserStream(tokens);
            var program = ParserEngine.ParseMofSpecificationAst(stream);

            return program;

        }

    }

}
