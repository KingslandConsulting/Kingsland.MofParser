using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record ParenthesisCloseToken : SyntaxToken
{

    #region Constructors

    public ParenthesisCloseToken()
        : this(SourceExtent.Empty)
    {
    }

    public ParenthesisCloseToken(SourcePosition start, SourcePosition end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    public ParenthesisCloseToken(SourceExtent extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return (this.Extent != SourceExtent.Empty) ?
            this.Extent.Text :
            ")";
    }

    #endregion

}
