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
        public static class ReadIdentifierTokenMethod
        {

            [Test]
            public static void ShouldReadIdentifierToken()
            {
                var sourceText =
                    "myIdentifier\r\n" +
                    "myIdentifier2";
                var expectedTokens = new List<SyntaxToken> {
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(11, 1, 12),
                            "myIdentifier"
                        ),
                        "myIdentifier"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(12, 1, 13),
                            new SourcePosition(13, 1, 14),
                            "\r\n"
                        ),
                        "\r\n"
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(14, 2, 1),
                            new SourcePosition(26, 2, 13),
                            "myIdentifier2"
                        ),
                        "myIdentifier2"
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

        }

    }

}
