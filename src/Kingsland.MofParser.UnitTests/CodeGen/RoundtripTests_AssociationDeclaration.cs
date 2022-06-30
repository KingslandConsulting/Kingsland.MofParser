using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.3 Association declaration

    public static class AssociationDeclarationTests
    {

        [Test]
        public static void EmptyAssociationDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                association GOLF_MemberLocker
                {
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // association GOLF_MemberLocker
                .IdentifierToken("association")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_MemberLocker")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline)
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
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                association GOLF_MemberLocker : GOLF_Base
                {
                    GOLF_ClubMember REF Member;
                    GOLF_Locker REF Locker;
                    GOLF_Date AssignedOnDate;
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // association GOLF_MemberLocker : GOLF_Base
                .IdentifierToken("association")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_MemberLocker")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Base")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     GOLF_ClubMember REF Member;
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(" ")
                .IdentifierToken("REF")
                .WhitespaceToken(" ")
                .IdentifierToken("Member")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     GOLF_Locker REF Locker;
                .IdentifierToken("GOLF_Locker")
                .WhitespaceToken(" ")
                .IdentifierToken("REF")
                .WhitespaceToken(" ")
                .IdentifierToken("Locker")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     GOLF_Date AssignedOnDate;
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(" ")
                .IdentifierToken("AssignedOnDate")
                .StatementEndToken()
                .WhitespaceToken(newline)
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
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                association GOLF_MemberLocker
                {
                    GOLF_ClubMember REF Member;
                    GOLF_Locker REF Locker;
                    GOLF_Date AssignedOnDate;
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // association GOLF_MemberLocker
                .IdentifierToken("association")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_MemberLocker")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     GOLF_ClubMember REF Member;
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(" ")
                .IdentifierToken("REF")
                .WhitespaceToken(" ")
                .IdentifierToken("Member")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     GOLF_Locker REF Locker;
                .IdentifierToken("GOLF_Locker")
                .WhitespaceToken(" ")
                .IdentifierToken("REF")
                .WhitespaceToken(" ")
                .IdentifierToken("Locker")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     GOLF_Date AssignedOnDate
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(" ")
                .IdentifierToken("AssignedOnDate")
                .StatementEndToken()
                .WhitespaceToken(newline)
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
