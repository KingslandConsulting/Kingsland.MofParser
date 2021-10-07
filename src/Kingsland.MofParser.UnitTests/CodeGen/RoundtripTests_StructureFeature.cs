using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;
using System;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.1 Structure declaration

        public static class StructureFeatureTests
        {

            [Test]
            public static void StructureFeatureWithQualifierShouldRoundtrip()
            {
                var newline = Environment.NewLine;
                var indent = "    ";
                var sourceText = @"
                    structure Sponsor
                    {
                        [Description(""Monthly salary in $US"")] string Name;
                    };
                ".TrimIndent(newline).TrimString(newline);
                var expectedTokens = new TokenBuilder()
                    // structure Sponsor
                    .IdentifierToken("structure")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Sponsor")
                    .WhitespaceToken($"{newline}")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken($"{newline}{indent}")
                    // [Description("Monthly salary in $US")] string Name;
                    .AttributeOpenToken()
                    .IdentifierToken("Description")
                    .ParenthesisOpenToken()
                    .StringLiteralToken("Monthly salary in $US")
                    .ParenthesisCloseToken()
                    .AttributeCloseToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("string")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Name")
                    .StatementEndToken()
                    .WhitespaceToken($"{newline}")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void InvalidStructureFeatureShouldThrow()
            {
                var newline = Environment.NewLine;
                var sourceText = @"
                    structure Sponsor
                    {
                        100
                    };
                ".TrimIndent(newline).TrimString(newline);
                var errorline = 3;
                var expectedMessage = @$"
                    Unexpected token found at Position {22 + (errorline - 1) * newline.Length}, Line Number {errorline}, Column Number 5.
                    Token Type: 'IntegerLiteralToken'
                    Token Text: '100'
                ".TrimIndent(newline).TrimString(newline);
                RoundtripTests.AssertRoundtripException(sourceText, expectedMessage);
            }

            [Test]
            public static void StructureFeatureWithStructureDeclarationShouldRoundtrip()
            {
                var newline = Environment.NewLine;
                var indent = "    ";
                var sourceText = @"
                    structure Sponsor
                    {
                        structure Nested
                        {
                        };
                    };
                ".TrimIndent(newline).TrimString(newline);
                var expectedTokens = new TokenBuilder()
                    // structure Sponsor
                    .IdentifierToken("structure")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Sponsor")
                    .WhitespaceToken($"{newline}")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken($"{newline}{indent}")
                    // structure Nested
                    .IdentifierToken("structure")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Nested")
                    .WhitespaceToken($"{newline}{indent}")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken($"{newline}{indent}")
                    // };
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
            public static void StructureFeatureWithEnumerationDeclarationShouldRoundtrip()
            {
                var newline = Environment.NewLine;
                var indent = "    ";
                var sourceText = @"
                    structure Sponsor
                    {
                        enumeration MonthsEnum : Integer
                        {
                        };
                    };
                ".TrimIndent(newline).TrimString(newline);
                var expectedTokens = new TokenBuilder()
                    // structure Sponsor
                    .IdentifierToken("structure")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Sponsor")
                    .WhitespaceToken($"{newline}")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken($"{newline}{indent}")
                    // enumeration MonthsEnum : Integer
                    .IdentifierToken("enumeration")
                    .WhitespaceToken(" ")
                    .IdentifierToken("MonthsEnum")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("Integer")
                    .WhitespaceToken($"{newline}{indent}")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken($"{newline}{indent}")
                    // };
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
            public static void StructureFeatureWithPropertyDeclarationShouldRoundtrip()
            {
                var newline = Environment.NewLine;
                var indent = "    ";
                var sourceText = @"
                    structure Sponsor
                    {
                        string Name;
                    };
                ".TrimIndent(newline).TrimString(newline);
                var expectedTokens = new TokenBuilder()
                    // structure Sponsor
                    .IdentifierToken("structure")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Sponsor")
                    .WhitespaceToken($"{newline}")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken($"{newline}{indent}")
                    // string Name;
                    .IdentifierToken("string")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Name")
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

}