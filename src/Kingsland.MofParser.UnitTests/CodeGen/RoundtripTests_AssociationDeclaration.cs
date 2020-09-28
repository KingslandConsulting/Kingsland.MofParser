using Kingsland.MofParser.Tokens;
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
                var expectedTokens = new TokenBuilder()
                    // association GOLF_MemberLocker
                    .IdentifierToken("association")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_MemberLocker")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
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
                var expectedTokens = new TokenBuilder()
                    // association GOLF_MemberLocker : GOLF_Base
                    .IdentifierToken("association")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_MemberLocker")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Base")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // GOLF_ClubMember REF Member;
                    .IdentifierToken("GOLF_ClubMember")
                    .WhitespaceToken(" ")
                    .IdentifierToken("REF")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Member")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // GOLF_Locker REF Locker;
                    .IdentifierToken("GOLF_Locker")
                    .WhitespaceToken(" ")
                    .IdentifierToken("REF")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Locker")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // GOLF_Date AssignedOnDate;
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken(" ")
                    .IdentifierToken("AssignedOnDate")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
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
                var expectedTokens = new TokenBuilder()
                    // association GOLF_MemberLocker
                    .IdentifierToken("association")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_MemberLocker")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // GOLF_ClubMember REF Member;
                    .IdentifierToken("GOLF_ClubMember")
                    .WhitespaceToken(" ")
                    .IdentifierToken("REF")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Member")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // GOLF_Locker REF Locker;
                    .IdentifierToken("GOLF_Locker")
                    .WhitespaceToken(" ")
                    .IdentifierToken("REF")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Locker")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // GOLF_Date AssignedOnDate
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken(" ")
                    .IdentifierToken("AssignedOnDate")
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