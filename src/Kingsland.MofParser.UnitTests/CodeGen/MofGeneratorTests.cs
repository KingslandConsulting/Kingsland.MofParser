﻿using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Source;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static class RoundtripTests
    {

        #region 7.3 Compiler directives

        public static class CompilerDirectiveTests
        {

            [Test]
            public static void CompilerDirectiveShouldRoundtrip()
            {
                var expectedMof =
                    "#pragma include (\"GlobalStructs/GOLF_Address.mof\")";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void CompilerDirectiveWithMultipleSingleStringsShouldRoundtrip()
            {
                var expectedMof =
                    "#pragma include (\"GlobalStructs\" \"/\" \"GOLF_Address.mof\")";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.1 Structure declaration

        public static class StructureDeclarationTests
        {

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

            [Test]
            public static void StructureDeclarationAstWithLocalEnumerationDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "structure GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tenumeration MonthsEnum : String\r\n" +
                    "\t{\r\n" +
                    "\t\tJanuary,\r\n" +
                    "\t\tFebruary,\r\n" +
                    "\t\tMarch,\r\n" +
                    "\t\tApril,\r\n" +
                    "\t\tMay,\r\n" +
                    "\t\tJune,\r\n" +
                    "\t\tJuly,\r\n" +
                    "\t\tAugust,\r\n" +
                    "\t\tSeptember,\r\n" +
                    "\t\tOctober,\r\n" +
                    "\t\tNovember,\r\n" +
                    "\t\tDecember\r\n" +
                    "\t};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StructureDeclarationAstWithEnumerationPropertyShouldRoundtrip()
            {
                var expectedMof =
                    "structure GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonthsEnum Month = MonthsEnum.January;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.2 Class declaration

        public static class ClassDeclarationTests
        {

            [Test]
            public static void ClassDeclarationsAstShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tstring InstanceID;\r\n" +
                    "\tstring Caption = Null;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassDeclarationsAstWithQualifierListShouldRoundtrip()
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
            public static void ClassDeclarationsAstWithZeroValueUint8PropertyShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tuint8 Severity = 0;\r\n" +
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

            [Test]
            public static void ClassDeclarationsAstWithEnumerationPropertyInitializerShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Professional : GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tGOLF_ProfessionalStatusEnum Status = Professional;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.3 Association declaration

        public static class AssociationDeclarationTests
        {

            [Test]
            public static void AssociationDeclarationAstShouldRoundtrip()
            {
                var expectedMof =
                    "association GOLF_MemberLocker : GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tGOLF_ClubMember REF Member;\r\n" +
                    "\tGOLF_Locker REF Locker;\r\n" +
                    "\tGOLF_Date AssignedOnDate;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.4 Enumeration declaration

        public static class EnumerationDeclarationTests
        {

            [Test]
            public static void EnumerationDeclarationAstShouldRoundtrip()
            {
                var expectedMof =
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
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.5.6 Method declaration

        public static class MethodDeclarationTests
        {

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/27"),
             Ignore("https://github.com/mikeclayton/MofParser/issues/27")]
            public static void ClassDeclarationsWithMethodDeclarationWithEnumParameterShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Professional : GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tGOLF_ResultCodeEnum GetNumberOfProfessionals (\r\n" +
                    "\tInteger NoOfPros,\r\n" +
                    "\tGOLF_Club Club,\r\n" +
                    "\tProfessionalStatusEnum Status = Professional\r\n" +
                    "\t)\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28"),
             Ignore("https://github.com/mikeclayton/MofParser/issues/28")]
            public static void ClassDeclarationsWithMethodDeclarationWithDeprecatedPrimtitiveValueTypeInitializerShouldRoundtrip()
            {
                var expectedMof =
                    "class Win32_SoftwareFeature : CIM_SoftwareFeature\r\n" +
                    "{\r\n" +
                    "\tuint32 Reinstall(uint16 ReinstallMode = 1);\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.6.1.3 String values

        public static class StringValueTests
        {

            [Test]
            public static void StringValueWithSingleQuoteShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.6.1.5 Boolean value

        public static class BooleanValueTests
        {

            [Test]
            public static void BooleanValueAstShouldRoundtrip()
            {
                var expectedMof =
                    "instance of myType as $Alias00000070\r\n" +
                    "{\r\n" +
                    "\tReference = TRUE;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.6.2 Complex type value

        public static class ComplexTypeValueTests
        {

            [Test]
            public static void InstanceValueDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
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
                    "\tMyBinaryValue = 0101010b;\r\n" +
                    "\tMyOctalValue = 0444444;\r\n" +
                    "\tMyHexValue = 0xABC123;\r\n" +
                    "\tMyDecimalValue = 12345;\r\n" +
                    "\tMyRealValue = 123.45;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/26"),
             Ignore("https://github.com/mikeclayton/MofParser/issues/26")]
            public static void InstanceValueDeclarationsWithInstanceValuePropertyShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = instance of GOLF_Date\r\n" +
                    "\t{\r\n" +
                    "\tYear = 2011;\r\n" +
                    "\tMonth = July;\r\n" +
                    "\tDay = 31;\r\n" +
                    "\t};\r\n" +
                    "}";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void InstanceValueDeclarationWithStructureValueDeclarationPropertyShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
                    "\tMemberAddress = value of GOLF_Address\r\n" +
                    "\t{\r\n" +
                    "\t\tState = \"IL\";\r\n" +
                    "\t\tCity = \"Oak Park\";\r\n" +
                    "\t\tStreet = \"Oak Park Av.\";\r\n" +
                    "\t\tStreetNo = \"1177\";\r\n" +
                    "\t\tApartmentNo = \"3B\";\r\n" +
                    "\t};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StructureValueDeclarationShouldRoundtrip()
            {
                var expectedMof =
                    "value of GOLF_PhoneNumber as $JohnDoesPhoneNo\r\n" +
                    "{\r\n" +
                    "\tAreaCode = {\"9\", \"0\", \"7\"};\r\n" +
                    "\tNumber = {\"7\", \"4\", \"7\", \"4\", \"8\", \"8\", \"4\"};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

        #endregion

        #region 7.6.3 Enum type value

        public static class EnumTypeValueTests
        {

            [Test]
            public static void ClassDeclarationsAstWithUnqualifiedEnumValuePropertyInitializerShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonthsEnum Month = January;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void ClassDeclarationsAstWithQualifiedEnumValuePropertyInitializerShouldRoundtrip()
            {
                var expectedMof =
                    "class GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonthsEnum Month = MonthsEnum.January;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void InstanceValueDeclarationsAstWithUnqualifiedEnumValuePropertyShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = July;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void InstanceValueDeclarationsAstWithQualifiedEnumValuePropertyShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = MonthsEnum.January;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
            public static void InstanceValueDeclarationsAstWithUnqualifiedEnumValueArrayPropertyShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {July, August};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/25")]
            public static void InstanceValueDeclarationsAstWithQualifiedEnumValueArrayPropertyShouldRoundtrip()
            {
                var expectedMof =
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {MonthsEnum.January, MonthEnum.February};\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StructureValueDeclarationsAstWithUnqualifiedEnumValuePropertyShouldRoundtrip()
            {
                var expectedMof =
                    "value of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = July;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

            [Test]
            public static void StructureValueDeclarationsAstWithQualifiedEnumValuePropertyShouldRoundtrip()
            {
                var expectedMof =
                    "value of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = MonthsEnum.January;\r\n" +
                    "};";
                var actualTokens = Lexing.Lexer.Lex(SourceReader.From(expectedMof));
                var actualAst = Parser.Parse(actualTokens);
                var actualMof = MofGenerator.ConvertToMof(actualAst);
                Assert.AreEqual(expectedMof, actualMof);
            }

        }

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