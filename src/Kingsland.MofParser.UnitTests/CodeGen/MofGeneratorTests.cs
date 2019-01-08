using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Source;
using Kingsland.MofParser.UnitTests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static class MofGeneratorTests
    {

        public static class ConvertToMofTests
        {

            #region Individual Roundtrip Tests

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
            public static void ClassDeclarationsAstWithMofV2QualifierFlavorsShouldRoundtrip()
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
            public static void ClassDeclarationsAstWithNumericPropertiesShouldRoundtrip()
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

            [Test]
            public static void ClassDeclarationsAstWithReferencePropertyShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Club : GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tGOLF_ClubMember REF AllMembers[];\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassDeclarationsAstWithMethodWithRefArrayParameters()
            {
                var expectedMof =
                    "class GOLF_Club : GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees([Out] GOLF_ClubMember REF lateMembers[]);\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassDeclarationsAstWithStructureDeclarationPropertyShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Professional : GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tstructure Sponsor\r\n" +
                    "\t{\r\n" +
                    "\t\tstring Name;\r\n" +
                    "\t\tGOLF_Date ContractSignedDate;\r\n" +
                    "\t\treal32 ContractAmount;\r\n" +
                    "\t};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            #region 7.5.1 Structure declaration

            [Test]
            public static void StructureDeclarationAstShouldRoundtrip()
            {
                var expectedMof =
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\tstring Name;\r\n" +
                    "\tGOLF_Date ContractSignedDate;\r\n" +
                    "\treal32 ContractAmount;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            #endregion

            #endregion

            #region Roundtrip Test Cases

            //[TestFixture]
            //public static class ConvertToMofMethodTestCasesWmiWinXp
            //{
            //    [Test, TestCaseSource(typeof(ConvertToMofMethodTestCasesWmiWinXp), "GetTestCases")]
            //    public static void ConvertToMofMethodTestsFromDisk(string mofFilename)
            //    {
            //        ConvertToMofTests.MofGeneratorRoundtripTest(mofFilename);
            //    }
            //    public static IEnumerable<TestCaseData> GetTestCases
            //    {
            //        get
            //        {
            //            return TestUtils.GetMofTestCase("Parsing\\WMI\\WinXp");
            //        }
            //    }
            //}

            //[TestFixture]
            //public static class ConvertToMofMethodGolfExamples
            //{
            //    //[Test, TestCaseSource(typeof(ConvertToMofMethodGolfExamples), "GetTestCases")]
            //    public static void ConvertToMofMethodTestsFromDisk(string mofFilename)
            //    {
            //        ConvertToMofTests.MofGeneratorRoundtripTest(mofFilename);
            //    }
            //    public static IEnumerable<TestCaseData> GetTestCases
            //    {
            //        get
            //        {
            //            return TestUtils.GetMofTestCase("Parsing\\DSP0221_3.0.1");
            //        }
            //    }
            //}

            //private static void MofGeneratorRoundtripTest(string mofFilename)
            //{
            //    var expectedMof = File.ReadAllText(mofFilename);
            //    var reader = SourceReader.From(expectedMof);
            //    var tokens = Lexing.Lexer.Lex(reader);
            //    var ast = Parser.Parse(tokens);
            //    var actualMof = MofGenerator.ConvertToMof(ast);
            //    Assert.AreEqual(expectedMof, actualMof);
            //}

            #endregion

        }

    }

}
