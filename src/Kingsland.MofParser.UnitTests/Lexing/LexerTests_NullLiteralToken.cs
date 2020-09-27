using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Lexing
{

    [TestFixture]
    public static partial class LexerTests
    {

        [TestFixture]
        public static class ReadNullLiteralTokenMethod
        {

            [Test]
            public static void ShouldReadLowerCaseNullToken()
            {
                var sourceText = "null";
                var expectedTokens = new TokenBuilder()
                    .NullLiteralToken(
                        new SourcePosition(0, 1, 1),
                        new SourcePosition(3, 1, 4),
                        "null"
                    )
                    .ToList();
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadMixedCaseNullToken()
            {
                var sourceText = "Null";
                var expectedTokens = new TokenBuilder()
                    .NullLiteralToken(
                        new SourcePosition(0, 1, 1),
                        new SourcePosition(3, 1, 4),
                        "Null"
                    )
                    .ToList();
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadUpperCaseNullToken()
            {
                var sourceText = "NULL";
                var expectedTokens = new TokenBuilder()
                    .NullLiteralToken(
                        new SourcePosition(0, 1, 1),
                        new SourcePosition(3, 1, 4),
                        "NULL"
                    )
                    .ToList();
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

        }

    }

}
