using Kingsland.ParseFx.Text;

namespace Kingsland.ParseFx.Syntax;

public abstract record SyntaxToken
{

    #region Constructors

    protected SyntaxToken(SourceExtent extent)
    {
        this.Extent = extent ?? throw new ArgumentNullException(nameof(extent));
    }

    #endregion

    #region Properties

    public SourceExtent Extent
    {
        get;
    }

    public string Text =>
        this.Extent.Text;

    #endregion

    #region Methods

    public virtual string GetDebugString()
    {
        return $"{this.GetType().Name} (\"{this?.Extent.Text}\")";
    }

    public virtual string GetSourceString()
    {
        return this.Extent.Text;
    }

    #endregion

    #region Object Interface

    public override string ToString()
    {
        return this.GetDebugString();
    }

    #endregion

}
