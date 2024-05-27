using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.6.2 Complex type value

    public static class InstanceValueDeclarationTests
    {

        [Test]
        public static void InstanceValueDeclarationWithNoPropertiesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_ClubMember
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst([
                new InstanceValueDeclarationAst(
                    new("instance"), new("of"), new("GOLF_ClubMember"),
                    new([]),
                    new()
                )
            ]);
            var expectedModule = new Module([
                new(
                    "GOLF_ClubMember",
                    []
                )
            ]);
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void InstanceValueDeclarationWithChildPropertiesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    FirstName = ""John"";
                    LastName = ""Doe"";
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_ClubMember
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     FirstName = "John";
                .IdentifierToken("FirstName")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .StringLiteralToken("John")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     LastName = "Doe";
                .IdentifierToken("LastName")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .StringLiteralToken("Doe")
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst([
                new InstanceValueDeclarationAst(
                    new("instance"), new("of"), new("GOLF_ClubMember"),
                    new([
                        new(new("FirstName"), new StringValueAst(new StringLiteralToken("John"), "John")),
                        new(new("LastName"), new StringValueAst(new StringLiteralToken("Doe"), "Doe"))
                    ]),
                    new()
                )
            ]);
            var expectedModule = new Module([
                new(
                    "GOLF_ClubMember",
                    [
                        new("FirstName", "John"),
                        new("LastName", "Doe")
                    ]
                )
            ]);
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        [Test]
        public static void InstanceValueDeclarationWithAliasShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                instance of GOLF_ClubMember as $MyAliasIdentifier
                {
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_ClubMember as $MyAliasIdentifier
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(" ")
                .IdentifierToken("as")
                .WhitespaceToken(" ")
                .AliasIdentifierToken("MyAliasIdentifier")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst([
                new InstanceValueDeclarationAst(
                    new("instance"), new("of"), new("GOLF_ClubMember"), new("as"), new("MyAliasIdentifier"),
                    new([]),
                    new()
                )
            ]);
            var expectedModule = new Module([
                new(
                    "GOLF_ClubMember", "MyAliasIdentifier",
                    []
                )
            ]);
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModule);
        }

        //[Test]
        //public static void InstanceValueDeclarationShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "instance of GOLF_ClubMember\r\n" +
        //        "{\r\n" +
        //        "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
        //        "};"
        //    );
        //}

        //[Test(Description = "https://github.com/mikeclayton/MofParser/issues/26"),
        // Ignore("https://github.com/mikeclayton/MofParser/issues/26")]
        //public static void InstanceValueDeclarationsWithInstanceValuePropertyShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "instance of GOLF_ClubMember\r\n" +
        //        "{\r\n" +
        //        "\tLastPaymentDate = instance of GOLF_Date\r\n" +
        //        "\t{\r\n" +
        //        "\tYear = 2011;\r\n" +
        //        "\tMonth = July;\r\n" +
        //        "\tDay = 31;\r\n" +
        //        "\t};\r\n" +
        //        "}";
        //    );
        //}

        //[Test]
        //public static void InstanceValueDeclarationWithStructureValueDeclarationPropertyShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "instance of GOLF_ClubMember\r\n" +
        //        "{\r\n" +
        //        "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
        //        "\tMemberAddress = value of GOLF_Address\r\n" +
        //        "\t{\r\n" +
        //        "\t\tState = \"IL\";\r\n" +
        //        "\t\tCity = \"Oak Park\";\r\n" +
        //        "\t\tStreet = \"Oak Park Av.\";\r\n" +
        //        "\t\tStreetNo = \"1177\";\r\n" +
        //        "\t\tApartmentNo = \"3B\";\r\n" +
        //        "\t};\r\n" +
        //        "};";
        //    );
        //}

    }

    #endregion

}
