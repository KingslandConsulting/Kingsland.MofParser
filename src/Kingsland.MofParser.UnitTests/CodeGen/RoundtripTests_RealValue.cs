using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.1.1 Real values

        public static class RealValueTests
        {

            [Test]
            public static void RealValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 0.5;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_ClubMember
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_ClubMember")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Caption = 0.5;
                    .IdentifierToken("Caption")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .RealLiteralToken(0.5)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void PositiveRealValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = +0.5;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_ClubMember
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_ClubMember")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Caption = +0.5;
                    .IdentifierToken("Caption")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .RealLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "+0.5", 0.5
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void NegativeRealValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = -0.5;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_ClubMember
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_ClubMember")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Caption = +0.5;
                    .IdentifierToken("Caption")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .RealLiteralToken(-0.5)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithNoFractionShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 5.0;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_ClubMember
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_ClubMember")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Caption = 5.0;
                    .IdentifierToken("Caption")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .RealLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "5.0", 5.0
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithTrailingZerosShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 0.50;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_ClubMember
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_ClubMember")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Caption = 0.50;
                    .IdentifierToken("Caption")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .RealLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "0.50", 0.5
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithNoIntegerPartShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = .5;\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // instance of GOLF_ClubMember
                    .IdentifierToken("instance")
                    .WhitespaceToken(" ")
                    .IdentifierToken("of")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_ClubMember")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // Caption = .5;
                    .IdentifierToken("Caption")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .RealLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        ".5", 0.5
                    )
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