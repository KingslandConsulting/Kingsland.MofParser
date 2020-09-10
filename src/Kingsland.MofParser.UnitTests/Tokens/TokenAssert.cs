using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.UnitTests.Tokens
{

    internal static class TokenAssert
    {

        #region Token Comparison Methods

        public static bool AreEqual(AliasIdentifierToken expected, AliasIdentifierToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent) &&
                       (expected.Name == actual.Name);
            }
        }

        public static bool AreEqual(AttributeCloseToken expected, AttributeCloseToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }
        public static bool AreEqual(AttributeOpenToken expected, AttributeOpenToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(BlockCloseToken expected, BlockCloseToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(BlockOpenToken expected, BlockOpenToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(BooleanLiteralToken expected, BooleanLiteralToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent) &&
                       (expected.Value == actual.Value);
            }
        }

        public static bool AreEqual(ColonToken expected, ColonToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(CommaToken expected, CommaToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(CommentToken expected, CommentToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(DotOperatorToken expected, DotOperatorToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(EqualsOperatorToken expected, EqualsOperatorToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(IdentifierToken expected, IdentifierToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent) &&
                       (expected.Name == actual.Name);
            }
        }

        public static bool AreEqual(IntegerLiteralToken expected, IntegerLiteralToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent) &&
                       (expected.Kind == actual.Kind) &&
                       (expected.Value == actual.Value);
            }
        }

        public static bool AreEqual(NullLiteralToken expected, NullLiteralToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(ParenthesisCloseToken expected, ParenthesisCloseToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(ParenthesisOpenToken expected, ParenthesisOpenToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(PragmaToken expected, PragmaToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(RealLiteralToken expected, RealLiteralToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent) &&
                       (expected.Value == actual.Value);
            }
        }

        public static bool AreEqual(StatementEndToken expected, StatementEndToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        public static bool AreEqual(StringLiteralToken expected, StringLiteralToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent) &&
                       (expected.Value == actual.Value);
            }
        }

        public static bool AreEqual(WhitespaceToken expected, WhitespaceToken actual)
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
                return TokenAssert.AreEqual(expected.Extent, actual.Extent);
            }
        }

        #endregion

        #region Helper Methods

        public static bool AreEqual(SourceExtent expected, SourceExtent actual)
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

        public static bool AreEqual(SourcePosition expected, SourcePosition actual)
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
