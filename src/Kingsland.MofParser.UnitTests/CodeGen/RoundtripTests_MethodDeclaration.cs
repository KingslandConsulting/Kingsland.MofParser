using Kingsland.MofParser.Tokens;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.6 Method declaration

        public static class MethodDeclarationTests
        {

            [Test]
            public static void MethodDeclarationWithNoParametersShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees();\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // instance of GOLF_Club
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Club")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // Integer GetMembersWithOutstandingFees();
                   .IdentifierToken("Integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GetMembersWithOutstandingFees")
                   .ParenthesisOpenToken()
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void MethodDeclarationWithParameterShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // instance of GOLF_Club
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Club")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // Integer GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);
                   .IdentifierToken("Integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GetMembersWithOutstandingFees")
                   .ParenthesisOpenToken()
                   .IdentifierToken("GOLF_ClubMember")
                   .WhitespaceToken(" ")
                   .IdentifierToken("lateMembers")
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void MethodDeclarationWithArrayParameterShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers[]);\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // instance of GOLF_Club
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Club")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // Integer GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers[]);
                   .IdentifierToken("Integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GetMembersWithOutstandingFees")
                   .ParenthesisOpenToken()
                   .IdentifierToken("GOLF_ClubMember")
                   .WhitespaceToken(" ")
                   .IdentifierToken("lateMembers")
                   .AttributeOpenToken()
                   .AttributeCloseToken()
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void MethodDeclarationsWithRefParameterShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember REF lateMembers);\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // instance of GOLF_Club
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Club")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // Integer GetMembersWithOutstandingFees(GOLF_ClubMember REF lateMembers);
                   .IdentifierToken("Integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GetMembersWithOutstandingFees")
                   .ParenthesisOpenToken()
                   .IdentifierToken("GOLF_ClubMember")
                   .WhitespaceToken(" ")
                   .IdentifierToken("REF")
                   .WhitespaceToken(" ")
                   .IdentifierToken("lateMembers")
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/27")]
            public static void ClassDeclarationsWithMethodDeclarationWithEnumParameterShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Professional : GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tGOLF_ResultCodeEnum GetNumberOfProfessionals(ProfessionalStatusEnum Status = Professional);\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // instance of GOLF_Club
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Professional")
                   .WhitespaceToken(" ")
                   .ColonToken()
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_ClubMember")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // GOLF_ResultCodeEnum GetNumberOfProfessionals(ProfessionalStatusEnum Status = Professional);
                   .IdentifierToken("GOLF_ResultCodeEnum")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GetNumberOfProfessionals")
                   .ParenthesisOpenToken()
                   .IdentifierToken("ProfessionalStatusEnum")
                   .WhitespaceToken(" ")
                   .IdentifierToken("Status")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IdentifierToken("Professional")
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/37")]
            public static void MethodDeclarationsWithArrayReturnTypeShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger[] GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // instance of GOLF_Club
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Club")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // Integer[] GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);
                   .IdentifierToken("Integer")
                   .AttributeOpenToken()
                   .AttributeCloseToken()
                   .WhitespaceToken(" ")
                   .IdentifierToken("GetMembersWithOutstandingFees")
                   .ParenthesisOpenToken()
                   .IdentifierToken("GOLF_ClubMember")
                   .WhitespaceToken(" ")
                   .IdentifierToken("lateMembers")
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/38")]
            public static void MethodDeclarationWithMultipleParametersShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Professional : GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tGOLF_ResultCodeEnum GetNumberOfProfessionals(Integer NoOfPros, GOLF_Club Club, ProfessionalStatusEnum Status = Professional);\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // class GOLF_Professional : GOLF_ClubMember
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Professional")
                   .WhitespaceToken(" ")
                   .ColonToken()
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_ClubMember")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // GOLF_ResultCodeEnum GetNumberOfProfessionals(Integer NoOfPros, GOLF_Club Club, ProfessionalStatusEnum Status = Professional);
                   .IdentifierToken("GOLF_ResultCodeEnum")
                   .WhitespaceToken(" ")
                   .IdentifierToken("GetNumberOfProfessionals")
                   .ParenthesisOpenToken()
                   .IdentifierToken("Integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("NoOfPros")
                   .CommaToken()
                   .WhitespaceToken(" ")
                   .IdentifierToken("GOLF_Club")
                   .WhitespaceToken(" ")
                   .IdentifierToken("Club")
                   .CommaToken()
                   .WhitespaceToken(" ")
                   .IdentifierToken("ProfessionalStatusEnum")
                   .WhitespaceToken(" ")
                   .IdentifierToken("Status")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IdentifierToken("Professional")
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28")]
            public static void MethodDeclarationWithDeprecatedMof300IntegerReturnTypesAndQuirksDisabledShouldRoundtrip()
            {
                var sourceText =
                    "class Win32_SoftwareFeature : CIM_SoftwareFeature\r\n" +
                    "{\r\n" +
                    "\tuint8 ReinstallUint8(integer ReinstallMode = 1);\r\n" +
                    "\tuint16 ReinstallUint16(integer ReinstallMode = 1);\r\n" +
                    "\tuint32 ReinstallUint32(integer ReinstallMode = 1);\r\n" +
                    "\tuint64 ReinstallUint64(integer ReinstallMode = 1);\r\n" +
                    "\tsint8 ReinstallUint8(integer ReinstallMode = 1);\r\n" +
                    "\tsint16 ReinstallUint16(integer ReinstallMode = 1);\r\n" +
                    "\tsint32 ReinstallUint32(integer ReinstallMode = 1);\r\n" +
                    "\tsint64 ReinstallUint64(integer ReinstallMode = 1);\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // class GOLF_Professional : GOLF_ClubMember
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("Win32_SoftwareFeature")
                   .WhitespaceToken(" ")
                   .ColonToken()
                   .WhitespaceToken(" ")
                   .IdentifierToken("CIM_SoftwareFeature")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // uint8 ReinstallUint8(integer ReinstallMode = 1);
                   .IdentifierToken("uint8")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint8")
                   .ParenthesisOpenToken()
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // uint16 ReinstallUint16(integer ReinstallMode = 1);
                   .IdentifierToken("uint16")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint16")
                   .ParenthesisOpenToken()
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // uint32 ReinstallUint32(integer ReinstallMode = 1);
                   .IdentifierToken("uint32")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint32")
                   .ParenthesisOpenToken()
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // uint64 ReinstallUint64(integer ReinstallMode = 1);
                   .IdentifierToken("uint64")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint64")
                   .ParenthesisOpenToken()
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // sint8 ReinstallUint8(integer ReinstallMode = 1);
                   .IdentifierToken("sint8")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint8")
                   .ParenthesisOpenToken()
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // sint16 ReinstallUint16(integer ReinstallMode = 1);
                   .IdentifierToken("sint16")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint16")
                   .ParenthesisOpenToken()
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // sint32 ReinstallUint32(integer ReinstallMode = 1);
                   .IdentifierToken("sint32")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint32")
                   .ParenthesisOpenToken()
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // sint64 ReinstallUint64(integer ReinstallMode = 1);
                   .IdentifierToken("sint64")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint64")
                   .ParenthesisOpenToken()
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28")]
            public static void MethodDeclarationWithDeprecatedMof300IntegerParameterTypesShouldRoundtrip()
            {
                var sourceText =
                    "class Win32_SoftwareFeature : CIM_SoftwareFeature\r\n" +
                    "{\r\n" +
                    "\tinteger ReinstallUint8(uint8 ReinstallMode = 1);\r\n" +
                    "\tinteger ReinstallUint16(uint16 ReinstallMode = 1);\r\n" +
                    "\tinteger ReinstallUint32(uint32 ReinstallMode = 1);\r\n" +
                    "\tinteger ReinstallUint64(uint64 ReinstallMode = 1);\r\n" +
                    "\tinteger ReinstallUint8(sint8 ReinstallMode = 1);\r\n" +
                    "\tinteger ReinstallUint16(sint16 ReinstallMode = 1);\r\n" +
                    "\tinteger ReinstallUint32(sint32 ReinstallMode = 1);\r\n" +
                    "\tinteger ReinstallUint64(sint64 ReinstallMode = 1);\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                   // class GOLF_Professional : GOLF_ClubMember
                   .IdentifierToken("class")
                   .WhitespaceToken(" ")
                   .IdentifierToken("Win32_SoftwareFeature")
                   .WhitespaceToken(" ")
                   .ColonToken()
                   .WhitespaceToken(" ")
                   .IdentifierToken("CIM_SoftwareFeature")
                   .WhitespaceToken("\r\n")
                   // {
                   .BlockOpenToken()
                   .WhitespaceToken("\r\n\t")
                   // integer ReinstallUint8(uint8 ReinstallMode = 1);
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint8")
                   .ParenthesisOpenToken()
                   .IdentifierToken("uint8")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // integer ReinstallUint16(uint16 ReinstallMode = 1);
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint16")
                   .ParenthesisOpenToken()
                   .IdentifierToken("uint16")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // integer ReinstallUint32(uint32 ReinstallMode = 1);
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint32")
                   .ParenthesisOpenToken()
                   .IdentifierToken("uint32")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // integer ReinstallUint64(uint64 ReinstallMode = 1);
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint64")
                   .ParenthesisOpenToken()
                   .IdentifierToken("uint64")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // integer ReinstallUint8(sint8 ReinstallMode = 1);
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint8")
                   .ParenthesisOpenToken()
                   .IdentifierToken("sint8")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // integer ReinstallUint16(sint16 nteger ReinstallMode = 1);
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint16")
                   .ParenthesisOpenToken()
                   .IdentifierToken("sint16")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // integer ReinstallUint32(sint32 ReinstallMode = 1);
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint32")
                   .ParenthesisOpenToken()
                   .IdentifierToken("sint32")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n\t")
                   // integer ReinstallUint64(sint64 ReinstallMode = 1);
                   .IdentifierToken("integer")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallUint64")
                   .ParenthesisOpenToken()
                   .IdentifierToken("sint64")
                   .WhitespaceToken(" ")
                   .IdentifierToken("ReinstallMode")
                   .WhitespaceToken(" ")
                   .EqualsOperatorToken()
                   .WhitespaceToken(" ")
                   .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                   .ParenthesisCloseToken()
                   .StatementEndToken()
                   .WhitespaceToken("\r\n")
                   // };
                   .BlockCloseToken()
                   .StatementEndToken()
                   .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

        }

        #endregion

    }

}