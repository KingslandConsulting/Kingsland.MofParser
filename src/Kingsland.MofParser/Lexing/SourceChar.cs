namespace Kingsland.MofParser.Lexing
{

    /// <summary>
    /// Represents a character from a source stream together with the position,
    /// text line and column number of the character in the stream.
    /// </summary>
    public class SourceChar
    {

        public SourceChar(char value, int position, int lineNumber, int columnNumber)
        {
            this.Value = value;
            this.Position = position;
            this.LineNumber = lineNumber;
            this.ColumnNumber = columnNumber;
        }

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

    }

}
