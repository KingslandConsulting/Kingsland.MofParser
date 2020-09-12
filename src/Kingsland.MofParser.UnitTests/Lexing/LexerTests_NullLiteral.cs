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
        public static class ReadNullLiteralTokenMethod
        {

            [Test]
            public static void ShouldReadLowerCaseNullToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("null")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMixedCaseNullToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("Null")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadUpperCaseNullToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("NULL")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

    }

}
