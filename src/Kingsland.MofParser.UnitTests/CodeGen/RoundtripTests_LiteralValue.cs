using Kingsland.MofParser.Tokens;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.1 Primitive type value

        public static class LiteralValueTests
        {

            [Test]
            public static void IntegerLiteralValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = 1;\r\n" +
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
                    // LastPaymentDate = 1;
                    .IdentifierToken("LastPaymentDate")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void RealLiteralValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = 0.5;\r\n" +
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
                    // LastPaymentDate = 0.5;
                    .IdentifierToken("LastPaymentDate")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .RealLiteralToken(0.5)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void BooleanLiteralValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = true;\r\n" +
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
                   // LastPaymentDate = true;
                   .IdentifierToken("LastPaymentDate")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .BooleanLiteralToken(true)
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void NullLiteralValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = null;\r\n" +
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
                   // LastPaymentDate = null;
                   .IdentifierToken("LastPaymentDate")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .NullLiteralToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void StringLiteralValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = \"aaa\";\r\n" +
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
                   // LastPaymentDate = "aaa";
                   .IdentifierToken("LastPaymentDate")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .StringLiteralToken("aaa")
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