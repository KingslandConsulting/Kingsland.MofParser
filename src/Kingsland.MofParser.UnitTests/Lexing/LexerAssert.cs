using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Tokens;
using Kingsland.ParseFx.Syntax;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
                LexerAssert.Fail(
                    "expected is null, but actual is not null", index
                );
            }
            if (actualToken == null)
            {
                LexerAssert.Fail(
                    "expected is not null, but actual is null", index
                );
            }
            if (expectedToken.GetType() != actualToken.GetType())
            {
                LexerAssert.Fail(
                    $"Actual token type does not match expected token type",
                    $"{expectedToken.GetType().Name} (\"{LexerAssert.EscapeString(expectedToken.GetSourceString())}\")",
                    $"{actualToken.GetType().Name} (\"{LexerAssert.EscapeString(actualToken.GetSourceString())}\")",
                    index
                );
            }
            if (!ignoreExtent)
            {
                Assert.AreEqual(expectedToken.Extent.StartPosition.Position, actualToken.Extent.StartPosition.Position,
                    LexerAssert.GetFailMessage($"actual Start Position does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.StartPosition.LineNumber, actualToken.Extent.StartPosition.LineNumber,
                    LexerAssert.GetFailMessage($"actual Start Line does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.StartPosition.ColumnNumber, actualToken.Extent.StartPosition.ColumnNumber,
                    LexerAssert.GetFailMessage($"actual Start Column does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.EndPosition.Position, actualToken.Extent.EndPosition.Position,
                    LexerAssert.GetFailMessage($"actual End Position does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.EndPosition.LineNumber, actualToken.Extent.EndPosition.LineNumber,
                    LexerAssert.GetFailMessage($"actual End Line does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.EndPosition.ColumnNumber, actualToken.Extent.EndPosition.ColumnNumber,
                    LexerAssert.GetFailMessage($"actual End Column does not match expected value", index));
                Assert.AreEqual(expectedToken.Extent.Text, actualToken.Extent.Text,
                    LexerAssert.GetFailMessage($"actual Text does not match expected value", index));
            }
            var tokensEqual = expectedToken switch
            {
                AliasIdentifierToken _ =>
                    TokenAssert.AreEqual((AliasIdentifierToken)expectedToken, (AliasIdentifierToken)actualToken, ignoreExtent),
                AttributeCloseToken _ =>
                    TokenAssert.AreEqual((AttributeCloseToken)expectedToken, (AttributeCloseToken)actualToken, ignoreExtent),
                AttributeOpenToken _ =>
                    TokenAssert.AreEqual((AttributeOpenToken)expectedToken, (AttributeOpenToken)actualToken, ignoreExtent),
                BlockCloseToken _ =>
                    TokenAssert.AreEqual((BlockCloseToken)expectedToken, (BlockCloseToken)actualToken, ignoreExtent),
                BlockOpenToken _ =>
                    TokenAssert.AreEqual((BlockOpenToken)expectedToken, (BlockOpenToken)actualToken, ignoreExtent),
                BooleanLiteralToken _ =>
                    TokenAssert.AreEqual((BooleanLiteralToken)expectedToken, (BooleanLiteralToken)actualToken, ignoreExtent),
                ColonToken _ =>
                    TokenAssert.AreEqual((ColonToken)expectedToken, (ColonToken)actualToken, ignoreExtent),
                CommaToken _ =>
                    TokenAssert.AreEqual((CommaToken)expectedToken, (CommaToken)actualToken, ignoreExtent),
                CommentToken _ =>
                    TokenAssert.AreEqual((CommentToken)expectedToken, (CommentToken)actualToken, ignoreExtent),
                DotOperatorToken _ =>
                    TokenAssert.AreEqual((DotOperatorToken)expectedToken, (DotOperatorToken)actualToken, ignoreExtent),
                EqualsOperatorToken _ =>
                    TokenAssert.AreEqual((EqualsOperatorToken)expectedToken, (EqualsOperatorToken)actualToken, ignoreExtent),
                IdentifierToken _ =>
                    TokenAssert.AreEqual((IdentifierToken)expectedToken, (IdentifierToken)actualToken, ignoreExtent),
                IntegerLiteralToken _ =>
                    TokenAssert.AreEqual((IntegerLiteralToken)expectedToken, (IntegerLiteralToken)actualToken, ignoreExtent),
                NullLiteralToken _ =>
                    TokenAssert.AreEqual((NullLiteralToken)expectedToken, (NullLiteralToken)actualToken, ignoreExtent),
                ParenthesisCloseToken _ =>
                    TokenAssert.AreEqual((ParenthesisCloseToken)expectedToken, (ParenthesisCloseToken)actualToken, ignoreExtent),
                ParenthesisOpenToken _ =>
                    TokenAssert.AreEqual((ParenthesisOpenToken)expectedToken, (ParenthesisOpenToken)actualToken, ignoreExtent),
                PragmaToken _ =>
                    TokenAssert.AreEqual((PragmaToken)expectedToken, (PragmaToken)actualToken, ignoreExtent),
                RealLiteralToken _ =>
                    TokenAssert.AreEqual((RealLiteralToken)expectedToken, (RealLiteralToken)actualToken, ignoreExtent),
                StatementEndToken _ =>
                    TokenAssert.AreEqual((StatementEndToken)expectedToken, (StatementEndToken)actualToken, ignoreExtent),
                StringLiteralToken _ =>
                    TokenAssert.AreEqual((StringLiteralToken)expectedToken, (StringLiteralToken)actualToken, ignoreExtent),
                WhitespaceToken _ =>
                    TokenAssert.AreEqual((WhitespaceToken)expectedToken, (WhitespaceToken)actualToken, ignoreExtent),
                _ =>
                    throw new NotImplementedException($"Cannot compare type '{expectedToken.GetType().Name}'")
            };
            if (!tokensEqual)
            {
                LexerAssert.Fail(
                    $"Actual token does not match expected token",
                    $"{expectedToken.GetType().Name} (\"{LexerAssert.EscapeString(expectedToken.GetSourceString())}\")",
                    $"{actualToken.GetType().Name} (\"{LexerAssert.EscapeString(actualToken.GetSourceString())}\")",
                    index
                );
            }
        }

        private static string EscapeString(string value)
        {
            var result = new StringBuilder();
            var mappings = new Dictionary<char, string> {
                {'\"', "\\\""},
                {'\\', "\\\\"},
                {'\a', "\\a"},
                {'\b', "\\b"},
                {'\f', "\\f"},
                {'\n', "\\n"},
                {'\r', "\\r"},
                {'\t', "\\t"},
                {'\v', "\\v"},
                {'\0', "\\0"},
            };
            foreach (var c in value.ToCharArray())
            {
                result.Append(
                    mappings.ContainsKey(c) ?
                    mappings[c] :
                    new string(new char[] { c })
                );
            }
            return result.ToString();
        }

        private static void Fail(string message, int? index)
        {
            LexerAssert.Fail(message, null, null, index);
        }

        private static void Fail(string message, string expected, string actual, int? index)
        {
            Assert.Fail(LexerAssert.GetFailMessage(message, expected, actual, index));
        }

        private static string GetFailMessage(string message, int? index)
        {
            return LexerAssert.GetFailMessage(message, null, null, index);
        }

        private static string GetFailMessage(string message, string expected, string actual, int? index)
        {
            var error = new StringBuilder();
            error.Append(message);
            if (index.HasValue)
            {
                error.Append($" at index {index}");
            }
            error.AppendLine();
            if (expected != null)
            {
                error.AppendLine($"Expected: {expected}");
            }
            if (actual != null)
            {
                error.Append($"But was:  {actual}");
            }
            return error.ToString();
        }

    }

}
