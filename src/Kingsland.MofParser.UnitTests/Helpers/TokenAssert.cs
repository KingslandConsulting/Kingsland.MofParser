using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Helpers;

internal static class TokenAssert
{

    public static void AreEqual(AliasIdentifierToken? expected, AliasIdentifierToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(AttributeCloseToken? expected, AttributeCloseToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(AttributeOpenToken? expected, AttributeOpenToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(BlockCloseToken? expected, BlockCloseToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(BlockOpenToken? expected, BlockOpenToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(BooleanLiteralToken? expected, BooleanLiteralToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(ColonToken? expected, ColonToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(CommaToken? expected, CommaToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(CommentToken? expected, CommentToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(DotOperatorToken? expected, DotOperatorToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(EqualsOperatorToken? expected, EqualsOperatorToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(IdentifierToken? expected, IdentifierToken? actual, bool ignoreExtent)
    {
        Assert.Multiple(() => {
            if ((expected == null) || (actual == null))
            {
                Assert.That(actual, Is.EqualTo(expected));
                return;
            }
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            if (!ignoreExtent)
            {
                TokenAssert.AreEqual(expected.Extent, actual!.Extent);
            }
        });
    }

    public static void AreEqual(IntegerLiteralToken? expected, IntegerLiteralToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(NullLiteralToken? expected, NullLiteralToken? actual, bool ignoreExtent)
    {
        Assert.Multiple(() => {
            if ((expected == null) || (actual == null))
            {
                Assert.That(actual, Is.EqualTo(expected));
                return;
            }
            if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
            {
                throw new InvalidOperationException();
            }
        });
    }

    public static void AreEqual(ParenthesisCloseToken? expected, ParenthesisCloseToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(ParenthesisOpenToken? expected, ParenthesisOpenToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(PragmaToken? expected, PragmaToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(RealLiteralToken? expected, RealLiteralToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(StatementEndToken? expected, StatementEndToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(StringLiteralToken? expected, StringLiteralToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    public static void AreEqual(WhitespaceToken? expected, WhitespaceToken? actual, bool ignoreExtent)
    {
        if (!TokenCompare.AreEqual(expected, actual, ignoreExtent))
        {
            throw new InvalidOperationException();
        }
    }

    #region Helper Methods

    private static void AreEqual(SourceExtent? expected, SourceExtent? actual)
    {
        Assert.Multiple(() => {
            if ((expected == null) || (actual == null))
            {
                Assert.That(actual, Is.EqualTo(expected));
                return;
            }
            TokenAssert.AreEqual(expected.StartPosition, actual.StartPosition);
            TokenAssert.AreEqual(expected.EndPosition, actual.EndPosition);
            Assert.That(actual!.Text, Is.EqualTo(expected.Text));
        });
    }

    private static void AreEqual(SourcePosition? expected, SourcePosition? actual)
    {
        Assert.Multiple(() => {
            if ((expected == null) || (actual == null))
            {
                Assert.That(actual, Is.EqualTo(expected));
                return;
            }
            Assert.That(actual.Position, Is.EqualTo(expected.Position));
            Assert.That(actual.LineNumber, Is.EqualTo(expected.LineNumber));
            Assert.That(actual.ColumnNumber, Is.EqualTo(expected.ColumnNumber));
        });
    }

    #endregion

}
