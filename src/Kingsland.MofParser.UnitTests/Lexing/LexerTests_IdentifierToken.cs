using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Lexing;

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
            var expectedTokens = new TokenBuilder()
                .IdentifierToken(
                    new SourcePosition(0, 1, 1),
                    new SourcePosition(11, 1, 12),
                    "myIdentifier"
                )
                .WhitespaceToken(
                    new SourcePosition(12, 1, 13),
                    new SourcePosition(13, 1, 14),
                    "\r\n"
                )
                .IdentifierToken(
                    new SourcePosition(14, 2, 1),
                    new SourcePosition(26, 2, 13),
                    "myIdentifier2"
                )
                .ToList();
            LexerTests.AssertLexerTest(sourceText, expectedTokens);
        }

    }

}
