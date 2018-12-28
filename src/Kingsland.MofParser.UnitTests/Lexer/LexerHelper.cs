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
            if ((expectedToken == null) && (actualToken == null))
            {
                return;
            }
            if (expectedToken == null)
            {
                Assert.Fail("expected is null, but actual is not null");
            }
            if (actualToken == null)
            {
                Assert.Fail("expected is not null, but actual is null");
            }
            Assert.AreEqual(expectedToken.GetType(), actualToken.GetType(),
                $"actual type does not match expected value");
            Assert.AreEqual(expectedToken.Extent.StartPosition.Position, actualToken.Extent.StartPosition.Position,
                $"actual Start Position does not match expected value");
            Assert.AreEqual(expectedToken.Extent.StartPosition.LineNumber, actualToken.Extent.StartPosition.LineNumber,
                $"actual Start Line does not match expected value");
            Assert.AreEqual(expectedToken.Extent.StartPosition.ColumnNumber, actualToken.Extent.StartPosition.ColumnNumber,
                $"actual Start Column does not match expected value");
            Assert.AreEqual(expectedToken.Extent.EndPosition.Position, actualToken.Extent.EndPosition.Position,
                $"actual End Position does not match expected value");
            Assert.AreEqual(expectedToken.Extent.EndPosition.LineNumber, actualToken.Extent.EndPosition.LineNumber,
                $"actual End Line does not match expected value");
            Assert.AreEqual(expectedToken.Extent.EndPosition.ColumnNumber, actualToken.Extent.EndPosition.ColumnNumber,
                $"actual End Column does not match expected value");
            Assert.AreEqual(expectedToken.Extent.Text, actualToken.Extent.Text,
                $"actual Text does not match expected value");
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
                var expectedToken = expectedTokens[i];
                var actualToken = actualTokens[i];
                if ((expectedToken == null) && (actualToken == null))
                {
                    return;
                }
                if (expectedToken == null)
                {
                    Assert.Fail($"expected is null, but actual is not null at index {i}");
                }
                if (actualToken == null)
                {
                    Assert.Fail($"expected is not null, but actual is null at index {i}");
                }
                Assert.AreEqual(expectedToken.GetType(), actualToken.GetType(),
                    $"actual type does not match expected value at index {i}");
                Assert.AreEqual(expectedToken.Extent.StartPosition.Position, actualToken.Extent.StartPosition.Position,
                    $"actual Start Position does not match expected value at index {i}");
                Assert.AreEqual(expectedToken.Extent.StartPosition.LineNumber, actualToken.Extent.StartPosition.LineNumber,
                    $"actual Start Line does not match expected value at index {i}");
                Assert.AreEqual(expectedToken.Extent.StartPosition.ColumnNumber, actualToken.Extent.StartPosition.ColumnNumber,
                    $"actual Start Column does not match expected value at index {i}");
                Assert.AreEqual(expectedToken.Extent.EndPosition.Position, actualToken.Extent.EndPosition.Position,
                    $"actual End Position does not match expected value at index {i}");
                Assert.AreEqual(expectedToken.Extent.EndPosition.LineNumber, actualToken.Extent.EndPosition.LineNumber,
                    $"actual End Line does not match expected value at index {i}");
                Assert.AreEqual(expectedToken.Extent.EndPosition.ColumnNumber, actualToken.Extent.EndPosition.ColumnNumber,
                    $"actual End Column does not match expected value at index {i}");
                Assert.AreEqual(expectedToken.Extent.Text, actualToken.Extent.Text,
                    $"actual Text does not match expected value at index {i}");
            }
            Assert.AreEqual(expectedTokens.Count, actualTokens.Count, "expected and actual are different lengths");
        }

    }

}
