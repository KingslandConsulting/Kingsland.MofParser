using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using NUnit.Framework;
using System.Collections.Generic;

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
                var expectedTokens = new List<SyntaxToken> {
                    new NullLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "null"
                        )
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadMixedCaseNullToken()
            {
                var sourceText = "Null";
                var expectedTokens = new List<SyntaxToken> {
                    new NullLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "Null"
                        )
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadUpperCaseNullToken()
            {
                var sourceText = "NULL";
                var expectedTokens = new List<SyntaxToken> {
                    new NullLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "NULL"
                        )
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

        }

    }

}
