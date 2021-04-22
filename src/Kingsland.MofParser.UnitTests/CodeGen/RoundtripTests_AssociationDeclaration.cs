using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using NUnit.Framework;
using System.Collections.Generic;

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
                var expectedAst = new MofSpecificationAst.Builder
                {
                    Productions = new List<MofProductionAst>
                    {
                        new AssociationDeclarationAst(
                            new QualifierListAst(),
                            new IdentifierToken("GOLF_MemberLocker"),
                            null,
                            new List<IClassFeatureAst>()
                        )
                    }
                }.Build();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
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
                var expectedAst = new MofSpecificationAst.Builder {
                    Productions = new List<MofProductionAst> {
                        new AssociationDeclarationAst.Builder {
                            AssociationName = new IdentifierToken("GOLF_MemberLocker"),
                            SuperAssociation = new IdentifierToken("GOLF_Base"),
                            ClassFeatures = new List<IClassFeatureAst> {
                                new PropertyDeclarationAst.Builder {
                                    ReturnType = new IdentifierToken("GOLF_ClubMember"),
                                    ReturnTypeRef = new IdentifierToken(Constants.REF),
                                    PropertyName = new IdentifierToken("Member"),
                                }.Build(),
                                new PropertyDeclarationAst.Builder {
                                    ReturnType = new IdentifierToken("GOLF_Locker"),
                                    ReturnTypeRef = new IdentifierToken(Constants.REF),
                                    PropertyName = new IdentifierToken("Locker"),
                                }.Build(),
                                new PropertyDeclarationAst.Builder {
                                    ReturnType = new IdentifierToken("GOLF_Date"),
                                    PropertyName = new IdentifierToken("AssignedOnDate"),
                                }.Build()
                            }
                        }.Build()
                    }
                }.Build();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
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
                var expectedAst = new MofSpecificationAst.Builder
                {
                    Productions = new List<MofProductionAst> {
                        new AssociationDeclarationAst.Builder {
                            AssociationName = new IdentifierToken("GOLF_MemberLocker"),
                            ClassFeatures = new List<IClassFeatureAst> {
                                new PropertyDeclarationAst.Builder {
                                    ReturnType = new IdentifierToken("GOLF_ClubMember"),
                                    ReturnTypeRef = new IdentifierToken(Constants.REF),
                                    PropertyName = new IdentifierToken("Member"),
                                }.Build(),
                                new PropertyDeclarationAst.Builder {
                                    ReturnType = new IdentifierToken("GOLF_Locker"),
                                    ReturnTypeRef = new IdentifierToken(Constants.REF),
                                    PropertyName = new IdentifierToken("Locker"),
                                }.Build(),
                                new PropertyDeclarationAst.Builder {
                                    ReturnType = new IdentifierToken("GOLF_Date"),
                                    PropertyName = new IdentifierToken("AssignedOnDate"),
                                }.Build()
                            }
                        }.Build()
                    }
                }.Build();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
            }

        }

        #endregion

    }

}