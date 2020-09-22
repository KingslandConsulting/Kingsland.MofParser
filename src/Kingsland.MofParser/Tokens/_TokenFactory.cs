using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    internal static class TokenFactory
    {

        #region AliasIdentifierToken

        public static AliasIdentifierToken AliasIdentifierToken(string name) =>
            new AliasIdentifierToken(SourceExtent.Empty, name);

        public static AliasIdentifierToken AliasIdentifierToken(SourcePosition start, SourcePosition end, string text, string name) =>
            new AliasIdentifierToken(
                new SourceExtent(start, end, text),
                name
            );

        public static AliasIdentifierToken AliasIdentifierToken(SourceExtent extent, string name) =>
            new AliasIdentifierToken(extent, name);

        #endregion

        #region AttributeCloseToken

        public static AttributeCloseToken AttributeCloseToken() =>
            new AttributeCloseToken(SourceExtent.Empty);

        public static AttributeCloseToken AttributeCloseToken(SourcePosition start, SourcePosition end, string text) =>
            new AttributeCloseToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region AttributeOpenToken

        public static AttributeOpenToken AttributeOpenToken() =>
            new AttributeOpenToken(SourceExtent.Empty);

        public static AttributeOpenToken AttributeOpenToken(SourcePosition start, SourcePosition end, string text) =>
            new AttributeOpenToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region BlockCloseToken

        public static BlockCloseToken BlockCloseToken() =>
            new BlockCloseToken(SourceExtent.Empty);

        public static BlockCloseToken BlockCloseToken(SourcePosition start, SourcePosition end, string text) =>
            new BlockCloseToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region BlockOpenToken

        public static BlockOpenToken BlockOpenToken() =>
            new BlockOpenToken(SourceExtent.Empty);

        public static BlockOpenToken BlockOpenToken(SourcePosition start, SourcePosition end, string text) =>
            new BlockOpenToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region BooleanLiteralToken

        public static BooleanLiteralToken BooleanLiteralToken(bool value) =>
            new BooleanLiteralToken(SourceExtent.Empty, value);

        public static BooleanLiteralToken BooleanLiteralToken(SourcePosition start, SourcePosition end, string text, bool value) =>
            new BooleanLiteralToken(
                new SourceExtent(start, end, text),
                value
            );

        #endregion

        #region ColonToken

        public static ColonToken ColonToken() =>
            new ColonToken(SourceExtent.Empty);

        public static ColonToken ColonToken(SourcePosition start, SourcePosition end, string text) =>
            new ColonToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region CommaToken

        public static CommaToken CommaToken() =>
            new CommaToken(SourceExtent.Empty);

        public static CommaToken CommaToken(SourcePosition start, SourcePosition end, string text) =>
            new CommaToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region CommentToken

        public static CommentToken CommentToken(string value) =>
            new CommentToken(SourceExtent.Empty, value);

        public static CommentToken CommentToken(SourcePosition start, SourcePosition end, string text) =>
            new CommentToken(
                new SourceExtent(start, end, text),
                text
            );

        #endregion

        #region DotOperatorToken

        public static DotOperatorToken DotOperatorToken() =>
            new DotOperatorToken(SourceExtent.Empty);

        public static DotOperatorToken DotOperatorToken(SourcePosition start, SourcePosition end, string text) =>
            new DotOperatorToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region EqualsOperatorToken

        public static EqualsOperatorToken EqualsOperatorToken() =>
            new EqualsOperatorToken(SourceExtent.Empty);

        public static EqualsOperatorToken EqualsOperatorToken(SourcePosition start, SourcePosition end, string text) =>
            new EqualsOperatorToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region IdentifierToken

        public static IdentifierToken IdentifierToken(string name) =>
            new IdentifierToken(SourceExtent.Empty, name);

        public static IdentifierToken IdentifierToken(SourcePosition start, SourcePosition end, string text) =>
            new IdentifierToken(
                new SourceExtent(start, end, text),
                text
            );

        #endregion

        #region IntegerLiteralToken

        public static IntegerLiteralToken IntegerLiteralToken(IntegerKind kind, long value) =>
            new IntegerLiteralToken(SourceExtent.Empty, kind, value);

        public static IntegerLiteralToken IntegerLiteralToken(SourcePosition start, SourcePosition end, string text, IntegerKind kind, long value) =>
            new IntegerLiteralToken(
                new SourceExtent(start, end, text),
                kind, value
            );

        #endregion

        #region NullLiteralToken

        public static NullLiteralToken NullLiteralToken() =>
            new NullLiteralToken(SourceExtent.Empty);

        public static NullLiteralToken NullLiteralToken(SourcePosition start, SourcePosition end, string text) =>
            new NullLiteralToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region ParenthesisCloseToken

        public static ParenthesisCloseToken ParenthesisCloseToken() =>
            new ParenthesisCloseToken(SourceExtent.Empty);

        public static ParenthesisCloseToken ParenthesisCloseToken(SourcePosition start, SourcePosition end, string text) =>
            new ParenthesisCloseToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region ParenthesisOpenToken

        public static ParenthesisOpenToken ParenthesisOpenToken() =>
            new ParenthesisOpenToken(SourceExtent.Empty);

        public static ParenthesisOpenToken ParenthesisOpenToken(SourcePosition start, SourcePosition end, string text) =>
            new ParenthesisOpenToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region PragmaToken

        public static PragmaToken PragmaToken() =>
            new PragmaToken(SourceExtent.Empty);

        public static PragmaToken PragmaToken(SourcePosition start, SourcePosition end, string text) =>
            new PragmaToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region RealLiteralToken

        public static RealLiteralToken RealLiteralToken(double value) =>
            new RealLiteralToken(SourceExtent.Empty, value);

        public static RealLiteralToken RealLiteralToken(SourcePosition start, SourcePosition end, string text, double value) =>
            new RealLiteralToken(
                new SourceExtent(start, end, text),
                value
            );

        #endregion

        #region StatementEndToken

        public static StatementEndToken StatementEndToken() =>
            new StatementEndToken(SourceExtent.Empty);

        public static StatementEndToken StatementEndToken(SourcePosition start, SourcePosition end, string text) =>
            new StatementEndToken(
                new SourceExtent(start, end, text)
            );

        #endregion

        #region StringLiteralToken

        public static StringLiteralToken StringLiteralToken(string value) =>
            new StringLiteralToken(SourceExtent.Empty, value);

        public static StringLiteralToken StringLiteralToken(SourcePosition start, SourcePosition end, string text, string value) =>
            new StringLiteralToken(
                new SourceExtent(start, end, text),
                value
            );

        #endregion

        #region WhitespaceToken

        public static WhitespaceToken WhitespaceToken(string value) =>
            new WhitespaceToken(SourceExtent.Empty, value);

        public static WhitespaceToken WhitespaceToken(SourcePosition start, SourcePosition end, string text) =>
            new WhitespaceToken(
                new SourceExtent(start, end, text),
                text
            );

        #endregion

    }

}
