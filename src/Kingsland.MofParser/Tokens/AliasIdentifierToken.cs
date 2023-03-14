using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record AliasIdentifierToken : SyntaxToken
{

    #region Constructors

    public AliasIdentifierToken(string name)
        : this((SourceExtent?)null, name)
    {
    }

    public AliasIdentifierToken(string? text, string name)
        : this(
              text is null ? null : new SourceExtent(null, null, text),
              name
        )
    {
    }

    public AliasIdentifierToken(SourcePosition? start, SourcePosition? end, string text, string name)
        : this(new SourceExtent(start, end, text), name)
    {
    }

    public AliasIdentifierToken(SourceExtent? extent, string name)
        : base(extent)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    #endregion

    #region Properties

    public string Name
    {
        get;
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return this.Text
            ?? $"${this.Name}";
    }

    #endregion

}
