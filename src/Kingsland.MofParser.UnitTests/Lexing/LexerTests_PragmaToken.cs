using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Lexing
{

    [TestFixture]
    public static partial class LexerTests
    {

        [TestFixture]
        public static class ReadPragmaTokenMethod
        {

            [Test]
            public static void ShouldReadLowerCasePragmaToken()
            {
                var sourceText = "#pragma";
                var expectedTokens = new TokenBuilder()
                    .PragmaToken(
                        new SourcePosition(0, 1, 1),
                        new SourcePosition(6, 1, 7),
                        "#pragma"
                    )
                    .ToList();
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadMixedCasePragmaToken()
            {
                var sourceText = "#Pragma";
                var expectedTokens = new TokenBuilder()
                    .PragmaToken(
                        new SourcePosition(0, 1, 1),
                        new SourcePosition(6, 1, 7),
                        "#Pragma"
                    )
                    .ToList();
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadUpperCasePragmaToken()
            {
                var sourceText = "#PRAGMA";
                var expectedTokens = new TokenBuilder()
                    .PragmaToken(
                        new SourcePosition(0, 1, 1),
                        new SourcePosition(6, 1, 7),
                        "#PRAGMA"
                    )
                    .ToList();
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

        }

    }

}
