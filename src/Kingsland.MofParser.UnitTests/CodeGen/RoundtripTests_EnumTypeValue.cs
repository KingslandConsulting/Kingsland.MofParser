using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Parsing;
using Kingsland.ParseFx.Text;
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
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = July;\r\n" +
                    "\tDay = 31;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void EnumTypeValueWithEnumValueArrayShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = {June};\r\n" +
                    "\tDay = 31;\r\n" +
                    "};"
                );
            }

        }

        public static class EnumValueTests
        {

            [Test]
            public static void UnqalifiedEnumValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = July;\r\n" +
                    "\tDay = 31;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void QualifiedEnumValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = MonthEnums.July;\r\n" +
                    "\tDay = 31;\r\n" +
                    "};"
                );
            }

        }

        public static class EnumValueArrayTests
        {

            [Test]
            public static void EmptyEnumValueArrayShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = {June};\r\n" +
                    "\tDay = 31;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void EnumValueArrayWithSingleEnumValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = {June};\r\n" +
                    "\tDay = 31;\r\n" +
                    "};"
                );
            }

            public static void EnumValueArrayWithMultipleEnumValuesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {January, February};\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
            public static void EnumValueArrayWithQualifiedEnumValuesAndQuirksEnabledShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {MonthEnums.July};\r\n" +
                    "};",
                    ParserQuirks.EnumValueArrayContainsEnumValuesNotEnumNames
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
            public static void EnumValueArrayWithQualifiedEnumValuesAndQuirksDisabledShouldThrow()
            {
                var sourceMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {MonthEnums.July};\r\n" +
                    "};";
                var tokens = Lexer.Lex(SourceReader.From(sourceMof));
                var tokensMof = TokenMofGenerator.ConvertToMof(tokens);
                var ex = Assert.Throws<UnexpectedTokenException>(
                    () =>
                    {
                        var astNodes = Parser.Parse(tokens);
                    }
                );
                Assert.AreEqual(
                    "Unexpected token found at Position 46, Line Number 3, Column Number 21.\r\n" +
                    "Token Type: 'DotOperatorToken'\r\n" +
                    "Token Text: '.'",
                    ex.Message
                );
            }

        }

        #endregion

    }

}