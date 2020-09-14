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
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void MethodDeclarationWithParameterShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void MethodDeclarationWithArrayParameterShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers[]);\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void MethodDeclarationsWithRefParameterShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger GetMembersWithOutstandingFees(GOLF_ClubMember REF lateMembers);\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/27")]
            public static void ClassDeclarationsWithMethodDeclarationWithEnumParameterShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Professional : GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tGOLF_ResultCodeEnum GetNumberOfProfessionals(Integer NoOfPros, GOLF_Club Club, ProfessionalStatusEnum Status = Professional);\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/37")]
            public static void MethodDeclarationsWithArrayReturnTypeShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Club\r\n" +
                    "{\r\n" +
                    "\tInteger[] GetMembersWithOutstandingFees(GOLF_ClubMember lateMembers);\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/38")]
            public static void MethodDeclarationWithMultipleParametersShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Professional : GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tGOLF_ResultCodeEnum GetNumberOfProfessionals(Integer NoOfPros, GOLF_Club Club, ProfessionalStatusEnum Status = Professional);\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
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
                RoundtripTests.AssertRoundtrip(sourceText);
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
                RoundtripTests.AssertRoundtrip(sourceText);
            }

        }

        #endregion

    }

}