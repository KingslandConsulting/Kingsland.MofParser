using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.6.2 Complex type value

    public static class StructureValueDeclarationTests
    {

        [Test]
        public static void StructureValueDeclarationWithNoPropertiesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                value of GOLF_ClubMember as $MyAliasIdentifier
                {
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // value of GOLF_ClubMember as $MyAliasIdentifier
                .IdentifierToken("value")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(" ")
                .IdentifierToken("as")
                .WhitespaceToken(" ")
                .AliasIdentifierToken("MyAliasIdentifier")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void StructureValueDeclarationWithChildPropertiesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                value of GOLF_ClubMember as $MyAliasIdentifier
                {
                    FirstName = ""John"";
                    LastName = ""Doe"";
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // value of GOLF_ClubMember as $MyAliasIdentifier
               .IdentifierToken("value")
               .WhitespaceToken(" ")
               .IdentifierToken("of")
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_ClubMember")
               .WhitespaceToken(" ")
               .IdentifierToken("as")
               .WhitespaceToken(" ")
               .AliasIdentifierToken("MyAliasIdentifier")
               .WhitespaceToken($"{newline}")
               // {
               .BlockOpenToken()
               .WhitespaceToken($"{newline}{indent}")
               // FirstName = "John"
               .IdentifierToken("FirstName")
               .WhitespaceToken(" ")
               .EqualsOperatorToken()
               .WhitespaceToken(" ")
               .StringLiteralToken("John")
               .StatementEndToken()
               .WhitespaceToken($"{newline}{indent}")
               // LastName = "Doe"
               .IdentifierToken("LastName")
               .WhitespaceToken(" ")
               .EqualsOperatorToken()
               .WhitespaceToken(" ")
               .StringLiteralToken("Doe")
               .StatementEndToken()
               .WhitespaceToken($"{newline}")
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        //[Test]
        //public static void StructureValueDeclarationShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "value of GOLF_PhoneNumber as $JohnDoesPhoneNo\r\n" +
        //        "{\r\n" +
        //        "\tAreaCode = {\"9\", \"0\", \"7\"};\r\n" +
        //        "\tNumber = {\"7\", \"4\", \"7\", \"4\", \"8\", \"8\", \"4\"};\r\n" +
        //        "};";
        //    );
        //}

    }

    #endregion

}
