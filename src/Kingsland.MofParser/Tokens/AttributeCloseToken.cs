using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record AttributeCloseToken : SyntaxToken
{

    #region Constructors

    public AttributeCloseToken()
        : this(SourceExtent.Empty)
    {
    }

    public AttributeCloseToken(SourcePosition start, SourcePosition end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    public AttributeCloseToken(SourceExtent extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return (this.Extent != SourceExtent.Empty)
            ? this.Extent.Text
            : "]";
    }

    #endregion

}
