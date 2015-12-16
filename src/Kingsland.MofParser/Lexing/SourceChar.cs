namespace Kingsland.MofParser.Lexing
{

    /// <summary>
    /// Represents a character from a source stream together with the position,
    /// text line and column number of the character in the source stream.
    /// </summary>
    public sealed class SourceChar
    {

        #region Constructors

        public SourceChar(char value, int position, int lineNumber, int columnNumber)
        {
            this.Value = value;
            this.Position = position;
            this.LineNumber = lineNumber;
            this.ColumnNumber = columnNumber;
        }

        #endregion

        #region Properties

        public char Value
        {
            get;
            private set;
        }

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

        #region Object Methods

        public override string ToString()
        {
            return string.Format("{{Value=\"{0}\",Position={1},LineNumber={2},ColumnNumber={3}}}", 
                                 this.Value, this.Position, this.LineNumber, this.ColumnNumber);
        }

        #endregion

    }

}
