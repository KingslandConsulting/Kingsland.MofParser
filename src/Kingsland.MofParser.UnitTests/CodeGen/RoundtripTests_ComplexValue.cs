using Kingsland.MofParser.Tokens;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.9 Complex type value

        public static class ComplexValueTests
        {

            [Test]
            public static void ComplexValuePropertyShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = $MyAliasIdentifier;\r\n" +
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
                    // LastPaymentDate = $MyAliasIdentifier;
                    .IdentifierToken("LastPaymentDate")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .AliasIdentifierToken("MyAliasIdentifier")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void ComplexValuePropertyWithValueOfShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = value of GOLF_Date\r\n" +
                    "\t{\r\n" +
                    "\t\tMonth = July;\r\n" +
                    "\t};\r\n" +
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
                    // LastPaymentDate = value of GOLF_Date
                    .IdentifierToken("LastPaymentDate")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("value")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken("\r\n\t")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t\t")
                    // Month = July;
                    .IdentifierToken("Month")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("July")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // };
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