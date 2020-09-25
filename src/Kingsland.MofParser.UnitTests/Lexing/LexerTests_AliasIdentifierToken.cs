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
        public static class ReadAliasIdentifierTokenMethod
        {

            [Test]
            public static void ShouldReadAliasIdentifierToken()
            {
                var sourceText =
                    "$myAliasIdentifier\r\n" +
                    "$myAliasIdentifier2";
                var expectedTokens = new List<SyntaxToken> {
                    new AliasIdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(17, 1, 18),
                            "$myAliasIdentifier"
                        ),
                        "myAliasIdentifier"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(18, 1, 19),
                            new SourcePosition(19, 1, 20),
                            "\r\n"
                        ),
                        "\r\n"
                    ),
                    new AliasIdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(20, 2, 1),
                            new SourcePosition(38, 2, 19),
                            "$myAliasIdentifier2"
                        ),
                        "myAliasIdentifier2"
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadInstanceWithAliasIdentifier()
            {
                // test case for https://github.com/mikeclayton/MofParser/issues/4
                var sourceText =
                    "instance of cTentacleAgent as $cTentacleAgent1ref\r\n" +
                    "{\r\n" +
                    "};";
                var expectedTokens = new List<SyntaxToken>
                {
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(7, 1, 8),
                            "instance"
                        ),
                        "instance"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(8, 1, 9),
                            new SourcePosition(8, 1, 9),
                            " "
                        ),
                        " "
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(9, 1, 10),
                            new SourcePosition(10, 1, 11),
                            "of"
                        ),
                        "of"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(11, 1, 12),
                            new SourcePosition(11, 1, 12),
                            " "
                        ),
                        " "
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(12, 1, 13),
                            new SourcePosition(25, 1, 26),
                            "cTentacleAgent"
                        ),
                        "cTentacleAgent"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(26, 1, 27),
                            new SourcePosition(26, 1, 27),
                            " "
                        ),
                        " "
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(27, 1, 28),
                            new SourcePosition(28, 1, 29),
                            "as"
                        ),
                        "as"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(29, 1, 30),
                            new SourcePosition(29, 1, 30),
                            " "
                        ),
                        " "
                    ),
                    new AliasIdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(30, 1, 31),
                            new SourcePosition(48, 1, 49),
                            "$cTentacleAgent1ref"
                        ),
                        "cTentacleAgent1ref"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(49, 1, 50),
                            new SourcePosition(50, 1, 51),
                            "\r\n"
                        ),
                        "\r\n"
                    ),
                    new BlockOpenToken(
                        new SourceExtent
                        (
                            new SourcePosition(51, 2, 1),
                            new SourcePosition(51, 2, 1),
                            "{"
                        )
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(52, 2, 2),
                            new SourcePosition(53, 2, 3),
                            "\r\n"
                        ),
                        "\r\n"
                    ),
                    new BlockCloseToken(
                        new SourceExtent
                        (
                            new SourcePosition(54, 3, 1),
                            new SourcePosition(54, 3, 1),
                            "}"
                        )
                    ),
                    new StatementEndToken(
                        new SourceExtent
                        (
                            new SourcePosition(55, 3, 2),
                            new SourcePosition(55, 3, 2),
                            ";"
                        )
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

        }

    }

}
