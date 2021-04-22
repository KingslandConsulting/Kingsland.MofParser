using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Model;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Parsing
{

    public static class Parser
    {

        public static MofSpecificationAst Parse(List<SyntaxToken> lexerTokens, ParserQuirks quirks = ParserQuirks.None)
        {

            // remove all comments and whitespace
            var tokens = lexerTokens.Where(
                lt => !(lt is CommentToken) &&
                    !(lt is WhitespaceToken)
            ).ToList();

            var stream = new TokenStream(tokens);
            var program = ParserEngine.ParseMofSpecificationAst(stream, quirks);

            return program;

        }

        public static Module ParseFile(string filename)
        {
            return Parser.ParseText(File.ReadAllText(filename));
        }

        public static Module ParseText(string mofText, ParserQuirks quirks = ParserQuirks.None)
        {
            // turn the text into a stream of characters for lexing
            var reader = SourceReader.From(mofText);
            // lex the characters into a sequence of tokens
            var tokens = Lexer.Lex(reader);
            // parse the tokens into an ast tree
            var mofSpecificationAst = Parser.Parse(tokens, quirks);
            // convert the ast into a Module
            var module = ModelConverter.ConvertMofSpecificationAst(mofSpecificationAst);
            // return the result
            return module;
        }

    }

}
