using Kingsland.MofParser.CodeGen;
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
                RoundtripTests.AssertRoundtrip(
                    "#pragma include (\"GlobalStructs/GOLF_Address.mof\")"
                );
            }

            [Test]
            public static void CompilerDirectiveWithMultipleSingleStringsShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "#pragma include (\"GlobalStructs\" \"/\" \"GOLF_Address.mof\")"
                );
            }

        }

        #endregion

        #region 7.4 Qualifiers

        public static class QualifierTests
        {

            [Test]
            public static void QualifierShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "[Description(\"Instances of this class represent golf clubs. A golf club is \" \"an organization that provides member services to golf players \" \"both amateur and professional.\")]\r\n" +
                    "class GOLF_Club : GOLF_Base\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

        }

        #endregion

        #region 7.4.1 QualifierList

        public static class QualifierListTests
        {

        }

        public static class ParseQualifierValueTests
        {

            [Test]
            public static void QualifierWithMofV2FlavorsAndQuirksEnabledShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "[Locale(1033): ToInstance, UUID(\"{BE46D060-7A7C-11d2-BC85-00104B2CF71C}\"): ToInstance]\r\n" +
                    "class Win32_PrivilegesStatus : __ExtendedStatus\r\n" +
                    "{\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesNotHeld[];\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesRequired[];\r\n" +
                    "};",
                    ParserQuirks.AllowMofV2Qualifiers
                );
            }

            [Test]
            public static void QualifierWithMofV2FlavorsAndQuirksDisabledShouldThrow()
            {
                var sourceMof =
                    "[Locale(1033): ToInstance, UUID(\"{BE46D060-7A7C-11d2-BC85-00104B2CF71C}\"): ToInstance]\r\n" +
                    "class Win32_PrivilegesStatus : __ExtendedStatus\r\n" +
                    "{\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesNotHeld[];\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesRequired[];\r\n" +
                    "};";
                var tokens = Lexing.Lexer.Lex(SourceReader.From(sourceMof));
                var tokensMof = TokenMofGenerator.ConvertToMof(tokens);
                var ex = Assert.Throws<UnexpectedTokenException>(
                    () =>
                    {
                        var astNodes = Parser.Parse(tokens);
                    }
                );
                Assert.AreEqual(
                    "Unexpected token found at Position 13, Line Number 1, Column Number 14.\r\n" +
                    "Token Type: 'ColonToken'\r\n" +
                    "Token Text: ':'",
                    ex.Message
                );
            }

        }

        #endregion

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
                var tokens = Lexing.Lexer.Lex(SourceReader.From(sourceMof));
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
                var tokens = Lexing.Lexer.Lex(SourceReader.From(sourceMof));
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

        #region 7.5.3 Association declaration

        public static class AssociationDeclarationTests
        {

            [Test]
            public static void EmptyAssociationDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "association GOLF_MemberLocker\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

            [Test]
            public static void AssociationDeclarationWithSuperAssociationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "association GOLF_MemberLocker : GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tGOLF_ClubMember REF Member;\r\n" +
                    "\tGOLF_Locker REF Locker;\r\n" +
                    "\tGOLF_Date AssignedOnDate;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void AssociationDeclarationWithClassFeaturesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "association GOLF_MemberLocker\r\n" +
                    "{\r\n" +
                    "\tGOLF_ClubMember REF Member;\r\n" +
                    "\tGOLF_Locker REF Locker;\r\n" +
                    "\tGOLF_Date AssignedOnDate;\r\n" +
                    "};"
                );
            }

        }

        #endregion

        #region 7.5.4 Enumeration declaration

        public static class EnumerationDeclarationTests
        {

            [Test]
            public static void EmptyIntegerEnumerationDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "enumeration MonthsEnum : Integer\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

            [Test]
            public static void EmptyStringEnumerationDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "enumeration MonthsEnum : String\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

            [Test]
            public static void EmptyInheritedEnumerationDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "enumeration MonthsEnum : GOLF_MyEnum\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

            [Test]
            public static void EnumerationDeclarationWithoutValuesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
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
                    "};"
                );
            }

        }

        public static class EnumElementTests
        {

            [Test]
            public static void EnumElementWithQualifiersShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "enumeration MonthsEnum : integer\r\n" +
                    "{\r\n" +
                    "\t[Description(\"myDescription\")] January = 1\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/41")]
            public static void IntegerEnumElementShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "enumeration MonthsEnum : integer\r\n" +
                    "{\r\n" +
                    "\tJanuary = 1\r\n" +
                    "};"
                );
            }

            [Test]
            public static void StringEnumElementWithoutValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "enumeration GOLF_StatesEnum : string\r\n" +
                    "{\r\n" +
                    "\tAL\r\n" +
                    "};"
                );
            }

            [Test]
            public static void StringEnumElementWithValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "enumeration GOLF_StatesEnum : string\r\n" +
                    "{\r\n" +
                    "\tAL = \"Alabama\"\r\n" +
                    "};"
                );
            }

        }

        #endregion

        #region 7.5.5 Property declaration

        public static class PropertyDeclarationTests
        {

            [Test]
            public static void PropertyDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void PropertyDeclarationWithArrayTypeShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity[];\r\n" +
                    "};"
                );
            }

            [Test]
            public static void PropertyDeclarationWithDefaultValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity = 0;\r\n" +
                    "};"
                );
            }

        }

        #endregion

        #region 7.5.6 Method declaration

        public static class MethodDeclarationTests
        {

            [Test]
            public static void MethodDeclarationWithNoParametersShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees();\r\n" +
                    "};"
                );
            }

            [Test]
            public static void MethodDeclarationWithParameterShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);\r\n" +
                    "};"
                );
            }

            [Test]
            public static void MethodDeclarationWithArrayParameterShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers[]);\r\n" +
                    "};"
                );
            }

            [Test]
            public static void MethodDeclarationsWithRefParameterShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember REF lateMembers);\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/27")]
            public static void ClassDeclarationsWithMethodDeclarationWithEnumParameterShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Professional : GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tGOLF_ResultCodeEnum GetNumberOfProfessionals(Integer NoOfPros, GOLF_Club Club, ProfessionalStatusEnum Status = Professional);\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28"),
             Ignore("https://github.com/mikeclayton/MofParser/issues/28")]
            public static void ClassDeclarationsWithMethodDeclarationWithDeprecatedPrimtitiveValueTypeInitializerShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class Win32_SoftwareFeature : CIM_SoftwareFeature\r\n" +
                    "{\r\n" +
                    "\tuint32 Reinstall(uint16 ReinstallMode = 1);\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/37")]
            public static void MethodDeclarationsWithArrayReturnTypeShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger[] GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/38")]
            public static void MethodDeclarationWithMultipleParametersShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Professional : GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tGOLF_ResultCodeEnum GetNumberOfProfessionals(Integer NoOfPros, GOLF_Club Club, ProfessionalStatusEnum Status = Professional);\r\n" +
                    "};"
                );
            }

        }

        #endregion

        #region 7.5.9 Complex type value

        public static class ComplexTypeValueTests
        {

            [Test]
            public static void ComplexTypeValueWithComplexValuePropertyShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = $MyAliasIdentifier;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void ComplexTypeValueWithComplexValueArrayPropertyShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier};\r\n" +
                    "};"
                );
            }

        }

        public static class ComplexValueTests
        {

            [Test]
            public static void ComplexValuePropertyShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = $MyAliasIdentifier;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void ComplexValuePropertyWithValueOfShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = value of GOLF_Date\r\n" +
                    "\t{\r\n" +
                    "\t\tMonth = July;\r\n" +
                    "\t};\r\n" +
                    "};"
                );
            }

        }

        public static class ComplexValueArrayTests
        {

            [Test]
            public static void ComplexValueArrayWithOneItemShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier};\r\n" +
                    "};"
                );
            }

            [Test]
            public static void ComplexValueArrayWithMultipleItemsShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier, $MyOtherAliasIdentifier};\r\n" +
                    "};"
                );
            }

        }

        #endregion

        #region 7.6.1 Primitive type value

        public static class LiteralValueArrayTests
        {

            [Test]
            public static void LiteralValueArrayWithOneItemShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {1};\r\n" +
                    "};"
                );
            }

            [Test]
            public static void LiteralValueArrayWithMultipleItemsShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {1, 2};\r\n" +
                    "};"
                );
            }

        }

        public static class LiteralValueTests
        {

            [Test]
            public static void IntegerLiteralValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = 1;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void RealLiteralValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = 0.5;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void BooleanLiteralValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = true;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void NullLiteralValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = null;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void StringLiteralValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = \"aaa\";\r\n" +
                    "};"
                );
            }

        }

        #endregion

        #region 7.6.1.1 Integer values

        public static class IntegerValueTests
        {

            [Test]
            public static void IntegerValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 100;\r\n" +
                    "};"
                );
            }

            [Test, Ignore("")]
            public static void PositiveIntegerValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = +100;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void NegativeIntegerValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = -100;\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void IntegerValuePropertiesInOtherBasesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of myType as $Alias00000070\r\n" +
                    "{\r\n" +
                    "\tMyBinaryValue1 = 0101010b;\r\n" +
                    "\tMyBinaryValue2 = +0101010b;\r\n" +
                    "\tMyBinaryValue3 = -0101010b;\r\n" +
                    "\tMyOctalValue1 = 000444444;\r\n" +
                    "\tMyOctalValue2 = +000444444;\r\n" +
                    "\tMyOctalValue3 = -000444444;\r\n" +
                    "\tMyHexValue1 = 0x00ABC123;\r\n" +
                    "\tMyHexValue2 = +0x00ABC123;\r\n" +
                    "\tMyHexValue3 = -0x00ABC123;\r\n" +
                    "\tMyDecimalValue1 = 12345;\r\n" +
                    "\tMyDecimalValue2 = +12345;\r\n" +
                    "\tMyDecimalValue3 = -12345;\r\n" +
                    "\tMyRealValue1 = 00123.45;\r\n" +
                    "\tMyRealValue2 = +00123.45;\r\n" +
                    "\tMyRealValue3 = -123.45;\r\n" +
                    "};"
                );
            }


        }

        #endregion

        #region 7.6.1.1 Real values

        public static class RealValueTests
        {

            [Test]
            public static void RealValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 0.5;\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void PositiveRealValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = +0.5;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void NegativeRealValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = -0.5;\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithNoFractionShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 5.0;\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithTrailingZerosShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 0.50;\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithNoIntegerPartShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = .5;\r\n" +
                    "};"
                );
            }

        }

        #endregion

        #region 7.6.1.3 String values

        public static class StringValueTests
        {

            [Test]
            public static void SingleStringValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance of John Doe\";\r\n" +
                    "};"
                );
            }

            [Test]
            public static void MultistringValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance\" \"of\" \"John\" \"Doe\";\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/20")]
            public static void StringValueWithSingleQuoteShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
                    "};"
                );
            }

        }

        #endregion

        #region 7.6.1.5 Boolean value

        public static class BooleanValueTests
        {

            [Test]
            public static void BooleanValueAstShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of myType as $Alias00000070\r\n" +
                    "{\r\n" +
                    "\tReference = TRUE;\r\n" +
                    "};"
                );
            }

        }

        #endregion

        #region 7.6.2 Complex type value

        public static class InstanceValueDeclarationTests
        {

            [Test]
            public static void InstanceValueDeclarationWithNoPropertiesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

            [Test]
            public static void InstanceValueDeclarationWithChildPropertiesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tFirstName = \"John\";\r\n" +
                    "\tLastName = \"Doe\";\r\n" +
                    "};"
                );
            }

            [Test]
            public static void InstanceValueDeclarationWithAliasShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

        }

        public static class StructureValueDeclarationTests
        {

            [Test]
            public static void StructureValueDeclarationWithNoPropertiesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "value of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

            [Test]
            public static void StructureValueDeclarationWithChildPropertiesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "value of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "\tFirstName = \"John\";\r\n" +
                    "\tLastName = \"Doe\";\r\n" +
                    "};"
                );
            }

        }

        //[Test]
        //public static void InstanceValueDeclarationShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "instance of GOLF_ClubMember\r\n" +
        //        "{\r\n" +
        //        "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
        //        "};"
        //    );
        //}

        //[Test]
        //public static void ClassDeclarationsAstWithNumericPropertiesShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "instance of myType as $Alias00000070\r\n" +
        //        "{\r\n" +
        //        "\tMyBinaryValue = 0101010b;\r\n" +
        //        "\tMyOctalValue = 0444444;\r\n" +
        //        "\tMyHexValue = 0xABC123;\r\n" +
        //        "\tMyDecimalValue = 12345;\r\n" +
        //        "\tMyRealValue = 123.45;\r\n" +
        //        "};"
        //    );
        //}

        //[Test(Description = "https://github.com/mikeclayton/MofParser/issues/26"),
        // Ignore("https://github.com/mikeclayton/MofParser/issues/26")]
        //public static void InstanceValueDeclarationsWithInstanceValuePropertyShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "instance of GOLF_ClubMember\r\n" +
        //        "{\r\n" +
        //        "\tLastPaymentDate = instance of GOLF_Date\r\n" +
        //        "\t{\r\n" +
        //        "\tYear = 2011;\r\n" +
        //        "\tMonth = July;\r\n" +
        //        "\tDay = 31;\r\n" +
        //        "\t};\r\n" +
        //        "}";
        //    );
        //}

        //[Test]
        //public static void InstanceValueDeclarationWithStructureValueDeclarationPropertyShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "instance of GOLF_ClubMember\r\n" +
        //        "{\r\n" +
        //        "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
        //        "\tMemberAddress = value of GOLF_Address\r\n" +
        //        "\t{\r\n" +
        //        "\t\tState = \"IL\";\r\n" +
        //        "\t\tCity = \"Oak Park\";\r\n" +
        //        "\t\tStreet = \"Oak Park Av.\";\r\n" +
        //        "\t\tStreetNo = \"1177\";\r\n" +
        //        "\t\tApartmentNo = \"3B\";\r\n" +
        //        "\t};\r\n" +
        //        "};";
        //    );
        //}

        //[Test]
        //public static void StructureValueDeclarationShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "value of GOLF_PhoneNumber as $JohnDoesPhoneNo\r\n" +
        //        "{\r\n" +
        //        "\tAreaCode = {\"9\", \"0\", \"7\"};\r\n" +
        //        "\tNumber = {\"7\", \"4\", \"7\", \"4\", \"8\", \"8\", \"4\"};\r\n" +
        //        "};";
        //    );
        //}

        #endregion

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
            public static void EnumValueArrayWithQualifiedEnumValuesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_Date\r\n" +
                    "{\r\n" +
                    "\tMonth = {MonthEnums.July};\r\n" +
                    "};"
                );
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

        private static void AssertRoundtrip(string sourceMof, ParserQuirks parserQuirks = ParserQuirks.None)
        {
            // check the lexer tokens roundtrips ok
            var tokens = Lexing.Lexer.Lex(SourceReader.From(sourceMof));
            var tokensMof = TokenMofGenerator.ConvertToMof(tokens);
            Assert.AreEqual(sourceMof, tokensMof);
            // check the parser ast roundtrips ok
            var astNodes = Parser.Parse(tokens, parserQuirks);
            var astMof = AstMofGenerator.ConvertToMof(astNodes);
            Assert.AreEqual(sourceMof, astMof);
        }

        #endregion

    }

}