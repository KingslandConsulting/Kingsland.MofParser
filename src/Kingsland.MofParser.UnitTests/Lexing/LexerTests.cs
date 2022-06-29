using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Helpers;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Lexing;

[TestFixture]
public static partial class LexerTests
{

    [TestFixture]
    public static class EmptyFileTests
    {

        [Test]
        public static void ShouldReadEmptyFile()
        {
            var sourceText = string.Empty;
            var expectedTokens = new TokenBuilder()
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    [TestFixture]
    public static class MiscTests
    {

        [Test]
        public static void MissingWhitespaceTest()
        {
            var sourceText = "12345myIdentifier";
            var expectedTokens = new TokenBuilder()
                .IntegerLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    "12345", IntegerKind.DecimalValue, 12345
                )
                .IdentifierToken(
                    new SourcePosition(5, 1, 6),
                    new SourcePosition(16, 1, 17),
                    "myIdentifier"
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    private static void AssertLexerTest(string sourceText, List<SyntaxToken> expectedTokens)
    {
        var actualTokens = Lexer.Lex(SourceReader.From(sourceText));
        LexerAssert.AreEqual(expectedTokens, actualTokens, false);
    }

    [TestFixture]
    public static class LexMethodTestCases
    {
        //[Test, TestCaseSource(typeof(LexMethodTestCases), "GetTestCases")]
        public static void LexMethodTestsFromDisk(string mofFilename)
        {
            LexerTests.LexMethodTest(mofFilename);
        }
        public static IEnumerable<TestCaseData> GetTestCases
        {
            get
            {
                return TestUtils.GetMofTestCase("cim_schema_2.51.0Final-MOFs");
            }
        }
    }

    [TestFixture]
    public static class LexCimSpec
    {
        //[Test, TestCaseSource(typeof(LexCimSpec), "GetTestCases")]
        public static void LexMethodTestsFromDisk(string mofFilename)
        {
            LexerTests.LexMethodTest(mofFilename);
        }
        public static IEnumerable<TestCaseData> GetTestCases
        {
            get
            {
                return TestUtils.GetMofTestCase("Lexer\\TestCases");
            }
        }
    }

    private static void LexMethodTest(string mofFilename)
    {
        var mofText = File.ReadAllText(mofFilename);
        var reader = SourceReader.From(mofText);
        var actualTokens = Lexer.Lex(reader);
        var actualText = TestUtils.ConvertToJson(actualTokens);
        var expectedFilename = Path.Combine(
            Path.GetDirectoryName(mofFilename)
                ?? throw new NullReferenceException(),
            Path.GetFileNameWithoutExtension(mofFilename) + ".json"
        );
        if (!File.Exists(expectedFilename))
        {
            File.WriteAllText(expectedFilename, actualText);
        }
        var expectedText = File.ReadAllText(expectedFilename);
        Assert.AreEqual(expectedText, actualText);
    }

}
