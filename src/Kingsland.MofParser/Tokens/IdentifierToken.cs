using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record IdentifierToken : SyntaxToken
{

    #region Constructors

    public IdentifierToken(string name)
        : this(SourceExtent.Empty, name)
    {
    }

    public IdentifierToken(SourcePosition start, SourcePosition end, string text)
        : this(new SourceExtent(start, end, text), text)
    {
    }

    public IdentifierToken(SourceExtent extent, string name)
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
        return this.Extent != SourceExtent.Empty ?
            this.Extent.Text :
            this.Name;
    }

    #endregion

    #region Helpers

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool IsKeyword(string value)
    {
        return this.Name.Equals(
            value,
            StringComparison.InvariantCultureIgnoreCase
        );
    }

    public bool IsKeyword(IEnumerable<string> values)
    {
        return values.Any(
            value =>
                this.Name.Equals(
                    value,
                    StringComparison.InvariantCultureIgnoreCase
                )
        );
    }

    #endregion

}
