namespace Kingsland.MofParser.CodeGen;

public sealed class AstWriterOptions
{

    #region Fields

    public readonly static AstWriterOptions Default = new(
        newLine: Environment.NewLine,
        indentStep: "\t",
        quirks: MofQuirks.None
    );

    #endregion

    #region Constructors

    public AstWriterOptions(
        string newLine, string indentStep, MofQuirks quirks = MofQuirks.None
    )
    {
        this.NewLine = newLine ?? throw new ArgumentNullException(nameof(newLine));
        this.IndentStep = indentStep ?? throw new ArgumentNullException(nameof(indentStep));
        this.Quirks = quirks;
    }

    #endregion

    #region Properties

    public string NewLine
    {
        get;
    }

    public string IndentStep
    {
        get;
    }

    public MofQuirks Quirks
    {
        get;
    }

    #endregion

    #region Methods

    public static AstWriterOptions Create(
        string? newLine = null, string? indentStep = null, MofQuirks? quirks = null
    )
    {
        return new AstWriterOptions(
            newLine ?? AstWriterOptions.Default.NewLine,
            indentStep ?? AstWriterOptions.Default.IndentStep,
            quirks ?? AstWriterOptions.Default.Quirks
        );
    }

    #endregion

}
