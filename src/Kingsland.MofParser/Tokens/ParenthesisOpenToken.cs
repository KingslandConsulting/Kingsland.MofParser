using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record ParenthesisOpenToken : SyntaxToken
{

    #region Constructors

    public ParenthesisOpenToken()
        : this((SourceExtent?)null)
    {
    }

    public ParenthesisOpenToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    public ParenthesisOpenToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return this?.Text
            ?? "(";
    }

    #endregion

}
