using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record StatementEndToken : SyntaxToken
{

    #region Constructors

    public StatementEndToken()
        : this((SourceExtent?)null)
    {
    }

    public StatementEndToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    public StatementEndToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return this.Text
            ?? ";";
    }

    #endregion

}
