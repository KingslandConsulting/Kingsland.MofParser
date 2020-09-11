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
        public static class EmptyFileTests
        {

            [Test]
            public static void ShouldReadEmptyFile()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From(string.Empty)
                );
                var expectedTokens = new List<SyntaxToken>();
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class MiscTests
        {

            [Test]
            public static void MissingWhitespaceTest()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("12345myIdentifier")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "12345"
                        ),
                        IntegerKind.DecimalValue, 12345
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(5, 1, 6),
                            new SourcePosition(16, 1, 17),
                            "myIdentifier"
                        ),
                        "myIdentifier"
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        private static void AssertLexerTest(string sourceText, List<SyntaxToken> expectedTokens)
        {
            var actualTokens = Lexer.Lex(SourceReader.From(sourceText));
            LexerAssert.AreEqual(expectedTokens, actualTokens);
        }

    }

}