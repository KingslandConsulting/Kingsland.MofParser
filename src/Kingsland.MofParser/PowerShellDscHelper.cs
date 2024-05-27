using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Models.Converter;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Text;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser;

public static class PowerShellDscHelper
{

    public static Module ParseMofFile(string filename)
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
        var module = ModelConverter.ConvertMofSpecificationAst(ast);
        return module;
    }

    public static ReadOnlyCollection<Instance> ParseMofFileInstances(string filename)
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
        var module = ModelConverter.ConvertMofSpecificationAst(ast);
        var instances = module.Instances;
        // return the result
        return instances;
    }

}
