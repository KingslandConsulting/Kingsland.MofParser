using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Lexing;

[TestFixture]
public static partial class LexerTests
{

    [TestFixture]
    public static class ReadBooleanLiteralTokenMethod
    {

        [Test]
        public static void ShouldReadLowerCaseFalseToken()
        {
            var sourceText = "false";
            var expectedTokens = new TokenBuilder()
                .BooleanLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    "false", false
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadMixedCaseFalseToken()
        {
            var sourceText = "False";
            var expectedTokens = new TokenBuilder()
                .BooleanLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    "False", false
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadUpperCaseFalseToken()
        {
            var sourceText = "FALSE";
            var expectedTokens = new TokenBuilder()
                .BooleanLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(4, 1, 5),
                    "FALSE", false
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadLowerCaseTrueToken()
        {
            var sourceText = "true";
            var expectedTokens = new TokenBuilder()
                .BooleanLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(3, 1, 4),
                    "true", true
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadMixedCaseTrueToken()
        {
            var sourceText = "True";
            var expectedTokens = new TokenBuilder()
                .BooleanLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(3, 1, 4),
                    "True", true
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

        [Test]
        public static void ShouldReadUpperCaseTrueToken()
        {
            var sourceText = "TRUE";
            var expectedTokens = new TokenBuilder()
                .BooleanLiteralToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(3, 1, 4),
                    "TRUE", true
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

}
