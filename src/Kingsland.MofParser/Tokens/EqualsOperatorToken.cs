using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record EqualsOperatorToken : SyntaxToken
{

    #region Constructors

    public EqualsOperatorToken()
        : this((SourceExtent?)null)
    {
    }

    public EqualsOperatorToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    public EqualsOperatorToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return this?.Text
            ?? "=";
    }

    #endregion

}
