using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Source;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static class MofGeneratorTests
    {

        public static class ConvertToMofTests
        {

            [Test]
            public static void BooleanValueAstShouldRoundtrip()
            {
                var expectedMof =
                    "instance of myType as $Alias00000070\r\n" +
                    "{\r\n" +
                    "    Reference = TRUE;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassDeclarationsAstWithQualifiersShouldRoundtrip()
            {
                var expectedMof =
                   "[Abstract, OCL{\"-- the key property cannot be NULL\", \"inv: InstanceId.size() = 10\"}]\r\n" +
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\t[Description(\"an instance of a class that derives from the GOLF_Base class. \"), Key] string InstanceID;\r\n" +
                    "\t[Description(\"A short textual description (one- line string) of the\"), MaxLen(64)] string Caption = Null;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassDeclarationsAstTestWithMofV2QualifierFlavorsShouldRoundtrip()
            {
                var expectedMof =
                    "[Locale(1033): ToInstance, UUID(\"{BE46D060-7A7C-11d2-BC85-00104B2CF71C}\"): ToInstance]\r\n" +
                    "class Win32_PrivilegesStatus : __ExtendedStatus\r\n" +
                    "{\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesNotHeld[];\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesRequired[];\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassDeclarationsAstTestWithNumericPropertiesShouldRoundtrip()
            {
                var expectedMof =
                    "instance of myType as $Alias00000070\r\n" +
                    "{\r\n" +
                    "    MyBinaryValue = 0101010b;\r\n" +
                    "    MyOctalValue = 0444444;\r\n" +
                    "    MyHexValue = 0xABC123;\r\n" +
                    "    MyDecimalValue = 12345;\r\n" +
                    "    MyRealValue = 123.45;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

    }

}
