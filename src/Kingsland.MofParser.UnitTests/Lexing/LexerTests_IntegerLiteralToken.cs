using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using NUnit.Framework;
using System.Collections.Generic;

namespace Kingsland.MofParser.UnitTests.Lexing
{

    [TestFixture]
    public static partial class LexerTests
    {

        [TestFixture]
        public static class ReadIntegerLiteralTokenMethod
        {

            // binaryValue

            [Test]
            public static void ShouldReadBinaryValue0b()
            {
                var sourceText = "0b";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(1, 1, 2),
                            "0b"
                        ),
                        IntegerKind.BinaryValue, 0
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue1b()
            {
                var sourceText = "1b";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(1, 1, 2),
                            "1b"
                        ),
                        IntegerKind.BinaryValue, 1
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue00000b()
            {
                var sourceText = "00000b";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "00000b"
                        ),
                        IntegerKind.BinaryValue, 0
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue10000b()
            {
                var sourceText = "10000b";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "10000b"
                        ),
                        IntegerKind.BinaryValue, 16
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue11111b()
            {
                var sourceText = "11111b";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "11111b"
                        ),
                        IntegerKind.BinaryValue, 31
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            // octalValue

            [Test]
            public static void ShouldReadOctalValue00()
            {
                var sourceText = "00";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(1, 1, 2),
                            "00"
                        ),
                        IntegerKind.OctalValue, 0
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadOctalValue01()
            {
                var sourceText = "01";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(1, 1, 2),
                            "01"
                        ),
                        IntegerKind.OctalValue, 1
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadOctalValue00000()
            {
                var sourceText = "00000";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "00000"
                        ),
                        IntegerKind.OctalValue, 0
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadOctalValue01000()
            {
                var sourceText = "01000";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "01000"
                        ),
                        IntegerKind.OctalValue, 512
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadOctalValue01111()
            {
                var sourceText = "01111";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "01111"
                        ),
                        IntegerKind.OctalValue, 585
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadOctalValue04444()
            {
                var sourceText = "04444";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "04444"
                        ),
                        IntegerKind.OctalValue, 2340
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadOctalValue07777()
            {
                var sourceText = "07777";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "07777"
                        ),
                        IntegerKind.OctalValue, 4095
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            // hexValue

            [Test]
            public static void ShouldReadHexValue0x0()
            {
                var sourceText = "0x0";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(2, 1, 3),
                            "0x0"
                        ),
                        IntegerKind.HexValue, 0
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadHexValue0x0000()
            {
                var sourceText = "0x0000";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "0x0000"
                        ),
                        IntegerKind.HexValue, 0
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadHexValue0x8888()
            {
                var sourceText = "0x8888";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "0x8888"
                        ),
                        IntegerKind.HexValue, 34952
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadHexValue0xabcd()
            {
                var sourceText = "0xabcd";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "0xabcd"
                        ),
                        IntegerKind.HexValue, 43981
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadHexValue0xABCD()
            {
                var sourceText = "0xABCD";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "0xABCD"
                        ),
                        IntegerKind.HexValue, 43981
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            // decimalValue

            [Test]
            public static void ShouldReadDecimalValue0()
            {
                var sourceText = "0";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "0"
                        ),
                        IntegerKind.DecimalValue, 0
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadDecimalValue12345()
            {
                var sourceText = "12345";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "12345"
                        ),
                        IntegerKind.DecimalValue, 12345
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadDecimalValuePlus12345()
            {
                var sourceText = "+12345";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "+12345"
                        ),
                        IntegerKind.DecimalValue, 12345
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadDecimalValueMinus12345()
            {
                var sourceText = "-12345";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "-12345"
                        ),
                        IntegerKind.DecimalValue, -12345
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadDecimalValue1234567890()
            {
                var sourceText = "1234567890";
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(9, 1, 10),
                            "1234567890"
                        ),
                        IntegerKind.DecimalValue, 1234567890
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

        }

    }

}
