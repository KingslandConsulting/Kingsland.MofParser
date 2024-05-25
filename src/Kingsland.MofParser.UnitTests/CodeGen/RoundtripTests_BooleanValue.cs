using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.6.1.5 Boolean value

    public static class BooleanValueTests
    {

        [Test]
        public static void BooleanValueAstShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of myType as $Alias00000070
                {
                    Reference = TRUE;
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // instance of myType as $Alias00000070
                .IdentifierToken("instance")
                .WhitespaceToken(" ")
                .IdentifierToken("of")
                .WhitespaceToken(" ")
                .IdentifierToken("myType")
                .WhitespaceToken(" ")
                .IdentifierToken("as")
                .WhitespaceToken(" ")
                .AliasIdentifierToken("Alias00000070")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Reference = TRUE;
                .IdentifierToken("Reference")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BooleanLiteralToken("TRUE", true)
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst.Builder
            {
                Productions = [
                    new InstanceValueDeclarationAst.Builder {
                        Instance = new IdentifierToken("instance"),
                        Of = new IdentifierToken("of"),
                        TypeName = new IdentifierToken("myType"),
                        As = new IdentifierToken("as"),
                        Alias = new AliasIdentifierToken("Alias00000070"),
                        PropertyValues = new PropertyValueListAst.Builder {
                            PropertySlots = [
                                new PropertySlotAst(
                                    new("Reference"),
                                    new BooleanValueAst(
                                        new BooleanLiteralToken("TRUE", true)
                                    )
                                )
                            ]
                        }.Build(),
                        StatementEnd = new StatementEndToken()
                    }.Build()
                ]
            }.Build();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst);
        }

    }

    #endregion

}
