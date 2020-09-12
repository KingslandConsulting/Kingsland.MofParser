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
        public static class ReadBooleanLiteralTokenMethod
        {

            [Test]
            public static void ShouldReadLowerCaseFalseToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("false")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "false"
                        ),
                        false
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMixedCaseFalseToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("False")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "False"
                        ),
                        false
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadUpperCaseFalseToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("FALSE")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "FALSE"
                        ),
                        false
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadLowerCaseTrueToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("true")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "true"
                        ),
                        true
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMixedCaseTrueToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("True")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "True"
                        ),
                        true
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadUpperCaseTrueToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("TRUE")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "TRUE"
                        ),
                        true
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

    }

}
