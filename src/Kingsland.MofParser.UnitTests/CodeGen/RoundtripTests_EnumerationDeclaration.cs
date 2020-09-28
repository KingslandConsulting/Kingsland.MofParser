using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.4 Enumeration declaration

        public static class EnumerationDeclarationTests
        {

            [Test]
            public static void EmptyIntegerEnumerationDeclarationShouldRoundtrip()
            {
                var sourceText =
                    "enumeration MonthsEnum : Integer\r\n" +
                    "{\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // enumeration MonthsEnum : Integer
                    .IdentifierToken("enumeration")
                    .WhitespaceToken(" ")
                    .IdentifierToken("MonthsEnum")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("Integer")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void EmptyStringEnumerationDeclarationShouldRoundtrip()
            {
                var sourceText =
                    "enumeration MonthsEnum : String\r\n" +
                    "{\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // enumeration MonthsEnum : Integer
                    .IdentifierToken("enumeration")
                    .WhitespaceToken(" ")
                    .IdentifierToken("MonthsEnum")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("String")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void EmptyInheritedEnumerationDeclarationShouldRoundtrip()
            {
                var sourceText =
                    "enumeration MonthsEnum : GOLF_MyEnum\r\n" +
                    "{\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // enumeration MonthsEnum : Integer
                    .IdentifierToken("enumeration")
                    .WhitespaceToken(" ")
                    .IdentifierToken("MonthsEnum")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_MyEnum")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void EnumerationDeclarationWithoutValuesShouldRoundtrip()
            {
                var sourceText =
                    "enumeration MonthsEnum : String\r\n" +
                    "{\r\n" +
                    "\tJanuary,\r\n" +
                    "\tFebruary,\r\n" +
                    "\tMarch,\r\n" +
                    "\tApril,\r\n" +
                    "\tMay,\r\n" +
                    "\tJune,\r\n" +
                    "\tJuly,\r\n" +
                    "\tAugust,\r\n" +
                    "\tSeptember,\r\n" +
                    "\tOctober,\r\n" +
                    "\tNovember,\r\n" +
                    "\tDecember\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // enumeration MonthsEnum : String
                    .IdentifierToken("enumeration")
                    .WhitespaceToken(" ")
                    .IdentifierToken("MonthsEnum")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("String")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // January,
                    .IdentifierToken("January")
                    .CommaToken()
                    .WhitespaceToken("\r\n\t")
                    // February,
                    .IdentifierToken("February")
                    .CommaToken()
                    .WhitespaceToken("\r\n\t")
                    // March,
                    .IdentifierToken("March")
                    .CommaToken()
                    .WhitespaceToken("\r\n\t")
                    // April,
                    .IdentifierToken("April")
                    .CommaToken()
                    .WhitespaceToken("\r\n\t")
                    // May,
                    .IdentifierToken("May")
                    .CommaToken()
                    .WhitespaceToken("\r\n\t")
                    // June,
                    .IdentifierToken("June")
                    .CommaToken()
                    .WhitespaceToken("\r\n\t")
                    // July,
                    .IdentifierToken("July")
                    .CommaToken()
                    .WhitespaceToken("\r\n\t")
                    // August,
                    .IdentifierToken("August")
                    .CommaToken()
                    .WhitespaceToken("\r\n\t")
                    // September,
                    .IdentifierToken("September")
                    .CommaToken()
                    .WhitespaceToken("\r\n\t")
                    // October,
                    .IdentifierToken("October")
                    .CommaToken()
                    .WhitespaceToken("\r\n\t")
                    // November,
                    .IdentifierToken("November")
                    .CommaToken()
                    .WhitespaceToken("\r\n\t")
                    // December
                    .IdentifierToken("December")
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/52")]
            public static void EnumerationDeclarationDeprecatedMof300IntegerBaseAndQuirksEnabledShouldThrow()
            {
                // this should throw because "uint32" is recognized as an integer type when quirks are enabled.
                // as a result, "July" (a string) is not a valid value for an integer enumElement value
                var sourceText =
                    "enumeration MonthsEnum : uint32\r\n" +
                    "{\r\n" +
                    "\tJuly = \"July\"\r\n" +
                    "};";
                var expectedMessage =
                    "Unexpected token found at Position 44, Line Number 3, Column Number 9.\r\n" +
                    "Token Type: 'StringLiteralToken'\r\n" +
                    "Token Text: '\"July\"'";
                RoundtripTests.AssertRoundtripException(
                    sourceText, expectedMessage,
                    ParserQuirks.AllowDeprecatedMof300IntegerTypesAsEnumerationDeclarationsBase
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/52")]
            public static void EnumerationDeclarationDeprecatedMof300IntegerBaseAndQuirksDisabledShouldRoundtrip()
            {
                // this should roundtrip because "uint32" is not recognized as an integer type, and
                // so it's assumed to be a separate base enum like "enumeration uint32 { ... };".
                // as a result, there's no validation done on the datatype of the enum element and
                // it will accept "July" as a valid value
                var sourceText =
                    "enumeration MonthsEnum : uint32\r\n" +
                    "{\r\n" +
                    "\tJuly = \"July\"\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // enumeration MonthsEnum : Integer
                    .IdentifierToken("enumeration")
                    .WhitespaceToken(" ")
                    .IdentifierToken("MonthsEnum")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("uint32")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // July = "July"
                    .IdentifierToken("July")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .StringLiteralToken("July")
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

        }

        public static class EnumElementTests
        {

            [Test]
            public static void EnumElementWithQualifiersShouldRoundtrip()
            {
                var sourceText =
                    "enumeration MonthsEnum : integer\r\n" +
                    "{\r\n" +
                    "\t[Description(\"myDescription\")] January = 1\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // enumeration MonthsEnum : Integer
                    .IdentifierToken("enumeration")
                    .WhitespaceToken(" ")
                    .IdentifierToken("MonthsEnum")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("integer")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // [Description("myDescription")] January = 1
                    .AttributeOpenToken()
                    .IdentifierToken("Description")
                    .ParenthesisOpenToken()
                    .StringLiteralToken("myDescription")
                    .ParenthesisCloseToken()
                    .AttributeCloseToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("January")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/41")]
            public static void IntegerEnumElementShouldRoundtrip()
            {
                var sourceText =
                    "enumeration MonthsEnum : integer\r\n" +
                    "{\r\n" +
                    "\tJanuary = 1\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // enumeration MonthsEnum : integer
                    .IdentifierToken("enumeration")
                    .WhitespaceToken(" ")
                    .IdentifierToken("MonthsEnum")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("integer")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // January = 1
                    .IdentifierToken("January")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void StringEnumElementWithoutValueShouldRoundtrip()
            {
                var sourceText =
                    "enumeration GOLF_StatesEnum : string\r\n" +
                    "{\r\n" +
                    "\tAL\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // enumeration GOLF_StatesEnum : string
                    .IdentifierToken("enumeration")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_StatesEnum")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("string")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // AL
                    .IdentifierToken("AL")
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void StringEnumElementWithValueShouldRoundtrip()
            {
                var sourceText =
                    "enumeration GOLF_StatesEnum : string\r\n" +
                    "{\r\n" +
                    "\tAL = \"Alabama\"\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // enumeration GOLF_StatesEnum : string
                    .IdentifierToken("enumeration")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_StatesEnum")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("string")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // AL = "Alabama"
                    .IdentifierToken("AL")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .StringLiteralToken("Alabama")
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