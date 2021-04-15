using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.UnitTests.Tokens
{

    internal static class TokenAssert
    {

        #region Token Comparison Methods

        public static bool AreEqual(AliasIdentifierToken? expected, AliasIdentifierToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent)) &&
                       (expected.Name == actual.Name);
            }
        }

        public static bool AreEqual(AttributeCloseToken? expected, AttributeCloseToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(AttributeOpenToken? expected, AttributeOpenToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(BlockCloseToken? expected, BlockCloseToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(BlockOpenToken? expected, BlockOpenToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(BooleanLiteralToken? expected, BooleanLiteralToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent)) &&
                       (expected.Value == actual.Value);
            }
        }

        public static bool AreEqual(ColonToken? expected, ColonToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(CommaToken? expected, CommaToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(CommentToken? expected, CommentToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(DotOperatorToken? expected, DotOperatorToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(EqualsOperatorToken? expected, EqualsOperatorToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(IdentifierToken? expected, IdentifierToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent)) &&
                       (expected.Name == actual.Name);
            }
        }

        public static bool AreEqual(IntegerLiteralToken? expected, IntegerLiteralToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent)) &&
                       (expected.Kind == actual.Kind) &&
                       (expected.Value == actual.Value);
            }
        }

        public static bool AreEqual(NullLiteralToken? expected, NullLiteralToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(ParenthesisCloseToken? expected, ParenthesisCloseToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(ParenthesisOpenToken? expected, ParenthesisOpenToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(PragmaToken? expected, PragmaToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(RealLiteralToken? expected, RealLiteralToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent)) &&
                       (expected.Value == actual.Value);
            }
        }

        public static bool AreEqual(StatementEndToken? expected, StatementEndToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent));
            }
        }

        public static bool AreEqual(StringLiteralToken? expected, StringLiteralToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent)) &&
                       (expected.Value == actual.Value);
            }
        }

        public static bool AreEqual(WhitespaceToken? expected, WhitespaceToken? actual, bool ignoreExtent)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (ignoreExtent || TokenAssert.AreEqual(expected.Extent, actual.Extent)) &&
                       (expected.Value == actual.Value);
            }
        }

        #endregion

        #region Helper Methods

        public static bool AreEqual(SourceExtent? expected, SourceExtent? actual)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return TokenAssert.AreEqual(expected.StartPosition, actual.StartPosition) &&
                       TokenAssert.AreEqual(expected.EndPosition, actual.EndPosition) &&
                       (expected.Text == actual.Text);
            }
        }

        public static bool AreEqual(SourcePosition? expected, SourcePosition? actual)
        {
            if ((expected == null) && (actual == null))
            {
                return true;
            }
            else if ((expected == null) || (actual == null))
            {
                return false;
            }
            else
            {
                return (expected.Position == actual.Position) &&
                       (expected.LineNumber == actual.LineNumber) &&
                       (expected.ColumnNumber == actual.ColumnNumber);
            }
        }

        #endregion

    }

}
