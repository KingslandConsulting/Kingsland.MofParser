namespace Kingsland.ParseFx.Text;

public sealed class SourcePosition
{

    #region Fields

    public static readonly SourcePosition Empty = new(-1, 0, 0);

    #endregion

    #region Constructor

    public SourcePosition(int position, int lineNumber, int columnNumber)
    {
        this.Position = position;
        this.LineNumber = lineNumber;
        this.ColumnNumber = columnNumber;
    }

    #endregion

    #region Properties

    public int Position
    {
        get;
    }

    public int LineNumber
    {
        get;
    }

    public int ColumnNumber
    {
        get;
    }

    #endregion

    #region Methods

    public bool IsEqualTo(SourcePosition obj)
    {
        return object.ReferenceEquals(obj, this) ||
               ((obj is not null) &&
                (obj.Position == this.Position) &&
                (obj.LineNumber == this.LineNumber) &&
                (obj.ColumnNumber == this.ColumnNumber));
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return $"Position={this.Position}," +
               $"LineNumber={this.LineNumber}," +
               $"ColumnNumber={this.ColumnNumber}";
    }

    #endregion

}
