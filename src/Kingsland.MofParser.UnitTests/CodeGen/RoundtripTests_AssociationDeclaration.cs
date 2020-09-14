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
                var sourceText =
                    "association GOLF_MemberLocker\r\n" +
                    "{\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void AssociationDeclarationWithSuperAssociationShouldRoundtrip()
            {
                var sourceText =
                    "association GOLF_MemberLocker : GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tGOLF_ClubMember REF Member;\r\n" +
                    "\tGOLF_Locker REF Locker;\r\n" +
                    "\tGOLF_Date AssignedOnDate;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void AssociationDeclarationWithClassFeaturesShouldRoundtrip()
            {
                var sourceText =
                    "association GOLF_MemberLocker\r\n" +
                    "{\r\n" +
                    "\tGOLF_ClubMember REF Member;\r\n" +
                    "\tGOLF_Locker REF Locker;\r\n" +
                    "\tGOLF_Date AssignedOnDate;\r\n" +
                    "};";
               RoundtripTests.AssertRoundtrip(sourceText);
            }

        }

        #endregion

    }

}