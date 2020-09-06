using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.UnitTests.Helpers
{

    internal static class TokenComparer
    {

        #region Token Comparison Methods

        public static bool AreEqual(AliasIdentifierToken obj1, AliasIdentifierToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent) &&
                       (obj1.Name == obj2.Name);
            }
        }

        public static bool AreEqual(AttributeCloseToken obj1, AttributeCloseToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }
        public static bool AreEqual(AttributeOpenToken obj1, AttributeOpenToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(BlockCloseToken obj1, BlockCloseToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(BlockOpenToken obj1, BlockOpenToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(BooleanLiteralToken obj1, BooleanLiteralToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent) &&
                       (obj1.Value == obj2.Value);
            }
        }

        public static bool AreEqual(ColonToken obj1, ColonToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(CommaToken obj1, CommaToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(CommentToken obj1, CommentToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(DotOperatorToken obj1, DotOperatorToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(EqualsOperatorToken obj1, EqualsOperatorToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(IdentifierToken obj1, IdentifierToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent) &&
                       (obj1.Name == obj2.Name);
            }
        }

        public static bool AreEqual(IntegerLiteralToken obj1, IntegerLiteralToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent) &&
                       (obj1.Kind == obj2.Kind) &&
                       (obj1.Value == obj2.Value);
            }
        }

        public static bool AreEqual(NullLiteralToken obj1, NullLiteralToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(ParenthesisCloseToken obj1, ParenthesisCloseToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(ParenthesisOpenToken obj1, ParenthesisOpenToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(PragmaToken obj1, PragmaToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(RealLiteralToken obj1, RealLiteralToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent) &&
                       (obj1.Value == obj2.Value);
            }
        }

        public static bool AreEqual(StatementEndToken obj1, StatementEndToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        public static bool AreEqual(StringLiteralToken obj1, StringLiteralToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent) &&
                       (obj1.Value == obj2.Value);
            }
        }

        public static bool AreEqual(WhitespaceToken obj1, WhitespaceToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.Extent, obj2.Extent);
            }
        }

        #endregion

        #region Helper Methods

        public static bool AreEqual(SourceExtent obj1, SourceExtent obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return TokenComparer.AreEqual(obj1.StartPosition, obj2.StartPosition) &&
                       TokenComparer.AreEqual(obj1.EndPosition, obj2.EndPosition) &&
                       (obj1.Text == obj2.Text);
            }
        }

        public static bool AreEqual(SourcePosition obj1, SourcePosition obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return (obj1.Position == obj2.Position) &&
                       (obj1.LineNumber == obj2.LineNumber) &&
                       (obj1.ColumnNumber == obj2.ColumnNumber);
            }
        }

        #endregion

    }

}
