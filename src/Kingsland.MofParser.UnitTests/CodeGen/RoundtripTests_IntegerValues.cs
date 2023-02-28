using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.6.1.1 Integer values

    public static class IntegerValueTests
    {

        [Test]
        public static void IntegerValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    Caption = 100;
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
                //     Caption = 100;
                .IdentifierToken("Caption")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.DecimalValue, 100)
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void PositiveIntegerValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    Caption = +100;
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
                //     Caption = +100;
                .IdentifierToken("Caption")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken("+100", IntegerKind.DecimalValue, 100)
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void NegativeIntegerValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    Caption = -100;
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
                //     Caption = -100;
                .IdentifierToken("Caption")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.DecimalValue, -100)
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
        public static void IntegerValuePropertiesInOtherBasesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                instance of GOLF_ClubMember
                {
                    MyBinaryValue1 = 101010b;
                    MyBinaryValue2 = 00101010b;
                    MyBinaryValue3 = +101010b;
                    MyBinaryValue4 = -101010b;
                    MyOctalValue1 = 0444444;
                    MyOctalValue2 = 000444444;
                    MyOctalValue3 = +0444444;
                    MyOctalValue4 = -0444444;
                    MyHexValue1 = 0xABC123;
                    MyHexValue2 = 0x00ABC123;
                    MyHexValue3 = +0xABC123;
                    MyHexValue4 = -0xABC123;
                    MyDecimalValue1 = 12345;
                    MyDecimalValue2 = +12345;
                    MyDecimalValue3 = -12345;
                    MyRealValue1 = 123.45;
                    MyRealValue2 = 00123.45;
                    MyRealValue3 = +123.45;
                    MyRealValue4 = -123.45;
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
                //     MyBinaryValue1 = 101010b;
                .IdentifierToken("MyBinaryValue1")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.BinaryValue, 0b101010)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyBinaryValue2 = 0101010b;
                .IdentifierToken("MyBinaryValue2")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken("00101010b", IntegerKind.BinaryValue, 0b0101010)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyBinaryValue2 = +101010b;
                .IdentifierToken("MyBinaryValue3")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken("+101010b", IntegerKind.BinaryValue, 0b101010)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyBinaryValue4 = -101010b;
                .IdentifierToken("MyBinaryValue4")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.BinaryValue, -0b101010)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyOctalValue1 = 0444444;
                .IdentifierToken("MyOctalValue1")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.OctalValue, Convert.ToInt32("0444444", 8))
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyOctalValue2 = 00444444;
                .IdentifierToken("MyOctalValue2")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken("000444444", IntegerKind.OctalValue, Convert.ToInt32("000444444", 8))
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyOctalValue3 = +000444444;
                .IdentifierToken("MyOctalValue3")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken("+0444444", IntegerKind.OctalValue, Convert.ToInt32("0444444", 8))
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyOctalValue4 = -0444444;
                .IdentifierToken("MyOctalValue4")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.OctalValue, -Convert.ToInt32("0444444", 8))
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyHexValue1 = 0xABC123;
                .IdentifierToken("MyHexValue1")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.HexValue, 0xABC123)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyHexValue1 = 0x00ABC123;
                .IdentifierToken("MyHexValue2")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken("0x00ABC123", IntegerKind.HexValue, 0x00ABC123)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyHexValue2 = +0xABC123;
                .IdentifierToken("MyHexValue3")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken("+0xABC123", IntegerKind.HexValue, 0x00ABC123)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyHexValue3 = -0xABC123;
                .IdentifierToken("MyHexValue4")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.HexValue, -0xABC123)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                // MyDecimalValue1 = 12345;
                .IdentifierToken("MyDecimalValue1")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.DecimalValue, 12345)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyDecimalValue2 = +12345;
                .IdentifierToken("MyDecimalValue2")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken("+12345", IntegerKind.DecimalValue, 12345)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyDecimalValue3 = -12345;
                .IdentifierToken("MyDecimalValue3")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.DecimalValue, -12345)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyRealValue1 = 123.45;
                .IdentifierToken("MyRealValue1")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .RealLiteralToken(123.45)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyRealValue2 = 00123.45;
                .IdentifierToken("MyRealValue2")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .RealLiteralToken("00123.45", 123.45)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyRealValue2 = +00123.45;
                .IdentifierToken("MyRealValue3")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .RealLiteralToken("+123.45", 123.45)
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     MyRealValue3 = -123.45;
                .IdentifierToken("MyRealValue4")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .RealLiteralToken(-123.45)
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
