using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.1.5 Boolean value

        public static class BooleanValueTests
        {

            [Test]
            public static void BooleanValueAstShouldRoundtrip()
            {
                var sourceText =
                    "instance of myType as $Alias00000070\r\n" +
                    "{\r\n" +
                    "\tReference = TRUE;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of myType as $Alias00000070
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("myType")
                    .WhitespaceToken(" ")
                    .IdentifierToken("as")
                    .WhitespaceToken(" ")
                    .AliasIdentifierToken("Alias00000070")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Reference = TRUE;
                    .IdentifierToken("Reference")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .BooleanLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "TRUE", true
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

        }

        #endregion

    }

}