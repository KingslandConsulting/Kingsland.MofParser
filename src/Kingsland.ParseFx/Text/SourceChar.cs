namespace Kingsland.ParseFx.Text;

/// <summary>
/// Represents a character from a source stream together with the position,
/// text line and column number of the character in the source stream.
/// </summary>
public sealed class SourceChar
{

    #region Constructors

    internal SourceChar(SourcePosition position, char value)
    {
        this.Position = position;
        this.Value = value;
    }

    #endregion

    #region Properties

    public SourcePosition Position
    {
        get;
    }

    public char Value
    {
        get;
    }

    #endregion

    #region Object Methods

    public override string ToString()
    {
        return $"{{" +
               $"Position={this.Position.Position}," +
               $"LineNumber={this.Position.LineNumber}," +
               $"ColumnNumber={this.Position.ColumnNumber}," +
               $"Value=\"{this.Value}\"" +
               $"}}";
    }

    #endregion

}
