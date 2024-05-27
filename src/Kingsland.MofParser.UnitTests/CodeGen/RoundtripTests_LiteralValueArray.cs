using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.6.1 Primitive type value

    public static class LiteralValueArrayTests
    {

        [Test]
        public static void LiteralValueArrayWithNoItemsShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = {};
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
                //     LastPaymentDate = { };
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst([
                new InstanceValueDeclarationAst(
                    new("instance"), new("of"), new("GOLF_ClubMember"), null, null,
                    new([
                        new(new("LastPaymentDate"), new LiteralValueArrayAst([]))
                    ]),
                    new()
                )
            ]);
            var expectedModel = new Module([
                new Instance(
                    "GOLF_ClubMember",
                    [
                        new("LastPaymentDate", new LiteralValueArray())
                    ]
                )
            ]);
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModel);
        }

        [Test]
        public static void LiteralValueArrayWithOneItemShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = {1};
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
                //     LastPaymentDate = {1};
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            var expectedAst = new MofSpecificationAst([
                new InstanceValueDeclarationAst(
                    new("instance"), new("of"), new("GOLF_ClubMember"), null, null,
                    new([
                        new(new("LastPaymentDate"), new LiteralValueArrayAst(
                            new IntegerValueAst(new(IntegerKind.DecimalValue, 1))
                        ))
                    ]),
                    new()
                )
            ]);
            var expectedModel = new Module([
                new Instance(
                    "GOLF_ClubMember",
                    [
                        new(
                            "LastPaymentDate",
                            new LiteralValueArray([
                                1
                            ])
                        )
                    ]
                )
            ]);
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModel);
        }

        [Test]
        public static void LiteralValueArrayWithMultipleItemsShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = {1, 2};
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
                //     LastPaymentDate = {1, 2};
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BlockOpenToken()
                .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
                .CommaToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.DecimalValue, 2)
                .BlockCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

    }

    #endregion

}
