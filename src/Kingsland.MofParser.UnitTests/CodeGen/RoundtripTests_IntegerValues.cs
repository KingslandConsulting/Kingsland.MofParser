using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;
using NUnit.Framework;
using System;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.1.1 Integer values

        public static class IntegerValueTests
        {

            [Test]
            public static void IntegerValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 100;\r\n" +
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
                    // Caption = 100;
                    .IdentifierToken("Caption")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.DecimalValue, 100)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void PositiveIntegerValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = +100;\r\n" +
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
                    // Caption = 100;
                    .IdentifierToken("Caption")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "+100",
                        IntegerKind.DecimalValue, 100
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
            public static void NegativeIntegerValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = -100;\r\n" +
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
                    // Caption = 100;
                    .IdentifierToken("Caption")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.DecimalValue, -100)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void IntegerValuePropertiesInOtherBasesShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tMyBinaryValue1 = 101010b;\r\n" +
                    "\tMyBinaryValue2 = 00101010b;\r\n" +
                    "\tMyBinaryValue3 = +101010b;\r\n" +
                    "\tMyBinaryValue4 = -101010b;\r\n" +
                    "\tMyOctalValue1 = 0444444;\r\n" +
                    "\tMyOctalValue2 = 000444444;\r\n" +
                    "\tMyOctalValue3 = +0444444;\r\n" +
                    "\tMyOctalValue4 = -0444444;\r\n" +
                    "\tMyHexValue1 = 0xABC123;\r\n" +
                    "\tMyHexValue2 = 0x00ABC123;\r\n" +
                    "\tMyHexValue3 = +0xABC123;\r\n" +
                    "\tMyHexValue4 = -0xABC123;\r\n" +
                    "\tMyDecimalValue1 = 12345;\r\n" +
                    "\tMyDecimalValue2 = +12345;\r\n" +
                    "\tMyDecimalValue3 = -12345;\r\n" +
                    "\tMyRealValue1 = 123.45;\r\n" +
                    "\tMyRealValue2 = 00123.45;\r\n" +
                    "\tMyRealValue3 = +123.45;\r\n" +
                    "\tMyRealValue4 = -123.45;\r\n" +
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
                    // MyBinaryValue1 = 101010b;
                    .IdentifierToken("MyBinaryValue1")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.BinaryValue, 0b101010)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyBinaryValue2 = 0101010b;
                    .IdentifierToken("MyBinaryValue2")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "00101010b",
                        IntegerKind.BinaryValue, 0b0101010
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyBinaryValue2 = +101010b;
                    .IdentifierToken("MyBinaryValue3")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "+101010b",
                        IntegerKind.BinaryValue, 0b101010
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyBinaryValue4 = -101010b;
                    .IdentifierToken("MyBinaryValue4")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.BinaryValue, -0b101010)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyOctalValue1 = 0444444;
                    .IdentifierToken("MyOctalValue1")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.OctalValue, Convert.ToInt32("0444444", 8))
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyOctalValue2 = 00444444;
                    .IdentifierToken("MyOctalValue2")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "000444444",
                        IntegerKind.OctalValue, Convert.ToInt32("000444444", 8)
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyOctalValue3 = +000444444;
                    .IdentifierToken("MyOctalValue3")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "+0444444",
                        IntegerKind.OctalValue, Convert.ToInt32("0444444", 8)
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyOctalValue4 = -0444444;
                    .IdentifierToken("MyOctalValue4")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.OctalValue, -Convert.ToInt32("0444444", 8))
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyHexValue1 = 0xABC123;
                    .IdentifierToken("MyHexValue1")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.HexValue, 0xABC123)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyHexValue1 = 0x00ABC123;
                    .IdentifierToken("MyHexValue2")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "0x00ABC123",
                        IntegerKind.HexValue, 0x00ABC123
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyHexValue2 = +0xABC123;
                    .IdentifierToken("MyHexValue3")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "+0xABC123",
                        IntegerKind.HexValue, 0x00ABC123
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyHexValue3 = -0xABC123;
                    .IdentifierToken("MyHexValue4")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.HexValue, -0xABC123)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyDecimalValue1 = 12345;
                    .IdentifierToken("MyDecimalValue1")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.DecimalValue, 12345)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyDecimalValue2 = +12345;
                    .IdentifierToken("MyDecimalValue2")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "+12345",
                        IntegerKind.DecimalValue, 12345
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyDecimalValue3 = -12345;
                    .IdentifierToken("MyDecimalValue3")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .IntegerLiteralToken(IntegerKind.DecimalValue, -12345)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyRealValue1 = 123.45;
                    .IdentifierToken("MyRealValue1")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .RealLiteralToken(123.45)
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyRealValue2 = 00123.45;
                    .IdentifierToken("MyRealValue2")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .RealLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "00123.45",
                        123.45
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyRealValue2 = +00123.45;
                    .IdentifierToken("MyRealValue3")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .RealLiteralToken(
                        SourcePosition.Empty,
                        SourcePosition.Empty,
                        "+123.45",
                        123.45
                    )
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // MyRealValue3 = -123.45;
                    .IdentifierToken("MyRealValue4")
                    .WhitespaceToken(" ")
                    .EqualsOperatorToken()
                    .WhitespaceToken(" ")
                    .RealLiteralToken(-123.45)
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