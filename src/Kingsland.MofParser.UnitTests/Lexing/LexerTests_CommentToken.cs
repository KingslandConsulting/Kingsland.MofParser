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
        public static class ReadCommentTokenMethod
        {

            [Test]
            public static void ShouldReadSingleLineEofCommentToken()
            {
                var sourceText = "// single line comment";
                var expectedTokens = new List<SyntaxToken> {
                    new CommentToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(21, 1, 22),
                            "// single line comment"
                        )
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadSingleLineEolCommentToken()
            {
                var sourceText = "// single line comment\r\n";
                var expectedTokens = new List<SyntaxToken> {
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
                        ),
                        "\r\n"
                    )
                };
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadMultilineEofCommentToken()
            {
                var sourceText =
                    "/*\r\n" +
                    "@TargetNode='MyServer'\r\n" +
                    "@GeneratedBy=mike.clayton\r\n" +
                    "@GenerationDate=07/19/2014 10:37:04\r\n" +
                    "@GenerationHost=MyDesktop\r\n" +
                    "*/";
                var expectedTokens = new List<SyntaxToken> {
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadMultilineUnclosedCommentToken()
            {
                var sourceText =
                    "/*\r\n" +
                    "@TargetNode='MyServer'\r\n" +
                    "@GeneratedBy=mike.clayton\r\n" +
                    "@GenerationDate=07/19/2014 10:37:04\r\n" +
                    "@GenerationHost=MyDesktop\r\n";
                var expectedTokens = new List<SyntaxToken> {
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadMultilineInlineAsterisks()
            {
                var sourceText =
                    "/*************\r\n" +
                    "@TargetNode='MyServer'\r\n" +
                    "@GeneratedBy=mike.clayton\r\n" +
                    "@GenerationDate=07/19/2014 10:37:04\r\n" +
                    "@GenerationHost=MyDesktop\r\n" +
                    "*************/";
                var expectedTokens = new List<SyntaxToken> {
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadMultilineMultiple()
            {
                var sourceText =
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
                    "*/";
                var expectedTokens = new List<SyntaxToken> {
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadExample1CommentToken()
            {
                // see DSP0221_3.0.1.pdf "5.4 Comments"
                var sourceText =
                    "Integer MyProperty; // This is an example of a single-line comment";
                var expectedTokens = new List<SyntaxToken> {
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
                        ),
                        " "
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
                        ),
                        " "
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadExample2CommentToken()
            {
                // see DSP0221_3.0.1.pdf "5.4 Comments"
                var sourceText =
                    "/* example of a comment between property definition tokens and a multi-line comment */\r\n" +
                    "Integer /* 16-bit integer property */ MyProperty; /* and a multi-line\r\n" +
                    "                        comment */";
                var expectedTokens = new List<SyntaxToken> {
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
                        ),
                        "\r\n"
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
                        ),
                        " "
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
                        ),
                        " "
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
                        ),
                        " "
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

        }

    }

}
