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
        public static class ReadAttributeCloseTokenMethod
        {

            [Test]
            public static void ShouldReadAttributeCloseToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("]")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new AttributeCloseToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "]"
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadAttributeOpenTokenMethod
        {

            [Test]
            public static void ShouldReadAttributeOpenToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("[")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new AttributeOpenToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "["
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadBlockCloseTokenMethod
        {

            [Test]
            public static void ShouldReadBlockCloseToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("}")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new BlockCloseToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "}"
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadBlockOpenTokenMethod
        {

            [Test]
            public static void ShouldReaBlockOpenToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("{")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new BlockOpenToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "{"
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadColonTokenMethod
        {

            [Test]
            public static void ShouldReadColonToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From(":")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new ColonToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            ":"
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadCommaTokenMethod
        {

            [Test]
            public static void ShouldReadCommaToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From(",")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new CommaToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            ","
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadDotOperatorTokenMethod
        {

            [Test]
            public static void ShouldReadDotOperatorToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From(".")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new DotOperatorToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "."
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadDotOperatorTokenWithTrailingNonDecimalDigit()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From(".abc")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new DotOperatorToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "."
                        )
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(1, 1, 2),
                            new SourcePosition(3, 1, 4),
                            "abc"
                        ),
                        "abc"
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadEqualsOperatorTokenMethod
        {

            [Test]
            public static void ShouldReadEqualsOperatorToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("=")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new EqualsOperatorToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "="
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadParenthesesCloseTokenMethod
        {

            [Test]
            public static void ShouldReadEqualsOperatorToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From(")")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new ParenthesisCloseToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            ")"
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadParenthesesOpenTokenMethod
        {

            [Test]
            public static void ShouldReadParenthesesOpenToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("(")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new ParenthesisOpenToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "("
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadStatementEndTokenMethod
        {

            [Test]
            public static void ShouldReadStatementEndToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From(";")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new StatementEndToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            ";"
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

    }

}
