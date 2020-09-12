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
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void EmptyStringEnumerationDeclarationShouldRoundtrip()
            {
                var sourceText =
                    "enumeration MonthsEnum : String\r\n" +
                    "{\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void EmptyInheritedEnumerationDeclarationShouldRoundtrip()
            {
                var sourceText =
                    "enumeration MonthsEnum : GOLF_MyEnum\r\n" +
                    "{\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
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
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/52")]
            public static void EnumerationDeclarationDeprecatedMof300IntegerBaseAndQuirksEnabledShouldThrow()
            {
                // this should throw because "uint32" is recognized as an integer type.
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
                RoundtripTests.AssertRoundtripException(sourceText, expectedMessage);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/52")]
            public static void EnumerationDeclarationDeprecatedMof300IntegerBaseAndQuirksDisabledShouldRoundtrip()
            {
                // this should roundtrip because "uint32" is not recognized as an integer type, and
                // so it's assumed to be a separate base enum like "enumeration uint32 { ... };".
                // as a result, there's no validation done on the datattype of the enum element and
                // it will accept "July" as a valid value
                var sourceText =
                    "enumeration MonthsEnum : uint32\r\n" +
                    "{\r\n" +
                    "\tJuly = \"July\"\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
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
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/41")]
            public static void IntegerEnumElementShouldRoundtrip()
            {
                var sourceText =
                    "enumeration MonthsEnum : integer\r\n" +
                    "{\r\n" +
                    "\tJanuary = 1\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void StringEnumElementWithoutValueShouldRoundtrip()
            {
                var sourceText =
                    "enumeration GOLF_StatesEnum : string\r\n" +
                    "{\r\n" +
                    "\tAL\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void StringEnumElementWithValueShouldRoundtrip()
            {
                var sourceText =
                    "enumeration GOLF_StatesEnum : string\r\n" +
                    "{\r\n" +
                    "\tAL = \"Alabama\"\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

        }

        #endregion

    }

}