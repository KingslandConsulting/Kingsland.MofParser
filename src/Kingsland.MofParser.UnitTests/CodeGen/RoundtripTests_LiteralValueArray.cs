using Kingsland.MofParser.Tokens;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.1 Primitive type value

        public static class LiteralValueArrayTests
        {

            [Test]
            public static void LiteralValueArrayWithOneItemShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {1};\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // instance of GOLF_ClubMember
                   .IdentifierToken("instance")
                   .WhitespaceToken(" ")
                   .IdentifierToken("of")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_ClubMember")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // LastPaymentDate = {1};
                   .IdentifierToken("LastPaymentDate")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .BlockOpenToken()
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .BlockCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void LiteralValueArrayWithMultipleItemsShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {1, 2};\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // instance of GOLF_ClubMember
                   .IdentifierToken("instance")
                   .WhitespaceToken(" ")
                   .IdentifierToken("of")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_ClubMember")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // LastPaymentDate = {1, 2};
                   .IdentifierToken("LastPaymentDate")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .BlockOpenToken()
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .CommaToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 2)
                   .BlockCloseToken()
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