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
        public static class ReadStringLiteralTokenMethod
        {

            [Test]
            public static void ShouldReadEmptyString()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("\"\"")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadBasicString()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("\"my string literal\"")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadEscapedString()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From(@"""my \\ string \"" literal \' with \b lots \t and \n lots \f of \r escapes""")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

    }

}
