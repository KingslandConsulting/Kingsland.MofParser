using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Objects;
using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser;

public static class PowerShellDscHelper
{

    public static List<Instance> ParseMofFileInstances(string filename)
    {

        // read the text from the mof file
        var sourceText = File.ReadAllText(filename);

        // turn the text into a stream of characters for lexing
        var reader = SourceReader.From(sourceText);

        // lex the characters into a sequence of tokens
        var tokens = Lexer.Lex(reader);

        // parse the tokens into an ast tree
        var ast = Parser.Parse(tokens);

        // scan the ast for any "instance" definitions and convert them
        var instances = ast.Productions
            .Where(p => p is InstanceValueDeclarationAst)
            .Cast<InstanceValueDeclarationAst>()
            .Select(Instance.FromAstNode)
            .ToList();

        // return the result
        return instances.ToList();

    }

}
