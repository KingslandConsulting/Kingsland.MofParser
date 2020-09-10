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
        public static class ReadPragmaTokenMethod
        {

            [Test]
            public static void ShouldReadLowerCasePragmaToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("#pragma")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new PragmaToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(6, 1, 7),
                            "#pragma"
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMixedCasePragmaToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("#Pragma")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new PragmaToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(6, 1, 7),
                            "#Pragma"
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadUpperCasePragmaToken()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("#PRAGMA")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new PragmaToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(6, 1, 7),
                            "#PRAGMA"
                        )
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

    }

}
