using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Lexing;

[TestFixture]
public static partial class LexerTests
{

    [TestFixture]
    public static class ReadAttributeCloseTokenMethod
    {

        [Test]
        public static void ShouldReadAttributeCloseToken()
        {
            var sourceText = "]";
            var expectedTokens = new TokenBuilder()
                .AttributeCloseToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    "]"
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    [TestFixture]
    public static class ReadAttributeOpenTokenMethod
    {

        [Test]
        public static void ShouldReadAttributeOpenToken()
        {
            var sourceText = "[";
            var expectedTokens = new TokenBuilder()
                .AttributeOpenToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    "["
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    [TestFixture]
    public static class ReadBlockCloseTokenMethod
    {

        [Test]
        public static void ShouldReadBlockCloseToken()
        {
            var sourceText = "}";
            var expectedTokens = new TokenBuilder()
                .BlockCloseToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    "}"
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    [TestFixture]
    public static class ReadBlockOpenTokenMethod
    {

        [Test]
        public static void ShouldReaBlockOpenToken()
        {
            var sourceText = "{";
            var expectedTokens = new TokenBuilder()
                .BlockOpenToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    "{"
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    [TestFixture]
    public static class ReadColonTokenMethod
    {

        [Test]
        public static void ShouldReadColonToken()
        {
            var sourceText = ":";
            var expectedTokens = new TokenBuilder()
                .ColonToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    ":"
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    [TestFixture]
    public static class ReadCommaTokenMethod
    {

        [Test]
        public static void ShouldReadCommaToken()
        {
            var sourceText = ",";
            var expectedTokens = new TokenBuilder()
                .CommaToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    ","
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    [TestFixture]
    public static class ReadDotOperatorTokenMethod
    {

        [Test]
        public static void ShouldReadDotOperatorToken()
        {
            var sourceText = ".";
            var expectedTokens = new TokenBuilder()
                .DotOperatorToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    "."
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadDotOperatorTokenWithTrailingNonDecimalDigit()
        {
            var sourceText = ".abc";
            var expectedTokens = new TokenBuilder()
                .DotOperatorToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    "."
                )
                .IdentifierToken(
                    new SourcePosition(1, 1, 2),
                    new SourcePosition(3, 1, 4),
                    "abc"
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    [TestFixture]
    public static class ReadEqualsOperatorTokenMethod
    {

        [Test]
        public static void ShouldReadEqualsOperatorToken()
        {
            var sourceText = "=";
            var expectedTokens = new TokenBuilder()
                .EqualsOperatorToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    "="
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    [TestFixture]
    public static class ReadParenthesesCloseTokenMethod
    {

        [Test]
        public static void ShouldReadEqualsOperatorToken()
        {
            var sourceText = ")";
            var expectedTokens = new TokenBuilder()
                .ParenthesisCloseToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    ")"
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    [TestFixture]
    public static class ReadParenthesesOpenTokenMethod
    {

        [Test]
        public static void ShouldReadParenthesesOpenToken()
        {
            var sourceText = "(";
            var expectedTokens = new TokenBuilder()
                .ParenthesisOpenToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    "("
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

    [TestFixture]
    public static class ReadStatementEndTokenMethod
    {

        [Test]
        public static void ShouldReadStatementEndToken()
        {
            var sourceText = ";";
            var expectedTokens = new TokenBuilder()
                .StatementEndToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(0, 1, 1),
                    ";"
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

}
