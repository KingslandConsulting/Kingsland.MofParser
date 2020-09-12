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

        #region 7.5.1 Structure declaration

        public static class StructureDeclarationTests
        {

            [Test]
            public static void EmptyStructureDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

            [Test]
            public static void StructureDeclarationWithSuperstructureShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "structure Sponsor : GOLF_MySupestructure\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

            [Test]
            public static void StructureDeclarationWithStructureFeaturesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\tstring Name;\r\n" +
                    "\tGOLF_Date ContractSignedDate;\r\n" +
                    "\treal32 ContractAmount;\r\n" +
                    "};"
                );
            }

        }

        public static class StructureFeatureTests
        {

            [Test]
            public static void StructureFeatureWithQualifierShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\t[Description(\"Monthly salary in $US\")] string Name;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void InvalidStructureFeatureShouldThrow()
            {

                var sourceMof =
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\t100\r\n" +
                    "};";
                var tokens = Lexer.Lex(SourceReader.From(sourceMof));
                var tokensMof = TokenMofGenerator.ConvertToMof(tokens);
                Assert.AreEqual(sourceMof, tokensMof);
                var ex = Assert.Throws<UnexpectedTokenException>(
                    () => {
                        var astNodes = Parser.Parse(tokens);
                    }
                );
                Assert.AreEqual(
                    "Unexpected token found at Position 23, Line Number 3, Column Number 2.\r\n" +
                    "Token Type: 'IntegerLiteralToken'\r\n" +
                    "Token Text: '100'",
                    ex.Message
                );
            }

            [Test]
            public static void StructureFeatureWithStructureDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\tstructure Nested\r\n" +
                    "\t{\r\n" +
                    "\t};\r\n" +
                    "};"
                );
            }

            [Test]
            public static void StructureFeatureWithEnumerationDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\tenumeration MonthsEnum : Integer\r\n" +
                    "\t{\r\n" +
                    "\t};\r\n" +
                    "};"
                );
            }

            [Test]
            public static void StructureFeatureWithPropertyDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\tstring Name;\r\n" +
                    "};"
                );
            }

        }

        #endregion

    }

}