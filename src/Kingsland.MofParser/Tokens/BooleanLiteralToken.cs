using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record BooleanLiteralToken : SyntaxToken
{

    #region Constructors

    public BooleanLiteralToken(bool value)
        : this((SourceExtent?)null, value)
    {
    }

    public BooleanLiteralToken(string text, bool value)
        : this(null, null, text, value)
    {
    }

    public BooleanLiteralToken(SourcePosition? start, SourcePosition? end, string text, bool value)
        : this(new SourceExtent(start, end, text), value)
    {
    }

    public BooleanLiteralToken(SourceExtent? extent, bool value)
        : base(extent)
    {
        this.Value = value;
    }

    #endregion

    #region Properties

    public bool Value
    {
        get;
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return this?.Text
            ?? (this.Value ? Constants.TRUE : Constants.FALSE);
    }

    #endregion

}
