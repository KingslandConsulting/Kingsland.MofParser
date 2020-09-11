using Kingsland.MofParser.Lexing;
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
        public static class ReadWhitespaceTokenMethod
        {

            [Test]
            public static void ShouldReadSpaceWhitespaceToken()
            {
                var sourceText = "     ";
                var expectedTokens = new List<SyntaxToken> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "     "
                        )
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadTabWhitespaceToken()
            {
                var sourceText = "\t\t\t\t\t";
                var expectedTokens = new List<SyntaxToken> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "\t\t\t\t\t"
                        )
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadCrWhitespaceToken()
            {
                var sourceText = "\r\r\r\r\r";
                var expectedTokens = new List<SyntaxToken> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 5, 1),
                            "\r\r\r\r\r"
                        )
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadLfWhitespaceToken()
            {
                var sourceText = "\n\n\n\n\n";
                var expectedTokens = new List<SyntaxToken> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 5, 1),
                            "\n\n\n\n\n"
                        )
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadCrLfWhitespaceToken()
            {
                var sourceText = "\r\n\r\n\r\n\r\n\r\n";
                var expectedTokens = new List<SyntaxToken> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(9, 5, 2),
                            "\r\n\r\n\r\n\r\n\r\n"
                        )
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadMixedWhitespaceToken()
            {
                var sourceText =
                    "     \t\t\t\t\t\r\r\r\r\r\n\n\n\n\n\r\n\r\n\r\n\r\n\r\n";
                var expectedTokens = new List<SyntaxToken> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(29, 14, 2),
                            "     \t\t\t\t\t\r\r\r\r\r\n\n\n\n\n\r\n\r\n\r\n\r\n\r\n"
                        )
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

        }

    }

}
