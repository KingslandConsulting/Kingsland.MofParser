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
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = July;\r\n" +
                    "\tDay = 31;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void EnumTypeValueWithEnumValueArrayShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = {June};\r\n" +
                    "\tDay = 31;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
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
                    "\tYear = 2011;\r\n" +
                    "\tMonth = July;\r\n" +
                    "\tDay = 31;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void QualifiedEnumValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = MonthEnums.July;\r\n" +
                    "\tDay = 31;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
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
                    "\tYear = 2011;\r\n" +
                    "\tMonth = {June};\r\n" +
                    "\tDay = 31;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void EnumValueArrayWithSingleEnumValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = {June};\r\n" +
                    "\tDay = 31;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            public static void EnumValueArrayWithMultipleEnumValuesShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {January, February};\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
            public static void EnumValueArrayWithQualifiedEnumValuesAndQuirksEnabledShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {MonthEnums.July};\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(
                    sourceText,
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
                var tokens = Lexer.Lex(SourceReader.From(sourceText));
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