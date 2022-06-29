using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.6 Method declaration

    public static class MethodDeclarationTests
    {

        [Test]
        public static void MethodDeclarationWithNoParametersShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class GOLF_Club
                    {
                        Integer GetMembersWithOutstandingFees();
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // instance of GOLF_Club
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_Club")
               .WhitespaceToken($"{newline}")
               // {
               .BlockOpenToken()
               .WhitespaceToken($"{newline}{indent}")
               // Integer GetMembersWithOutstandingFees();
               .IdentifierToken("Integer")
               .WhitespaceToken(" ")
               .IdentifierToken("GetMembersWithOutstandingFees")
               .ParenthesisOpenToken()
               .ParenthesisCloseToken()
               .StatementEndToken()
               .WhitespaceToken($"{newline}")
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void MethodDeclarationWithParameterShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class GOLF_Club
                    {
                        Integer GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // instance of GOLF_Club
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_Club")
               .WhitespaceToken($"{newline}")
               // {
               .BlockOpenToken()
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}")
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void MethodDeclarationWithArrayParameterShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class GOLF_Club
                    {
                        Integer GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers[]);
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // instance of GOLF_Club
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_Club")
               .WhitespaceToken($"{newline}")
               // {
               .BlockOpenToken()
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}")
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void MethodDeclarationsWithRefParameterShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class GOLF_Club
                    {
                        Integer GetMembersWithOutstandingFees(GOLF_ClubMember REF lateMembers);
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // instance of GOLF_Club
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_Club")
               .WhitespaceToken($"{newline}")
               // {
               .BlockOpenToken()
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}")
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/27")]
        public static void ClassDeclarationsWithMethodDeclarationWithEnumParameterShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class GOLF_Professional : GOLF_ClubMember
                    {
                        GOLF_ResultCodeEnum GetNumberOfProfessionals(ProfessionalStatusEnum Status = Professional);
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // instance of GOLF_Club
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_Professional")
               .WhitespaceToken(" ")
               .ColonToken()
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_ClubMember")
               .WhitespaceToken($"{newline}")
               // {
               .BlockOpenToken()
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}")
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/37")]
        public static void MethodDeclarationsWithArrayReturnTypeShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class GOLF_Club
                    {
                        Integer[] GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // instance of GOLF_Club
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_Club")
               .WhitespaceToken($"{newline}")
               // {
               .BlockOpenToken()
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}")
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/38")]
        public static void MethodDeclarationWithMultipleParametersShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class GOLF_Professional : GOLF_ClubMember
                    {
                        GOLF_ResultCodeEnum GetNumberOfProfessionals(Integer NoOfPros, GOLF_Club Club, ProfessionalStatusEnum Status = Professional);
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // class GOLF_Professional : GOLF_ClubMember
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_Professional")
               .WhitespaceToken(" ")
               .ColonToken()
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_ClubMember")
               .WhitespaceToken($"{newline}")
               // {
               .BlockOpenToken()
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}")
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28")]
        public static void MethodDeclarationWithDeprecatedMof300IntegerReturnTypesAndQuirksDisabledShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class Win32_SoftwareFeature : CIM_SoftwareFeature
                    {
                        uint8 ReinstallUint8(integer ReinstallMode = 1);
                        uint16 ReinstallUint16(integer ReinstallMode = 1);
                        uint32 ReinstallUint32(integer ReinstallMode = 1);
                        uint64 ReinstallUint64(integer ReinstallMode = 1);
                        sint8 ReinstallUint8(integer ReinstallMode = 1);
                        sint16 ReinstallUint16(integer ReinstallMode = 1);
                        sint32 ReinstallUint32(integer ReinstallMode = 1);
                        sint64 ReinstallUint64(integer ReinstallMode = 1);
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // class GOLF_Professional : GOLF_ClubMember
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("Win32_SoftwareFeature")
               .WhitespaceToken(" ")
               .ColonToken()
               .WhitespaceToken(" ")
               .IdentifierToken("CIM_SoftwareFeature")
               .WhitespaceToken($"{newline}")
               // {
               .BlockOpenToken()
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}")
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28")]
        public static void MethodDeclarationWithDeprecatedMof300IntegerParameterTypesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    class Win32_SoftwareFeature : CIM_SoftwareFeature
                    {
                        integer ReinstallUint8(uint8 ReinstallMode = 1);
                        integer ReinstallUint16(uint16 ReinstallMode = 1);
                        integer ReinstallUint32(uint32 ReinstallMode = 1);
                        integer ReinstallUint64(uint64 ReinstallMode = 1);
                        integer ReinstallUint8(sint8 ReinstallMode = 1);
                        integer ReinstallUint16(sint16 ReinstallMode = 1);
                        integer ReinstallUint32(sint32 ReinstallMode = 1);
                        integer ReinstallUint64(sint64 ReinstallMode = 1);
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // class GOLF_Professional : GOLF_ClubMember
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("Win32_SoftwareFeature")
               .WhitespaceToken(" ")
               .ColonToken()
               .WhitespaceToken(" ")
               .IdentifierToken("CIM_SoftwareFeature")
               .WhitespaceToken($"{newline}")
               // {
               .BlockOpenToken()
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}{indent}")
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
               .WhitespaceToken($"{newline}")
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

    }

    #endregion

}
