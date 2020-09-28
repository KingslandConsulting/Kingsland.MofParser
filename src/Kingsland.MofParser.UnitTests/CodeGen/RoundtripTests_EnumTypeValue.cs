using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.3 Enum type value

        public static class EnumTypeValueTests
        {

            [Test]
            public static void EnumTypeValueWithEnumValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = July;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_Date
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Month = July;
                    .IdentifierToken("Month")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("July")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void EnumTypeValueWithEnumValueArrayShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {June};\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_Date
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Month = {June};
                    .IdentifierToken("Month")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .BlockOpenToken()
                    .IdentifierToken("June")
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

        public static class EnumValueTests
        {

            [Test]
            public static void UnqalifiedEnumValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = July;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_Date
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Month = July;
                    .IdentifierToken("Month")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("July")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void QualifiedEnumValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = MonthEnums.July;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_Date
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Month = MonthEnums.July;
                    .IdentifierToken("Month")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("MonthEnums")
                    .DotOperatorToken()
                    .IdentifierToken("July")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
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
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {};\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_Date
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Month = {};
                    .IdentifierToken("Month")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .BlockOpenToken()
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
            public static void EnumValueArrayWithSingleEnumValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {June};\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_Date
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Month = {June};
                    .IdentifierToken("Month")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .BlockOpenToken()
                    .IdentifierToken("June")
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
            public static void EnumValueArrayWithMultipleEnumValuesShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {January, February};\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_Date
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
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
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
            public static void EnumValueArrayWithQualifiedEnumValuesAndQuirksEnabledShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {MonthEnums.July};\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_Date
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
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
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(
                    sourceText,
                    expectedTokens,
                    ParserQuirks.EnumValueArrayContainsEnumValuesNotEnumNames
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
            public static void EnumValueArrayWithQualifiedEnumValuesAndQuirksDisabledShouldThrow()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {MonthEnums.July};\r\n" +
                    "};";
                var expectedMessage =
                    "Unexpected token found at Position 46, Line Number 3, Column Number 21.\r\n" +
                    "Token Type: 'DotOperatorToken'\r\n" +
                    "Token Text: '.'";
                RoundtripTests.AssertRoundtripException(sourceText, expectedMessage);
            }

        }

        #endregion

    }

}