using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.9 Complex type value

    public static class ComplexValueArrayTests
    {

        [Test]
        public static void ComplexValueArrayWithOneItemShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = {$MyAliasIdentifier};
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_ClubMember
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // LastPaymentDate = {$MyAliasIdentifier};
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .AliasIdentifierToken("MyAliasIdentifier")
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void ComplexValueArrayWithMultipleItemsShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = {$MyAliasIdentifier, $MyOtherAliasIdentifier};
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_ClubMember
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // LastPaymentDate = {$MyAliasIdentifier, $MyOtherAliasIdentifier};
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .AliasIdentifierToken("MyAliasIdentifier")
                .CommaToken()
                .WhitespaceToken(" ")
                .AliasIdentifierToken("MyOtherAliasIdentifier")
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

    }

    #endregion

}
