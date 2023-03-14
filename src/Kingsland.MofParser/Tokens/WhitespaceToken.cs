using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record WhitespaceToken : SyntaxToken
{

    #region Constructors

    public WhitespaceToken(string value)
        : this(null, value)
    {
    }

    public WhitespaceToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text), text)
    {
    }

    public WhitespaceToken(SourceExtent? extent, string value)
        : base(extent)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    #endregion

    #region Properties

    public string Value
    {
        get;
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return this.Text
            ?? this.Value;
    }

    #endregion

}
