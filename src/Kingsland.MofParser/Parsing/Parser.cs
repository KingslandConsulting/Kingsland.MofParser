using System.Collections.Generic;
using System.Linq;
using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Parsing
{

    public static class Parser
    {

        public static MofSpecificationAst Parse(List<Token> lexerTokens, ParserQuirks quirks = ParserQuirks.None)
        {

            // remove all comments and whitespace
            var tokens = lexerTokens.Where(lt => !(lt is CommentToken) &&
                                                 !(lt is WhitespaceToken)).ToList();

            var stream = new ParserStream(tokens);
            var program = ParserEngine.ParseMofSpecificationAst(stream, quirks);

            return program;

        }

    }

}
