using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.6.3 Enum type value

    public static class EnumTypeValueTests
    {

        [Test]
        public static void EnumTypeValueWithEnumValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    instance of GOLF_Date
                    {
                        Month = July;
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // Month = July;
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IdentifierToken("July")
                .StatementEndToken()
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void EnumTypeValueWithEnumValueArrayShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    instance of GOLF_Date
                    {
                        Month = {June};
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // Month = {June};
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .IdentifierToken("June")
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

    public static class EnumValueTests
    {

        [Test]
        public static void UnqalifiedEnumValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    instance of GOLF_Date
                    {
                        Month = July;
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // Month = July;
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IdentifierToken("July")
                .StatementEndToken()
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void QualifiedEnumValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    instance of GOLF_Date
                    {
                        Month = MonthEnums.July;
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // Month = MonthEnums.July;
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IdentifierToken("MonthEnums")
                .DotOperatorToken()
                .IdentifierToken("July")
                .StatementEndToken()
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

    }

    public static class EnumValueArrayTests
    {

        [Test]
        public static void EmptyEnumValueArrayShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    instance of GOLF_Date
                    {
                        Month = {};
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // Month = {};
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
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
        public static void EnumValueArrayWithSingleEnumValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    instance of GOLF_Date
                    {
                        Month = {June};
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // Month = {June};
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .IdentifierToken("June")
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
        public static void EnumValueArrayWithMultipleEnumValuesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    instance of GOLF_Date
                    {
                        Month = {January, February};
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // Month = {January, February};
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .IdentifierToken("January")
                .CommaToken()
                .WhitespaceToken(" ")
                .IdentifierToken("February")
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
        public static void EnumValueArrayWithQualifiedEnumValuesAndQuirksEnabledShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    instance of GOLF_Date
                    {
                        Month = {MonthEnums.July};
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_Date
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // Month = {MonthEnums.July};
                .IdentifierToken("Month")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .IdentifierToken("MonthEnums")
                .DotOperatorToken()
                .IdentifierToken("July")
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(
                sourceText,
                expectedTokens,
                null,
                ParserQuirks.EnumValueArrayContainsEnumValuesNotEnumNames
            );
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
        public static void EnumValueArrayWithQualifiedEnumValuesAndQuirksDisabledShouldThrow()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                    instance of GOLF_Date
                    {
                        Month = {MonthEnums.July};
                    };
                ".TrimIndent(newline).TrimString(newline);
            var errorline = 3;
            var expectedMessage = @$"
                    Unexpected token found at Position {45 + (errorline - 1) * newline.Length}, Line Number {errorline}, Column Number 24.
                    Token Type: 'DotOperatorToken'
                    Token Text: '.'
                ".TrimIndent(newline).TrimString(newline);
            RoundtripTests.AssertRoundtripException(sourceText, expectedMessage);
        }

    }

    #endregion

}
