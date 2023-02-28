using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record NullLiteralToken : SyntaxToken
{

    #region Constructors

    public NullLiteralToken()
        : this((SourceExtent?)null)
    {
    }

    public NullLiteralToken(SourcePosition? start, SourcePosition? end, string text)
        : this (new SourceExtent(start, end, text))
    {
    }

    public NullLiteralToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return this?.Text
            ?? Constants.NULL;
    }

    #endregion

}
