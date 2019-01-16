using Kingsland.MofParser.Tokens;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Kingsland.MofParser.UnitTests.Lexer
{
    public sealed class LexerHelper
    {

        public static void AssertAreEqual(Token expectedToken, Token actualToken)
        {
            LexerHelper.AssertAreEqualInternal(expectedToken, actualToken);
        }

        public static void AssertAreEqual(List<Token> expectedTokens, List<Token> actualTokens)
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
                LexerHelper.AssertAreEqualInternal(expectedTokens[i], actualTokens[i], i);
            }
            Assert.AreEqual(expectedTokens.Count, actualTokens.Count, "expected and actual are different lengths");
        }

        private static void AssertAreEqualInternal(Token expectedToken, Token actualToken, int index = -1)
        {
            if ((expectedToken == null) && (actualToken == null))
            {
                return;
            }
            if (expectedToken == null)
            {
                Assert.Fail(LexerHelper.GetAssertErrorMessage("expected is null, but actual is not null", index));
            }
            if (actualToken == null)
            {
                Assert.Fail(LexerHelper.GetAssertErrorMessage("expected is not null, but actual is null", index));
            }
            Assert.AreEqual(expectedToken.GetType(), actualToken.GetType(),
                LexerHelper.GetAssertErrorMessage($"actual type does not match expected value", index));
            Assert.AreEqual(expectedToken.Extent.StartPosition.Position, actualToken.Extent.StartPosition.Position,
                LexerHelper.GetAssertErrorMessage($"actual Start Position does not match expected value", index));
            Assert.AreEqual(expectedToken.Extent.StartPosition.LineNumber, actualToken.Extent.StartPosition.LineNumber,
                LexerHelper.GetAssertErrorMessage($"actual Start Line does not match expected value", index));
            Assert.AreEqual(expectedToken.Extent.StartPosition.ColumnNumber, actualToken.Extent.StartPosition.ColumnNumber,
                LexerHelper.GetAssertErrorMessage($"actual Start Column does not match expected value", index));
            Assert.AreEqual(expectedToken.Extent.EndPosition.Position, actualToken.Extent.EndPosition.Position,
                LexerHelper.GetAssertErrorMessage($"actual End Position does not match expected value", index));
            Assert.AreEqual(expectedToken.Extent.EndPosition.LineNumber, actualToken.Extent.EndPosition.LineNumber,
                LexerHelper.GetAssertErrorMessage($"actual End Line does not match expected value", index));
            Assert.AreEqual(expectedToken.Extent.EndPosition.ColumnNumber, actualToken.Extent.EndPosition.ColumnNumber,
                LexerHelper.GetAssertErrorMessage($"actual End Column does not match expected value", index));
            Assert.AreEqual(expectedToken.Extent.Text, actualToken.Extent.Text,
                LexerHelper.GetAssertErrorMessage($"actual Text does not match expected value", index));
            switch (expectedToken)
            {
                case AliasIdentifierToken token:
                    Assert.IsTrue(
                        AliasIdentifierToken.AreEqual((AliasIdentifierToken)expectedToken, (AliasIdentifierToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case AttributeCloseToken token:
                    Assert.IsTrue(
                        AttributeCloseToken.AreEqual((AttributeCloseToken)expectedToken, (AttributeCloseToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case AttributeOpenToken token:
                    Assert.IsTrue(
                        AttributeOpenToken.AreEqual((AttributeOpenToken)expectedToken, (AttributeOpenToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case BlockCloseToken token:
                    Assert.IsTrue(
                        BlockCloseToken.AreEqual((BlockCloseToken)expectedToken, (BlockCloseToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case BlockOpenToken token:
                    Assert.IsTrue(
                        BlockOpenToken.AreEqual((BlockOpenToken)expectedToken, (BlockOpenToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case BooleanLiteralToken token:
                    Assert.IsTrue(
                        BooleanLiteralToken.AreEqual((BooleanLiteralToken)expectedToken, (BooleanLiteralToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case ColonToken token:
                    Assert.IsTrue(
                        ColonToken.AreEqual((ColonToken)expectedToken, (ColonToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case CommaToken token:
                    Assert.IsTrue(
                        CommaToken.AreEqual((CommaToken)expectedToken, (CommaToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case CommentToken token:
                    Assert.IsTrue(
                        CommentToken.AreEqual((CommentToken)expectedToken, (CommentToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case DotOperatorToken token:
                    Assert.IsTrue(
                        DotOperatorToken.AreEqual((DotOperatorToken)expectedToken, (DotOperatorToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case EqualsOperatorToken token:
                    Assert.IsTrue(
                        EqualsOperatorToken.AreEqual((EqualsOperatorToken)expectedToken, (EqualsOperatorToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case IdentifierToken token:
                    Assert.IsTrue(
                        IdentifierToken.AreEqual((IdentifierToken)expectedToken, (IdentifierToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case IntegerLiteralToken token:
                    Assert.IsTrue(
                        IntegerLiteralToken.AreEqual((IntegerLiteralToken)expectedToken, (IntegerLiteralToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case NullLiteralToken token:
                    Assert.IsTrue(
                        NullLiteralToken.AreEqual((NullLiteralToken)expectedToken, (NullLiteralToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case ParenthesisCloseToken token:
                    Assert.IsTrue(
                        ParenthesisCloseToken.AreEqual((ParenthesisCloseToken)expectedToken, (ParenthesisCloseToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case ParenthesisOpenToken token:
                    Assert.IsTrue(
                        ParenthesisOpenToken.AreEqual((ParenthesisOpenToken)expectedToken, (ParenthesisOpenToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case PragmaToken token:
                    Assert.IsTrue(
                        PragmaToken.AreEqual((PragmaToken)expectedToken, (PragmaToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case RealLiteralToken token:
                    Assert.IsTrue(
                        RealLiteralToken.AreEqual((RealLiteralToken)expectedToken, (RealLiteralToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case StatementEndToken token:
                    Assert.IsTrue(
                        StatementEndToken.AreEqual((StatementEndToken)expectedToken, (StatementEndToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case StringLiteralToken token:
                    Assert.IsTrue(
                        StringLiteralToken.AreEqual((StringLiteralToken)expectedToken, (StringLiteralToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
                    );
                    break;
                case WhitespaceToken token:
                    Assert.IsTrue(
                        WhitespaceToken.AreEqual((WhitespaceToken)expectedToken, (WhitespaceToken)actualToken),
                        LexerHelper.GetAssertErrorMessage($"actual token does not match expected token", index)
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
