using Kingsland.MofParser.Ast;
using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Models;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.UnitTests.Helpers;
using Kingsland.ParseFx.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region Roundtrip Test Cases

    //[TestFixture]
    //public static class ConvertToMofMethodTestCasesWmiWinXp
    //{
    //    [Test, TestCaseSource(typeof(ConvertToMofMethodTestCasesWmiWinXp), "GetTestCases")]
    //    public static void ConvertToMofMethodTestsFromDisk(string mofFilename)
    //    {
    //        ConvertToMofTests.MofGeneratorRoundtripTest(mofFilename);
    //    }
    //    public static IEnumerable<TestCaseData> GetTestCases
    //    {
    //        get
    //        {
    //            return TestUtils.GetMofTestCase("Parsing\\WMI\\WinXp");
    //        }
    //    }
    //}

    //[TestFixture]
    //public static class ConvertToMofMethodGolfExamples
    //{
    //    //[Test, TestCaseSource(typeof(ConvertToMofMethodGolfExamples), "GetTestCases")]
    //    public static void ConvertToMofMethodTestsFromDisk(string mofFilename)
    //    {
    //        ConvertToMofTests.MofGeneratorRoundtripTest(mofFilename);
    //    }
    //    public static IEnumerable<TestCaseData> GetTestCases
    //    {
    //        get
    //        {
    //            return TestUtils.GetMofTestCase("Parsing\\DSP0221_3.0.1");
    //        }
    //    }
    //}

    private static void AssertRoundtrip(
        string sourceText,
        List<SyntaxToken> expectedTokens,
        MofSpecificationAst? expectedAst = null,
        Module? expectedModule = null,
        ParserQuirks parserQuirks = ParserQuirks.None
    )
    {
        if (expectedTokens is null)
        {
            throw new ArgumentNullException(nameof(expectedTokens));
        }
        // check the lexer generates the expected tokens
        var actualTokens = Lexer.Lex(SourceReader.From(sourceText));
        LexerAssert.AreEqual(expectedTokens, actualTokens, true);
        // check the expected tokens serialize back ok
        var actualTokenText = TokenSerializer.ToSourceText(expectedTokens);
        Assert.AreEqual(sourceText, actualTokenText);
        // check the parser generates the expected ast
        var actualAst = Parser.Parse(actualTokens, parserQuirks);
        AstAssert.AreEqual(expectedAst, actualAst, true);
        // check the code generator builds the original source text
        var actualAstText = actualAst.ToString(
            new AstWriterOptions(
                newLine: Environment.NewLine,
                indentStep: "    ",
                quirks: MofQuirks.None
            )
        );
        Assert.AreEqual(sourceText, actualAstText);
    }

    private static void AssertRoundtripException(string sourceText, string expectedMessage, ParserQuirks parserQuirks = ParserQuirks.None)
    {
        var tokens = Lexer.Lex(SourceReader.From(sourceText));
        var tokensMof = TokenSerializer.ToSourceText(tokens);
        var ex = Assert.Throws<UnexpectedTokenException>(
            () => {
                var astNodes = Parser.Parse(tokens, parserQuirks);
            }
        );
        Assert.Multiple(() =>
        {
            Assert.IsNotNull(ex);
            Assert.AreEqual(expectedMessage, ex?.Message);
        });
    }

    #endregion

}
