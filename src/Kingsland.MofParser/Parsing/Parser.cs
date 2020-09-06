using System.Collections.Generic;
using System.Linq;
using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;
using Kingsland.ParseFx.Syntax;

namespace Kingsland.MofParser.Parsing
{

    public static class Parser
    {

        public static MofSpecificationAst Parse(List<SyntaxToken> lexerTokens, ParserQuirks quirks = ParserQuirks.None)
        {

            // remove all comments and whitespace
            var tokens = lexerTokens.Where(lt => !(lt is CommentToken) &&
                                                 !(lt is WhitespaceToken)).ToList();

            var stream = new TokenStream(tokens);
            var program = ParserEngine.ParseMofSpecificationAst(stream, quirks);

            return program;

        }

    }

}
