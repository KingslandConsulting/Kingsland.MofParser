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
        public static class ReadStringLiteralTokenMethod
        {

            [Test]
            public static void ShouldReadEmptyString()
            {
                var sourceText = "\"\"";
                var expectedTokens = new List<SyntaxToken> {
                    new StringLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(1, 1, 2),
                            "\"\""
                        ),
                        string.Empty
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadBasicString()
            {
                var sourceText = "\"my string literal\"";
                var expectedTokens = new List<SyntaxToken> {
                    new StringLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(18, 1, 19),
                            "\"my string literal\""
                        ),
                        "my string literal"
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadEscapedString()
            {
                var sourceText = @"""my \\ string \"" literal \' with \b lots \t and \n lots \f of \r escapes""";
                var expectedTokens = new List<SyntaxToken> {
                    new StringLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(72, 1, 73),
                            @"""my \\ string \"" literal \' with \b lots \t and \n lots \f of \r escapes"""
                        ),
                        "my \\ string \" literal \' with \b lots \t and \n lots \f of \r escapes"
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

        }

    }

}
