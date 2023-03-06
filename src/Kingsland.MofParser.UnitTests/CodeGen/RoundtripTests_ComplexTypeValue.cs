using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.9 Complex type value

    public static class ComplexTypeValueTests
    {

        [Test]
        public static void ComplexTypeValueWithComplexValuePropertyShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = $MyAliasIdentifier;
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_ClubMember
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     LastPaymentDate = $MyAliasIdentifier;
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .AliasIdentifierToken("MyAliasIdentifier")
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst.Builder {
                Productions = new List<MofProductionAst> {
                    new InstanceValueDeclarationAst.Builder {
                        Instance = new IdentifierToken("instance"),
                        Of = new IdentifierToken("of"),
                        TypeName = new IdentifierToken("GOLF_ClubMember"),
                        PropertyValues = new PropertyValueListAst(
                            new List<PropertySlotAst> {
                                new PropertySlotAst.Builder {
                                     PropertyName = new IdentifierToken("LastPaymentDate"),
                                     PropertyValue = new ComplexValueAst.Builder {
                                         Alias = new AliasIdentifierToken("MyAliasIdentifier")
                                     }.Build()
                                }.Build()
                            }
                        ),
                        StatementEnd = new StatementEndToken()
                    }.Build()
                }
            }.Build();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

        [Test]
        public static void ComplexTypeValueWithComplexValueArrayPropertyShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = {$MyAliasIdentifier};
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of GOLF_ClubMember
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_ClubMember")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     LastPaymentDate = $MyAliasIdentifier;
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .AliasIdentifierToken("MyAliasIdentifier")
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst.Builder {
                Productions = new List<MofProductionAst> {
                    new InstanceValueDeclarationAst.Builder {
                        Instance = new IdentifierToken("instance"),
                        Of = new IdentifierToken("of"),
                        TypeName = new IdentifierToken("GOLF_ClubMember"),
                        PropertyValues = new PropertyValueListAst(
                            new List<PropertySlotAst> {
                                new PropertySlotAst.Builder {
                                     PropertyName = new IdentifierToken("LastPaymentDate"),
                                     PropertyValue = new ComplexValueArrayAst(
                                         new List<ComplexValueAst> {
                                             new ComplexValueAst.Builder {
                                                Alias = new AliasIdentifierToken("MyAliasIdentifier")
                                            }.Build()
                                         }
                                     )
                                }.Build()
                            }
                        ),
                        StatementEnd = new StatementEndToken()
                    }.Build()
                }
            }.Build();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

    }

    #endregion

}
