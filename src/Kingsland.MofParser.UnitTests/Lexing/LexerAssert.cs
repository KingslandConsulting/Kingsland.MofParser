using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Tokens;
using Kingsland.ParseFx.Syntax;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Kingsland.MofParser.UnitTests.Lexing
{

    public sealed class LexerAssert
    {

        public static void AreEqual(SyntaxToken expectedToken, SyntaxToken actualToken)
        {
            LexerAssert.AreEqualInternal(expectedToken, actualToken);
        }

        public static void AreEqual(List<SyntaxToken> expectedTokens, List<SyntaxToken> actualTokens, bool ignoreExtent = false)
        {
            if ((expectedTokens == null) && (actualTokens == null))
            {
                return;
            }
            if (expectedTokens == null)
            {
                Assert.Fail("expected is null, but actual is not null");
            }
            if (actualTokens == null)
            {
                Assert.Fail("expected is not null, but actual is null");
            }
            for (var i = 0; i < Math.Min(expectedTokens.Count, actualTokens.Count); i++)
            {
                LexerAssert.AreEqualInternal(expectedTokens[i], actualTokens[i], ignoreExtent, i);
            }
            Assert.AreEqual(expectedTokens.Count, actualTokens.Count, "expected and actual are different lengths");
        }

        private static void AreEqualInternal(SyntaxToken expectedToken, SyntaxToken actualToken, bool ignoreExtent = false, int index = -1)
        {
            if ((expectedToken == null) && (actualToken == null))
            {
                return;
            }
            if (expectedToken == null)
            {
                Assert.Fail(LexerAssert.GetAssertErrorMessage("expected is null, but actual is not null", index));
            }
            if (actualToken == null)
            {
                Assert.Fail(LexerAssert.GetAssertErrorMessage("expected is not null, but actual is null", index));
            }
            Assert.AreEqual(expectedToken.GetType(), actualToken.GetType(),
                LexerAssert.GetAssertErrorMessage($"actual type does not match expected value", index));
            if (!ignoreExtent)
            {
                Assert.AreEqual(expectedToken.Extent.StartPosition.Position, actualToken.Extent.StartPosition.Position,
                    LexerAssert.GetAssertErrorMessage($"actual Start Position does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.StartPosition.LineNumber, actualToken.Extent.StartPosition.LineNumber,
                    LexerAssert.GetAssertErrorMessage($"actual Start Line does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.StartPosition.ColumnNumber, actualToken.Extent.StartPosition.ColumnNumber,
                    LexerAssert.GetAssertErrorMessage($"actual Start Column does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.EndPosition.Position, actualToken.Extent.EndPosition.Position,
                    LexerAssert.GetAssertErrorMessage($"actual End Position does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.EndPosition.LineNumber, actualToken.Extent.EndPosition.LineNumber,
                    LexerAssert.GetAssertErrorMessage($"actual End Line does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.EndPosition.ColumnNumber, actualToken.Extent.EndPosition.ColumnNumber,
                    LexerAssert.GetAssertErrorMessage($"actual End Column does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.Text, actualToken.Extent.Text,
                    LexerAssert.GetAssertErrorMessage($"actual Text does not match expected value", index));
            }
            switch (expectedToken)
            {
                case AliasIdentifierToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((AliasIdentifierToken)expectedToken, (AliasIdentifierToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case AttributeCloseToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((AttributeCloseToken)expectedToken, (AttributeCloseToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case AttributeOpenToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((AttributeOpenToken)expectedToken, (AttributeOpenToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case BlockCloseToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((BlockCloseToken)expectedToken, (BlockCloseToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case BlockOpenToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((BlockOpenToken)expectedToken, (BlockOpenToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case BooleanLiteralToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((BooleanLiteralToken)expectedToken, (BooleanLiteralToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case ColonToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((ColonToken)expectedToken, (ColonToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case CommaToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((CommaToken)expectedToken, (CommaToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case CommentToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((CommentToken)expectedToken, (CommentToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case DotOperatorToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((DotOperatorToken)expectedToken, (DotOperatorToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case EqualsOperatorToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((EqualsOperatorToken)expectedToken, (EqualsOperatorToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case IdentifierToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((IdentifierToken)expectedToken, (IdentifierToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case IntegerLiteralToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((IntegerLiteralToken)expectedToken, (IntegerLiteralToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case NullLiteralToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((NullLiteralToken)expectedToken, (NullLiteralToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case ParenthesisCloseToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((ParenthesisCloseToken)expectedToken, (ParenthesisCloseToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case ParenthesisOpenToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((ParenthesisOpenToken)expectedToken, (ParenthesisOpenToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case PragmaToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((PragmaToken)expectedToken, (PragmaToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case RealLiteralToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((RealLiteralToken)expectedToken, (RealLiteralToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case StatementEndToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((StatementEndToken)expectedToken, (StatementEndToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case StringLiteralToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((StringLiteralToken)expectedToken, (StringLiteralToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case WhitespaceToken _:
                    Assert.IsTrue(
                        TokenAssert.AreEqual((WhitespaceToken)expectedToken, (WhitespaceToken)actualToken, ignoreExtent),
                        LexerAssert.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                default:
                    throw new NotImplementedException($"Cannot compare type '{expectedToken.GetType().Name}'");
            }
        }

        private static string GetAssertErrorMessage(string message, int index = -1)
        {
            if (index == -1)
            {
                return message;
            }
            else
            {
                return $"{message} at index {index}";
            }
        }

    }

}
