﻿using Kingsland.MofParser.Lexing;
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
        public static class ReadRealLiteralTokenMethod
        {

            [Test]
            public static void ShouldReadRealValue0_0()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("0.0")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new RealLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(2, 1, 3),
                            "0.0"
                        ),
                        0
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValue123_45()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("123.45")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new RealLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "123.45"
                        ),
                        123.45
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValuePlus123_45()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("+123.45")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new RealLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(6, 1, 7),
                            "+123.45"
                        ),
                        123.45
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValueMinus123_45()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("-123.45")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new RealLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(6, 1, 7),
                            "-123.45"
                        ),
                        -123.45
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValue1234567890_00()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("1234567890.00")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new RealLiteralToken(
                        new SourceExtent(
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(12, 1, 13),
                            "1234567890.00"
                        ),
                        1234567890.00
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValue_45()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From(".45")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new RealLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(2, 1, 3),
                            ".45"
                        ),
                        0.45
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValuePlus_45()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("+.45")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new RealLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "+.45"
                        ),
                        0.45
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValueMinus_45()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("-.45")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new RealLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "-.45"
                        ),
                        -0.45
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

    }

}
