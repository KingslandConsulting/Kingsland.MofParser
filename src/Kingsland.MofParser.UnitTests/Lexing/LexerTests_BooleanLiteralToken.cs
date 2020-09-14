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
        public static class ReadBooleanLiteralTokenMethod
        {

            [Test]
            public static void ShouldReadLowerCaseFalseToken()
            {
                var sourceText = "false";
                var expectedTokens = new List<SyntaxToken> {
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadMixedCaseFalseToken()
            {
                var sourceText = "False";
                var expectedTokens = new List<SyntaxToken> {
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadUpperCaseFalseToken()
            {
                var sourceText = "FALSE";
                var expectedTokens = new List<SyntaxToken> {
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadLowerCaseTrueToken()
            {
                var sourceText = "true";
                var expectedTokens = new List<SyntaxToken> {
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadMixedCaseTrueToken()
            {
                var sourceText = "True";
                var expectedTokens = new List<SyntaxToken> {
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

            [Test]
            public static void ShouldReadUpperCaseTrueToken()
            {
                var sourceText = "TRUE";
                var expectedTokens = new List<SyntaxToken> {
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
                LexerTests.AssertLexerTest(sourceText, expectedTokens);
            }

        }

    }

}
