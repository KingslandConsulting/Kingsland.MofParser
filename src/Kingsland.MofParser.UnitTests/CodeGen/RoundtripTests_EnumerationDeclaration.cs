using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.4 Enumeration declaration

    public static class EnumerationDeclarationTests
    {

        [Test]
        public static void EmptyIntegerEnumerationDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                enumeration MonthsEnum : Integer
                {
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // enumeration MonthsEnum : Integer
                .IdentifierToken("enumeration")
                .WhitespaceToken(" ")
                .IdentifierToken("MonthsEnum")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("Integer")
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
        public static void EmptyStringEnumerationDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                enumeration MonthsEnum : String
                {
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // enumeration MonthsEnum : Integer
                .IdentifierToken("enumeration")
                .WhitespaceToken(" ")
                .IdentifierToken("MonthsEnum")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("String")
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
        public static void EmptyInheritedEnumerationDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                enumeration MonthsEnum : GOLF_MyEnum
                {
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // enumeration MonthsEnum : Integer
                .IdentifierToken("enumeration")
                .WhitespaceToken(" ")
                .IdentifierToken("MonthsEnum")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_MyEnum")
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
        public static void EnumerationDeclarationWithoutValuesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                enumeration MonthsEnum : String
                {
                    January,
                    February,
                    March,
                    April,
                    May,
                    June,
                    July,
                    August,
                    September,
                    October,
                    November,
                    December
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // enumeration MonthsEnum : String
                .IdentifierToken("enumeration")
                .WhitespaceToken(" ")
                .IdentifierToken("MonthsEnum")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("String")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // January,
                .IdentifierToken("January")
                .CommaToken()
                .WhitespaceToken($"{newline}{indent}")
                // February,
                .IdentifierToken("February")
                .CommaToken()
                .WhitespaceToken($"{newline}{indent}")
                // March,
                .IdentifierToken("March")
                .CommaToken()
                .WhitespaceToken($"{newline}{indent}")
                // April,
                .IdentifierToken("April")
                .CommaToken()
                .WhitespaceToken($"{newline}{indent}")
                // May,
                .IdentifierToken("May")
                .CommaToken()
                .WhitespaceToken($"{newline}{indent}")
                // June,
                .IdentifierToken("June")
                .CommaToken()
                .WhitespaceToken($"{newline}{indent}")
                // July,
                .IdentifierToken("July")
                .CommaToken()
                .WhitespaceToken($"{newline}{indent}")
                // August,
                .IdentifierToken("August")
                .CommaToken()
                .WhitespaceToken($"{newline}{indent}")
                // September,
                .IdentifierToken("September")
                .CommaToken()
                .WhitespaceToken($"{newline}{indent}")
                // October,
                .IdentifierToken("October")
                .CommaToken()
                .WhitespaceToken($"{newline}{indent}")
                // November,
                .IdentifierToken("November")
                .CommaToken()
                .WhitespaceToken($"{newline}{indent}")
                // December
                .IdentifierToken("December")
                .WhitespaceToken($"{newline}")
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
            var newline = Environment.NewLine;
            var sourceText = @"
                enumeration MonthsEnum : uint32
                {
                    July = ""July""
                };
            ".TrimIndent(newline).TrimString(newline);
            var errorline = 3;
            var expectedMessage = @$"
                Unexpected token found at Position {43 + (errorline - 1) * newline.Length}, Line Number {errorline}, Column Number 12.
                Token Type: 'StringLiteralToken'
                Token Text: '""July""'
            ".TrimIndent(newline).TrimString(newline);
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
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                enumeration MonthsEnum : uint32
                {
                    July = ""July""
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // enumeration MonthsEnum : Integer
                .IdentifierToken("enumeration")
                .WhitespaceToken(" ")
                .IdentifierToken("MonthsEnum")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("uint32")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // July = "July"
                .IdentifierToken("July")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .StringLiteralToken("July")
                .WhitespaceToken($"{newline}")
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
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                enumeration MonthsEnum : integer
                {
                    [Description(""myDescription"")] January = 1
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // enumeration MonthsEnum : Integer
                .IdentifierToken("enumeration")
                .WhitespaceToken(" ")
                .IdentifierToken("MonthsEnum")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("integer")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
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
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/41")]
        public static void IntegerEnumElementShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                enumeration MonthsEnum : integer
                {
                    January = 1
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // enumeration MonthsEnum : integer
                .IdentifierToken("enumeration")
                .WhitespaceToken(" ")
                .IdentifierToken("MonthsEnum")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("integer")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // January = 1
                .IdentifierToken("January")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void StringEnumElementWithoutValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                enumeration GOLF_StatesEnum : string
                {
                    AL
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // enumeration GOLF_StatesEnum : string
                .IdentifierToken("enumeration")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_StatesEnum")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("string")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // AL
                .IdentifierToken("AL")
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void StringEnumElementWithValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                enumeration GOLF_StatesEnum : string
                {
                    AL = ""Alabama""
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // enumeration GOLF_StatesEnum : string
                .IdentifierToken("enumeration")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_StatesEnum")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("string")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // AL = "Alabama"
                .IdentifierToken("AL")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .StringLiteralToken("Alabama")
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
