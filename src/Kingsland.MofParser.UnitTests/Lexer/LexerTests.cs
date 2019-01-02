using Kingsland.MofParser.Source;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Kingsland.MofParser.UnitTests.Lexer
{

    [TestFixture]
    public static class LexerTests
    {

        #region Symbols

        [TestFixture]
        public static class ReadAttributeCloseTokenMethod
        {

            [Test]
            public static void ShouldReadAttributeCloseToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("]")
                );
                var expectedTokens = new List<Token> {
                    new AttributeCloseToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "]"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadAttributeOpenTokenMethod
        {

            [Test]
            public static void ShouldReadAttributeOpenToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("[")
                );
                var expectedTokens = new List<Token> {
                    new AttributeOpenToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "["
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadBlockCloseTokenMethod
        {

            [Test]
            public static void ShouldReadBlockCloseToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("}")
                );
                var expectedTokens = new List<Token> {
                    new BlockCloseToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "}"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadBlockOpenTokenMethod
        {

            [Test]
            public static void ShouldReaBlockOpenToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("{")
                );
                var expectedTokens = new List<Token> {
                    new BlockOpenToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "{"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadColonTokenMethod
        {

            [Test]
            public static void ShouldReadColonToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(":")
                );
                var expectedTokens = new List<Token> {
                    new ColonToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            ":"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadCommaTokenMethod
        {

            [Test]
            public static void ShouldReadCommaToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(",")
                );
                var expectedTokens = new List<Token> {
                    new CommaToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            ","
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadEqualsOperatorTokenMethod
        {

            [Test]
            public static void ShouldReadEqualsOperatorToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("=")
                );
                var expectedTokens = new List<Token> {
                    new EqualsOperatorToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "="
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadParenthesesCloseTokenMethod
        {

            [Test]
            public static void ShouldReadEqualsOperatorToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(")")
                );
                var expectedTokens = new List<Token> {
                    new ParenthesesCloseToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            ")"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadParenthesesOpenTokenMethod
        {

            [Test]
            public static void ShouldReadParenthesesOpenToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("(")
                );
                var expectedTokens = new List<Token> {
                    new ParenthesesOpenToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "("
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadStatementEndTokenMethod
        {

            [Test]
            public static void ShouldReadStatementEndToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(";")
                );
                var expectedTokens = new List<Token> {
                    new StatementEndToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            ";"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        #endregion

        [TestFixture]
        public static class EmptyFileTests
        {

            [Test]
            public static void ShouldReadEmptyFile()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(string.Empty)
                );
                var expectedTokens = new List<Token>();
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadWhitespaceTokenMethod
        {

            [Test]
            public static void ShouldReadSpaceWhitespaceToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("     ")
                );
                var expectedTokens = new List<Token> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "     "
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadTabWhitespaceToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("\t\t\t\t\t")
                );
                var expectedTokens = new List<Token> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "\t\t\t\t\t"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadCrWhitespaceToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("\r\r\r\r\r")
                );
                var expectedTokens = new List<Token> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 5, 1),
                            "\r\r\r\r\r"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadLfWhitespaceToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("\n\n\n\n\n")
                );
                var expectedTokens = new List<Token> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 5, 1),
                            "\n\n\n\n\n"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadCrLfWhitespaceToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("\r\n\r\n\r\n\r\n\r\n")
                );
                var expectedTokens = new List<Token> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(9, 5, 2),
                            "\r\n\r\n\r\n\r\n\r\n"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMixedWhitespaceToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("     \t\t\t\t\t\r\r\r\r\r\n\n\n\n\n\r\n\r\n\r\n\r\n\r\n")
                );
                var expectedTokens = new List<Token> {
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(29, 14, 2),
                            "     \t\t\t\t\t\r\r\r\r\r\n\n\n\n\n\r\n\r\n\r\n\r\n\r\n"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadCommentTokenMethod
        {

            [Test]
            public static void ShouldReadSingleLineEofCommentToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("// single line comment")
                );
                var expectedTokens = new List<Token> {
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(21, 1, 22),
                            "// single line comment"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadSingleLineEolCommentToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("// single line comment\r\n")
                );
                var expectedTokens = new List<Token> {
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(21, 1, 22),
                            "// single line comment"
                        )
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(22, 1, 23),
                            new SourcePosition(23, 1, 24),
                            "\r\n"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMultilineEofCommentToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(
                        "/*\r\n" +
                        "@TargetNode='MyServer'\r\n" +
                        "@GeneratedBy=mike.clayton\r\n" +
                        "@GenerationDate=07/19/2014 10:37:04\r\n" +
                        "@GenerationHost=MyDesktop\r\n" +
                        "*/"
                    )
                );
                var expectedTokens = new List<Token> {
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(120, 6, 2),
                            "/*\r\n" +
                            "@TargetNode='MyServer'\r\n" +
                            "@GeneratedBy=mike.clayton\r\n" +
                            "@GenerationDate=07/19/2014 10:37:04\r\n" +
                            "@GenerationHost=MyDesktop\r\n" +
                            "*/"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMultilineUnclosedCommentToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(
                        "/*\r\n" +
                        "@TargetNode='MyServer'\r\n" +
                        "@GeneratedBy=mike.clayton\r\n" +
                        "@GenerationDate=07/19/2014 10:37:04\r\n" +
                        "@GenerationHost=MyDesktop\r\n"
                    )
                );
                var expectedTokens = new List<Token> {
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(118, 5, 27),
                            "/*\r\n" +
                            "@TargetNode='MyServer'\r\n" +
                            "@GeneratedBy=mike.clayton\r\n" +
                            "@GenerationDate=07/19/2014 10:37:04\r\n" +
                            "@GenerationHost=MyDesktop\r\n"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMultilineInlineAsterisks()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(
                        "/*************\r\n" +
                        "@TargetNode='MyServer'\r\n" +
                        "@GeneratedBy=mike.clayton\r\n" +
                        "@GenerationDate=07/19/2014 10:37:04\r\n" +
                        "@GenerationHost=MyDesktop\r\n" +
                        "*************/"
                    )
                );
                var expectedTokens = new List<Token> {
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(144, 6, 14),
                            "/*************\r\n" +
                            "@TargetNode='MyServer'\r\n" +
                            "@GeneratedBy=mike.clayton\r\n" +
                            "@GenerationDate=07/19/2014 10:37:04\r\n" +
                            "@GenerationHost=MyDesktop\r\n" +
                            "*************/"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMultilineMultiple()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(
                        "/*\r\n" +
                        "@TargetNode='MyServer'\r\n" +
                        "@GeneratedBy=mike.clayton\r\n" +
                        "@GenerationDate=07/19/2014 10:37:04\r\n" +
                        "@GenerationHost=MyDesktop\r\n" +
                        "*//*\r\n" +
                        "@TargetNode='MyServer2'\r\n" +
                        "@GeneratedBy=mike.clayton\r\n" +
                        "@GenerationDate=07/19/2014 10:37:04\r\n" +
                        "@GenerationHost=MyDesktop\r\n" +
                        "*/"
                    )
                );
                var expectedTokens = new List<Token> {
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(120, 6, 2),
                            "/*\r\n" +
                            "@TargetNode='MyServer'\r\n" +
                            "@GeneratedBy=mike.clayton\r\n" +
                            "@GenerationDate=07/19/2014 10:37:04\r\n" +
                            "@GenerationHost=MyDesktop\r\n" +
                            "*/"
                        )
                    ),
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(121, 6, 3),
                            new SourcePosition(242, 11, 2),
                            "/*\r\n" +
                            "@TargetNode='MyServer2'\r\n" +
                            "@GeneratedBy=mike.clayton\r\n" +
                            "@GenerationDate=07/19/2014 10:37:04\r\n" +
                            "@GenerationHost=MyDesktop\r\n" +
                            "*/"
                        )
                    )

                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadExample1CommentToken()
            {
                // see DSP0221_3.0.1.pdf "5.4 Comments"
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("Integer MyProperty; // This is an example of a single-line comment")
                );
                var expectedTokens = new List<Token> {
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(6, 1, 7),
                            "Integer"
                        ),
                        "Integer"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(7, 1, 8),
                            new SourcePosition(7, 1, 8),
                            " "
                        )
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(8, 1, 9),
                            new SourcePosition(17, 1, 18),
                            "MyProperty"
                        ),
                        "MyProperty"
                    ),
                    new StatementEndToken(
                        new SourceExtent
                        (
                            new SourcePosition(18, 1, 19),
                            new SourcePosition(18, 1, 19),
                            ";"
                        )
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(19, 1, 20),
                            new SourcePosition(19, 1, 20),
                            " "
                        )
                    ),
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(20, 1, 21),
                            new SourcePosition(65, 1, 66),
                            "// This is an example of a single-line comment"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadExample2CommentToken()
            {
                // see DSP0221_3.0.1.pdf "5.4 Comments"
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(
                        "/* example of a comment between property definition tokens and a multi-line comment */\r\n" +
                        "Integer /* 16-bit integer property */ MyProperty; /* and a multi-line\r\n" +
                        "                        comment */"
                    )
                );
                var expectedTokens = new List<Token> {
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(85, 1, 86),
                            "/* example of a comment between property definition tokens and a multi-line comment */"
                        )
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(86, 1, 87),
                            new SourcePosition(87, 1, 88),
                            "\r\n"
                        )
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(88, 2, 1),
                            new SourcePosition(94, 2, 7),
                            "Integer"
                        ),
                        "Integer"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(95, 2, 8),
                            new SourcePosition(95, 2, 8),
                            " "
                        )
                    ),
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(96, 2, 9),
                            new SourcePosition(124, 2, 37),
                            "/* 16-bit integer property */"
                        )
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(125, 2, 38),
                            new SourcePosition(125, 2, 38),
                            " "
                        )
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(126, 2, 39),
                            new SourcePosition(135, 2, 48),
                            "MyProperty"
                        ),
                        "MyProperty"
                    ),
                    new StatementEndToken(
                        new SourceExtent
                        (
                            new SourcePosition(136, 2, 49),
                            new SourcePosition(136, 2, 49),
                            ";"
                        )
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(137, 2, 50),
                            new SourcePosition(137, 2, 50),
                            " "
                        )
                    ),
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(138, 2, 51),
                            new SourcePosition(192, 3, 34),
                            "/* and a multi-line\r\n" +
                            "                        comment */"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadBooleanLiteralTokenMethod
        {

            [Test]
            public static void ShouldReadLowerCaseFalseToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("false")
                );
                var expectedTokens = new List<Token> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "false"
                        ),
                        false
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMixedCaseFalseToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("False")
                );
                var expectedTokens = new List<Token> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "False"
                        ),
                        false
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadUpperCaseFalseToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("FALSE")
                );
                var expectedTokens = new List<Token> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "FALSE"
                        ),
                        false
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadLowerCaseTrueToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("true")
                );
                var expectedTokens = new List<Token> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "true"
                        ),
                        true
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMixedCaseTrueToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("True")
                );
                var expectedTokens = new List<Token> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "True"
                        ),
                        true
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadUpperCaseTrueToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("TRUE")
                );
                var expectedTokens = new List<Token> {
                    new BooleanLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "TRUE"
                        ),
                        true
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadNullLiteralTokenMethod
        {

            [Test]
            public static void ShouldReadLowerCaseNullToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("null")
                );
                var expectedTokens = new List<Token> {
                    new NullLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "null"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMixedCaseNullToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("Null")
                );
                var expectedTokens = new List<Token> {
                    new NullLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "Null"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadUpperCaseNullToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("NULL")
                );
                var expectedTokens = new List<Token> {
                    new NullLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(3, 1, 4),
                            "NULL"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadPragmaTokenMethod
        {

            [Test]
            public static void ShouldReadLowerCasePragmaToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("#pragma")
                );
                var expectedTokens = new List<Token> {
                    new PragmaToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(6, 1, 7),
                            "#pragma"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadMixedCasePragmaToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("#Pragma")
                );
                var expectedTokens = new List<Token> {
                    new PragmaToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(6, 1, 7),
                            "#Pragma"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadUpperCasePragmaToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("#PRAGMA")
                );
                var expectedTokens = new List<Token> {
                    new PragmaToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(6, 1, 7),
                            "#PRAGMA"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadIdentifierTokenMethod
        {

            [Test]
            public static void ShouldReadIdentifierToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From
                    (
                        "myIdentifier\r\n" +
                        "myIdentifier2"
                    )
                );
                var expectedTokens = new List<Token> {
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(11, 1, 12),
                            "myIdentifier"
                        ),
                        "myIdentifier"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(12, 1, 13),
                            new SourcePosition(13, 1, 14),
                            "\r\n"
                        )
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(14, 2, 1),
                            new SourcePosition(26, 2, 13),
                            "myIdentifier2"
                        ),
                        "myIdentifier2"
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadAliasIdentifierTokenMethod
        {

            [Test]
            public static void ShouldReadAliasIdentifierToken()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From
                    (
                        "$myAliasIdentifier\r\n" +
                        "$myAliasIdentifier2"
                    )
                );
                var expectedTokens = new List<Token> {
                    new AliasIdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(17, 1, 18),
                            "$myAliasIdentifier"
                        ),
                        "myAliasIdentifier"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(18, 1, 19),
                            new SourcePosition(19, 1, 20),
                            "\r\n"
                        )
                    ),
                    new AliasIdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(20, 2, 1),
                            new SourcePosition(38, 2, 19),
                            "$myAliasIdentifier2"
                        ),
                        "myAliasIdentifier2"
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadInstanceWithAliasIdentifier()
            {
                // test case for https://github.com/mikeclayton/MofParser/issues/4
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From
                    (
                        "instance of cTentacleAgent as $cTentacleAgent1ref\r\n" +
                        "{\r\n" +
                        "};"
                    )
                );
                var expectedTokens = new List<Token>
                {
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(7, 1, 8),
                            "instance"
                        ),
                        "instance"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(8, 1, 9),
                            new SourcePosition(8, 1, 9),
                            " "
                        )
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(9, 1, 10),
                            new SourcePosition(10, 1, 11),
                            "of"
                        ),
                        "of"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(11, 1, 12),
                            new SourcePosition(11, 1, 12),
                            " "
                        )
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(12, 1, 13),
                            new SourcePosition(25, 1, 26),
                            "cTentacleAgent"
                        ),
                        "cTentacleAgent"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(26, 1, 27),
                            new SourcePosition(26, 1, 27),
                            " "
                        )
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(27, 1, 28),
                            new SourcePosition(28, 1, 29),
                            "as"
                        ),
                        "as"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(29, 1, 30),
                            new SourcePosition(29, 1, 30),
                            " "
                        )
                    ),
                    new AliasIdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(30, 1, 31),
                            new SourcePosition(48, 1, 49),
                            "$cTentacleAgent1ref"
                        ),
                        "cTentacleAgent1ref"
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(49, 1, 50),
                            new SourcePosition(50, 1, 51),
                            "\r\n"
                        )
                    ),
                    new BlockOpenToken(
                        new SourceExtent
                        (
                            new SourcePosition(51, 2, 1),
                            new SourcePosition(51, 2, 1),
                            "{"
                        )
                    ),
                    new WhitespaceToken(
                        new SourceExtent
                        (
                            new SourcePosition(52, 2, 2),
                            new SourcePosition(53, 2, 3),
                            "\r\n"
                        )
                    ),
                    new BlockCloseToken(
                        new SourceExtent
                        (
                            new SourcePosition(54, 3, 1),
                            new SourcePosition(54, 3, 1),
                            "}"
                        )
                    ),
                    new StatementEndToken(
                        new SourceExtent
                        (
                            new SourcePosition(55, 3, 2),
                            new SourcePosition(55, 3, 2),
                            ";"
                        )
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadIntegerLiteralTokenMethod
        {

            // binaryValue

            [Test]
            public static void ShouldReadBinaryValue0b()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("0b")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(1, 1, 2),
                            "0b"
                        ),
                        0
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue1b()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("1b")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(1, 1, 2),
                            "1b"
                        ),
                        1
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue00000b()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("00000b")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "00000b"
                        ),
                        0
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue10000b()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("10000b")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "10000b"
                        ),
                        16
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadBinaryValue11111b()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("11111b")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "11111b"
                        ),
                        31
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            // octalValue

            [Test]
            public static void ShouldReadOctalValue00()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("00")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(1, 1, 2),
                            "00"
                        ),
                        0
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue01()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("01")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(1, 1, 2),
                            "01"
                        ),
                        1
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue00000()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("00000")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "00000"
                        ),
                        0
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue01000()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("01000")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "01000"
                        ),
                        512
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue01111()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("01111")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "01111"
                        ),
                        585
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue04444()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("04444")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "04444"
                        ),
                        2340
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadOctalValue07777()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("07777")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "07777"
                        ),
                        4095
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            // hexValue

            [Test]
            public static void ShouldReadHexValue0x0()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("0x0")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(2, 1, 3),
                            "0x0"
                        ),
                        0
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadHexValue0x0000()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("0x0000")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "0x0000"
                        ),
                        0
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadHexValue0x8888()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("0x8888")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "0x8888"
                        ),
                        34952
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadHexValue0xabcd()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("0xabcd")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "0xabcd"
                        ),
                        43981
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadHexValue0xABCD()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("0xABCD")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "0xABCD"
                        ),
                        43981
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            // decimalValue

            [Test]
            public static void ShouldReadDecimalValue0()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("0")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(0, 1, 1),
                            "0"
                        ),
                        0
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadDecimalValue12345()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("12345")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "12345"
                        ),
                        12345
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadDecimalValuePlus12345()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("+12345")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "+12345"
                        ),
                        12345
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadDecimalValueMinus12345()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("-12345")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(5, 1, 6),
                            "-12345"
                        ),
                        -12345
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadDecimalValue1234567890()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("1234567890")
                );
                var expectedTokens = new List<Token> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(9, 1, 10),
                            "1234567890"
                        ),
                        1234567890
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadRealLiteralTokenMethod
        {

            [Test]
            public static void ShouldReadRealValue0_0()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("0.0")
                );
                var expectedTokens = new List<Token> {
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
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValue123_45()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("123.45")
                );
                var expectedTokens = new List<Token> {
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
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValuePlus123_45()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("+123.45")
                );
                var expectedTokens = new List<Token> {
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
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValueMinus123_45()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("-123.45")
                );
                var expectedTokens = new List<Token> {
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
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValue1234567890_00()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("1234567890.00")
                );
                var expectedTokens = new List<Token> {
                    new RealLiteralToken(
                        new SourceExtent(
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(12, 1, 13),
                            "1234567890.00"
                        ),
                        1234567890.00
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValue_45()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(".45")
                );
                var expectedTokens = new List<Token> {
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
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValuePlus_45()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("+.45")
                );
                var expectedTokens = new List<Token> {
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
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadRealValueMinus_45()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("-.45")
                );
                var expectedTokens = new List<Token> {
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
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class ReadStringLiteralTokenMethod
        {

            [Test]
            public static void ShouldReadEmptyString()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("\"\"")
                );
                var expectedTokens = new List<Token> {
                    new StringLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(1, 1, 2),
                            "\"\""
                        ),
                        string.Empty
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadBasicString()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From("\"my string literal\"")
                );
                var expectedTokens = new List<Token> {
                    new StringLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(18, 1, 19),
                            "\"my string literal\""
                        ),
                        "my string literal"
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

            [Test]
            public static void ShouldReadEscapedString()
            {
                var actualTokens = Lexing.Lexer.Lex(
                    SourceReader.From(@"""my \\ string \"" literal \' with \b lots \t and \n lots \f of \r escapes""")
                );
                var expectedTokens = new List<Token> {
                    new StringLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(72, 1, 73),
                            @"""my \\ string \"" literal \' with \b lots \t and \n lots \f of \r escapes"""
                        ),
                        "my \\ string \" literal \' with \b lots \t and \n lots \f of \r escapes"
                    )
                };
                LexerHelper.AssertAreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class LexMethodTestCases
        {

            [Test, TestCaseSource(typeof(LexMethodTestCases), "GetTestCases")]
            public static void LexMethodTestsFromDisk(string mofFilename)
            {
                var mofText = File.ReadAllText(mofFilename);
                var reader = SourceReader.From(mofText);
                var actualTokens = Lexing.Lexer.Lex(reader);
                var actualText = TestUtils.ConvertToJson(actualTokens);
                var expectedFilename = Path.Combine(Path.GetDirectoryName(mofFilename),
                                                    Path.GetFileNameWithoutExtension(mofFilename) + ".json");
                if (!File.Exists(expectedFilename))
                {
                    File.WriteAllText(expectedFilename, actualText);
                }
                var expectedText = File.ReadAllText(expectedFilename);
                Assert.AreEqual(expectedText, actualText);
            }

            public static IEnumerable<TestCaseData> GetTestCases
            {
                get
                {
                    return TestUtils.GetMofTestCase("Lexer\\TestCases");
                }
            }

        }

    }

}
