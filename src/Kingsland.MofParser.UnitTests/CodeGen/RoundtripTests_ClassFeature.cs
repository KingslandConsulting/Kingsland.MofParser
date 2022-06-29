using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.2 Class declaration

    public static class ClassFeatureTests
    {

        [Test]
        public static void ClassFeatureWithQualifiersShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class Sponsor
                    {
                        [Description(""Monthly salary in $US"")] string Name;
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // class Sponsor
                .IdentifierToken("class")
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
        public static void InvalidClassFeatureShouldThrow()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                    class Sponsor
                    {
                        100
                    };
                ".TrimIndent(newline).TrimString(newline);
            var errorline = 3;
            var expectedMessage = @$"
                    Unexpected token found at Position {18 + (errorline - 1) * newline.Length}, Line Number {errorline}, Column Number 5.
                    Token Type: 'IntegerLiteralToken'
                    Token Text: '100'
                ".TrimIndent(newline).TrimString(newline);
            RoundtripTests.AssertRoundtripException(sourceText, expectedMessage);
        }

        [Test]
        public static void ClassFeatureWithStructureDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class Sponsor
                    {
                        structure Nested
                        {
                        };
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // class Sponsor
                .IdentifierToken("class")
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
        public static void ClassFeatureWithEnumerationDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class Sponsor
                    {
                        enumeration MonthsEnum : Integer
                        {
                        };
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // class Sponsor
                .IdentifierToken("class")
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
        public static void ClassFeatureWithPropertyDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class Sponsor
                    {
                        string Name;
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // class Sponsor
                .IdentifierToken("class")
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
