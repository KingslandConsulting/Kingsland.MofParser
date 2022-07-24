using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Lexing;

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
            var expectedTokens = new TokenBuilder()
                .AliasIdentifierToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(17, 1, 18),
                    "$myAliasIdentifier",
                    "myAliasIdentifier"
                )
                .WhitespaceToken(
                    new SourcePosition(18, 1, 19),
                    new SourcePosition(19, 1, 20),
                    "\r\n"
                )
                .AliasIdentifierToken(
                    new SourcePosition(20, 2, 1),
                    new SourcePosition(38, 2, 19),
                    "$myAliasIdentifier2",
                    "myAliasIdentifier2"
                )
                .ToList();
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
            var expectedTokens = new TokenBuilder()
                .IdentifierToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(7, 1, 8),
                    "instance"
                )
                .WhitespaceToken(
                    new SourcePosition(8, 1, 9),
                    new SourcePosition(8, 1, 9),
                    " "
                )
                .IdentifierToken(
                    new SourcePosition(9, 1, 10),
                    new SourcePosition(10, 1, 11),
                    "of"
                )
                .WhitespaceToken(
                    new SourcePosition(11, 1, 12),
                    new SourcePosition(11, 1, 12),
                    " "
                )
                .IdentifierToken(
                    new SourcePosition(12, 1, 13),
                    new SourcePosition(25, 1, 26),
                    "cTentacleAgent"
                )
                .WhitespaceToken(
                    new SourcePosition(26, 1, 27),
                    new SourcePosition(26, 1, 27),
                    " "
                )
                .IdentifierToken(
                    new SourcePosition(27, 1, 28),
                    new SourcePosition(28, 1, 29),
                    "as"
                )
                .WhitespaceToken(
                    new SourcePosition(29, 1, 30),
                    new SourcePosition(29, 1, 30),
                    " "
                )
                .AliasIdentifierToken(
                    new SourcePosition(30, 1, 31),
                    new SourcePosition(48, 1, 49),
                    "$cTentacleAgent1ref",
                    "cTentacleAgent1ref"
                )
                .WhitespaceToken(
                    new SourcePosition(49, 1, 50),
                    new SourcePosition(50, 1, 51),
                    "\r\n"
                )
                .BlockOpenToken(
                    new SourcePosition(51, 2, 1),
                    new SourcePosition(51, 2, 1),
                    "{"
                )
                .WhitespaceToken(
                    new SourcePosition(52, 2, 2),
                    new SourcePosition(53, 2, 3),
                    "\r\n"
                )
                .BlockCloseToken(
                    new SourcePosition(54, 3, 1),
                    new SourcePosition(54, 3, 1),
                    "}"
                )
                .StatementEndToken(
                    new SourcePosition(55, 3, 2),
                    new SourcePosition(55, 3, 2),
                    ";"
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

}
