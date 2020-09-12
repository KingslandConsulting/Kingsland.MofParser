using Kingsland.MofParser.Lexing;
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
                var actualTokens = Lexer.Lex(
                    SourceReader.From("0b")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue1b()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("1b")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue00000b()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("00000b")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue10000b()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("10000b")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue11111b()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("11111b")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            // octalValue

            [Test]
            public static void ShouldReadOctalValue00()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("00")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue01()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("01")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue00000()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("00000")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue01000()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("01000")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue01111()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("01111")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue04444()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("04444")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue07777()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("07777")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            // hexValue

            [Test]
            public static void ShouldReadHexValue0x0()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("0x0")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadHexValue0x0000()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("0x0000")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadHexValue0x8888()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("0x8888")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadHexValue0xabcd()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("0xabcd")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadHexValue0xABCD()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("0xABCD")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            // decimalValue

            [Test]
            public static void ShouldReadDecimalValue0()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("0")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadDecimalValue12345()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("12345")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadDecimalValuePlus12345()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("+12345")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadDecimalValueMinus12345()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("-12345")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadDecimalValue1234567890()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("1234567890")
                );
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
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

    }

}
