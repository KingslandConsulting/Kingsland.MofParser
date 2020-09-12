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

    }

}