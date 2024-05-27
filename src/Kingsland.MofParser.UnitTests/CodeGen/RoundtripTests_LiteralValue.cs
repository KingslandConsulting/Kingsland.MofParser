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

    public static class LiteralValueTests
    {

        [Test]
        public static void IntegerLiteralValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = 1;
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
                //     LastPaymentDate = 1;
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
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
                        new(new("LastPaymentDate"), new IntegerValueAst(new(IntegerKind.DecimalValue, 1)))
                    ]),
                    new()
                )
            ]);
            var expectedModel = new Module([
                new Instance(
                    "GOLF_ClubMember",
                    [
                        new("LastPaymentDate", 1)
                    ]
                )
            ]);
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModel);
        }

        [Test]
        public static void RealLiteralValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = 0.5;
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
                //     LastPaymentDate = 0.5;
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .RealLiteralToken(0.5)
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
                        new(new("LastPaymentDate"), new RealValueAst(new(0.5)))
                    ]),
                    new()
                )
            ]);
            var expectedModel = new Module([
                new Instance(
                    "GOLF_ClubMember",
                    [
                        new("LastPaymentDate", 0.5)
                    ]
                )
            ]);
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModel);
        }

        [Test]
        public static void BooleanLiteralValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = true;
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
                //     LastPaymentDate = true;
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .BooleanLiteralToken(true)
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
                        new(new("LastPaymentDate"), new BooleanValueAst(new(true)))
                    ]),
                    new()
                )
            ]);
            var expectedModel = new Module([
                new Instance(
                    "GOLF_ClubMember",
                    [
                        new("LastPaymentDate", true)
                    ]
                )
            ]);
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModel);
        }

        [Test]
        public static void NullLiteralValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = null;
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
                //     LastPaymentDate = null;
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .NullLiteralToken()
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
                        new(new("LastPaymentDate"), new NullValueAst(new()))
                    ]),
                    new()
                )
            ]);
            var expectedModel = new Module([
                new Instance(
                    "GOLF_ClubMember",
                    [
                        new("LastPaymentDate", NullValue.Null)
                    ]
                )
            ]);
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModel);
        }

        [Test]
        public static void StringLiteralValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    LastPaymentDate = ""aaa"";
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
                //     LastPaymentDate = "aaa";
                .IdentifierToken("LastPaymentDate")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .StringLiteralToken("aaa")
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
                        new(new("LastPaymentDate"), new StringValueAst(new StringLiteralToken("aaa"), "aaa"))
                    ]),
                    new()
                )
            ]);
            var expectedModel = new Module([
                new Instance(
                    "GOLF_ClubMember",
                    [
                        new("LastPaymentDate", "aaa")
                    ]
                )
            ]);
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens, expectedAst, expectedModel);
        }

    }

    #endregion

}
