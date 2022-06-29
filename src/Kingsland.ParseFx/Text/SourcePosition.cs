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
        private set;
    }

    public int LineNumber
    {
        get;
        private set;
    }

    public int ColumnNumber
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    public bool IsEqualTo(SourcePosition obj)
    {
        return object.ReferenceEquals(obj, this) ||
               ((obj != null) &&
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
