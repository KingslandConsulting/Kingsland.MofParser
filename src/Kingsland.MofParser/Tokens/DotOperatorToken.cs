using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record DotOperatorToken : SyntaxToken
{

    #region Constructors

    public DotOperatorToken()
        : this(SourceExtent.Empty)
    {
    }

    public DotOperatorToken(SourcePosition start, SourcePosition end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    public DotOperatorToken(SourceExtent extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return (this.Extent != SourceExtent.Empty)
            ? this.Extent.Text
            : ".";
    }

    #endregion

}
