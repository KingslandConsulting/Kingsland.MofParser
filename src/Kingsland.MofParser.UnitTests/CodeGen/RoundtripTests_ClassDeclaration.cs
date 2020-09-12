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

        #region 7.5.2 Class declaration

        public static class ClassDeclarationTests
        {

            [Test]
            public static void EmptyClassDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

            [Test]
            public static void ClassDeclarationWithSuperclassShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Base : GOLF_Superclass\r\n" +
                    "{\r\n" +
                    "\tstring InstanceID;\r\n" +
                    "\tstring Caption = Null;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void ClassDeclarationWithClassFeaturesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tstring InstanceID;\r\n" +
                    "\tstring Caption = Null;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void ClassDeclarationsWithQualifierListShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "[Abstract, OCL{\"-- the key property cannot be NULL\", \"inv: InstanceId.size() = 10\"}]\r\n" +
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\t[Description(\"an instance of a class that derives from the GOLF_Base class. \"), Key] string InstanceID;\r\n" +
                    "\t[Description(\"A short textual description (one- line string) of the\"), MaxLen(64)] string Caption = Null;\r\n" +
                    "};"
                );
            }

        }

        public static class ClassFeatureTests
        {

            [Test]
            public static void ClassFeatureWithQualifiersShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\t[Description(\"Monthly salary in $US\")] string Name;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void InvalidClassFeatureShouldThrow()
            {
                var sourceMof =
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\t100\r\n" +
                    "};";
                var tokens = Lexer.Lex(SourceReader.From(sourceMof));
                var tokensMof = TokenMofGenerator.ConvertToMof(tokens);
                var ex = Assert.Throws<UnexpectedTokenException>(
                    () => {
                        var astNodes = Parser.Parse(tokens);
                    }
                );
                Assert.AreEqual(
                    "Unexpected token found at Position 19, Line Number 3, Column Number 2.\r\n" +
                    "Token Type: 'IntegerLiteralToken'\r\n" +
                    "Token Text: '100'",
                    ex.Message
                );
            }

            [Test]
            public static void ClassFeatureWithStructureDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\tstructure Nested\r\n" +
                    "\t{\r\n" +
                    "\t};\r\n" +
                    "};"
                );
            }

            [Test]
            public static void ClassFeatureWithEnumerationDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\tenumeration MonthsEnum : Integer\r\n" +
                    "\t{\r\n" +
                    "\t};\r\n" +
                    "};"
                );
            }

            [Test]
            public static void ClassFeatureWithPropertyDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\tstring Name;\r\n" +
                    "};"
                );
            }

        }

        #endregion

    }

}