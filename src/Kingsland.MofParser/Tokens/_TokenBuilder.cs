using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed class TokenBuilder
{

    #region Constructors

    public TokenBuilder()
    {
        this.Tokens = [];
    }

    #endregion

    #region Properties

    private List<SyntaxToken> Tokens
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public List<SyntaxToken> ToList()
    {
        // return a duplicate of the Tokens value so our
        // internal list isn't exposed to external code
        return [.. this.Tokens];
    }

    #endregion

    #region AliasIdentifierToken

    public TokenBuilder AliasIdentifierToken(string name)
    {
        this.Tokens.Add(new AliasIdentifierToken(name));
        return this;
    }

    public TokenBuilder AliasIdentifierToken(SourcePosition start, SourcePosition end, string text, string name)
    {
        this.Tokens.Add(new AliasIdentifierToken(start, end, text, name));
        return this;
    }

    public TokenBuilder AliasIdentifierToken(SourceExtent extent, string name)
    {
        this.Tokens.Add(new AliasIdentifierToken(extent, name));
        return this;
    }

    #endregion

    #region AttributeCloseToken

    public TokenBuilder AttributeCloseToken()
    {
        this.Tokens.Add(new AttributeCloseToken());
        return this;
    }

    public TokenBuilder AttributeCloseToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new AttributeCloseToken(start, end, text));
        return this;
    }

    #endregion

    #region AttributeOpenToken

    public TokenBuilder AttributeOpenToken()
    {
        this.Tokens.Add(new AttributeOpenToken());
        return this;
    }

    public TokenBuilder AttributeOpenToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new AttributeOpenToken(start, end, text));
        return this;
    }

    #endregion

    #region BlockCloseToken

    public TokenBuilder BlockCloseToken()
    {
        this.Tokens.Add(new BlockCloseToken());
        return this;
    }

    public TokenBuilder BlockCloseToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new BlockCloseToken(start, end, text));
        return this;
    }

    #endregion

    #region BlockOpenToken

    public TokenBuilder BlockOpenToken()
    {
        this.Tokens.Add(new BlockOpenToken());
        return this;
    }

    public TokenBuilder BlockOpenToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new BlockOpenToken(start, end, text));
        return this;
    }

    #endregion

    #region BooleanLiteralToken

    public TokenBuilder BooleanLiteralToken(bool value)
    {
        this.Tokens.Add(
            new BooleanLiteralToken(value)
        );
        return this;
    }

    public TokenBuilder BooleanLiteralToken(string text, bool value)
    {
        this.Tokens.Add(
            new BooleanLiteralToken(text, value)
        );
        return this;
    }

    public TokenBuilder BooleanLiteralToken(SourcePosition? start, SourcePosition? end, string text, bool value)
    {
        this.Tokens.Add(
            new BooleanLiteralToken(start, end, text, value)
        );
        return this;
    }

    #endregion

    #region ColonToken

    public TokenBuilder ColonToken()
    {
        this.Tokens.Add(new ColonToken());
        return this;
    }

    public TokenBuilder ColonToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new ColonToken(start, end, text));
        return this;
    }

    #endregion

    #region CommaToken

    public TokenBuilder CommaToken()
    {
        this.Tokens.Add(new CommaToken());
        return this;
    }

    public TokenBuilder CommaToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new CommaToken(start, end, text));
        return this;
    }

    #endregion

    #region CommentToken

    public TokenBuilder CommentToken(string value)
    {
        this.Tokens.Add(new CommentToken(value));
        return this;
    }

    public TokenBuilder CommentToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new CommentToken(start, end, text));
        return this;
    }

    #endregion

    #region DotOperatorToken

    public TokenBuilder DotOperatorToken()
    {
        this.Tokens.Add(new DotOperatorToken());
        return this;
    }

    public TokenBuilder DotOperatorToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new DotOperatorToken(start, end, text));
        return this;
    }

    #endregion

    #region EqualsOperatorToken

    public TokenBuilder EqualsOperatorToken()
    {
        this.Tokens.Add(new EqualsOperatorToken());
        return this;
    }

    public TokenBuilder EqualsOperatorToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new EqualsOperatorToken(start, end, text));
        return this;
    }

    #endregion

    #region IdentifierToken

    public TokenBuilder IdentifierToken(string name)
    {
        this.Tokens.Add(new IdentifierToken(name));
        return this;
    }

    public TokenBuilder IdentifierToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new IdentifierToken(start, end, text));
        return this;
    }

    #endregion

    #region IntegerLiteralToken

    public TokenBuilder IntegerLiteralToken(IntegerKind kind, long value)
    {
        this.Tokens.Add(new IntegerLiteralToken(kind, value));
        return this;
    }

    public TokenBuilder IntegerLiteralToken(string text, IntegerKind kind, long value)
    {
        this.Tokens.Add(new IntegerLiteralToken(text, kind, value));
        return this;
    }

    public TokenBuilder IntegerLiteralToken(SourcePosition start, SourcePosition end, string text, IntegerKind kind, long value)
    {
        this.Tokens.Add(new IntegerLiteralToken(start, end, text, kind, value));
        return this;
    }

    #endregion

    #region NullLiteralToken

    public TokenBuilder NullLiteralToken()
    {
        this.Tokens.Add(new NullLiteralToken());
        return this;
    }

    public TokenBuilder NullLiteralToken(string text)
    {
        this.Tokens.Add(
            new NullLiteralToken(null, null, text)
        );
        return this;
    }

    public TokenBuilder NullLiteralToken(SourcePosition? start, SourcePosition? end, string text)
    {
        this.Tokens.Add(
            new NullLiteralToken(start, end, text)
        );
        return this;
    }

    #endregion

    #region ParenthesisCloseToken

    public TokenBuilder ParenthesisCloseToken()
    {
        this.Tokens.Add(new ParenthesisCloseToken());
        return this;
    }

    public TokenBuilder ParenthesisCloseToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new ParenthesisCloseToken(start, end, text));
        return this;
    }

    #endregion

    #region ParenthesisOpenToken

    public TokenBuilder ParenthesisOpenToken()
    {
        this.Tokens.Add(new ParenthesisOpenToken());
        return this;
    }

    public TokenBuilder ParenthesisOpenToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new ParenthesisOpenToken(start, end, text));
        return this;
    }

    #endregion

    #region PragmaToken

    public TokenBuilder PragmaToken()
    {
        this.Tokens.Add(new PragmaToken());
        return this;
    }

    public TokenBuilder PragmaToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new PragmaToken(start, end, text));
        return this;
    }

    #endregion

    #region RealLiteralToken

    public TokenBuilder RealLiteralToken(double value)
    {
        this.Tokens.Add(
            new RealLiteralToken(value)
        );
        return this;
    }

    public TokenBuilder RealLiteralToken(string text, double value)
    {
        this.Tokens.Add(
            new RealLiteralToken(null, null, text, value)
        );
        return this;
    }

    public TokenBuilder RealLiteralToken(SourcePosition? start, SourcePosition? end, string text, double value)
    {
        this.Tokens.Add(
            new RealLiteralToken(start, end, text, value)
        );
        return this;
    }

    #endregion

    #region StatementEndToken

    public TokenBuilder StatementEndToken()
    {
        this.Tokens.Add(new StatementEndToken());
        return this;
    }

    public TokenBuilder StatementEndToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new StatementEndToken(start, end, text));
        return this;
    }

    #endregion

    #region StringLiteralToken

    public TokenBuilder StringLiteralToken(string value)
    {
        this.Tokens.Add(new StringLiteralToken(value));
        return this;
    }

    public TokenBuilder StringLiteralToken(SourcePosition start, SourcePosition end, string text, string value)
    {
        this.Tokens.Add(new StringLiteralToken(start, end, text, value));
        return this;
    }

    #endregion

    #region WhitespaceToken

    public TokenBuilder WhitespaceToken(string value)
    {
        this.Tokens.Add(new WhitespaceToken(value));
        return this;
    }

    public TokenBuilder WhitespaceToken(SourcePosition start, SourcePosition end, string text)
    {
        this.Tokens.Add(new WhitespaceToken(start, end, text));
        return this;
    }

    #endregion

}
