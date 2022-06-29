using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record AttributeOpenToken : SyntaxToken
{

    #region Constructors

    public AttributeOpenToken()
        : this(SourceExtent.Empty)
    {
    }

    public AttributeOpenToken(SourcePosition start, SourcePosition end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    public AttributeOpenToken(SourceExtent extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return (this.Extent != SourceExtent.Empty)
            ? this.Extent.Text
            : "[";
    }

    #endregion

}
