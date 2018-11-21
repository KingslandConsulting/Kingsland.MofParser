namespace Kingsland.MofParser.Source
{

    /// <summary>
    /// Represents a character from a source stream together with the position,
    /// text line and column number of the character in the source stream.
    /// </summary>
    public sealed class SourceChar
    {

        #region Builder

        public sealed class Builder
        {

            public SourcePosition Position
            {
                get;
                set;
            }

            public char Value
            {
                get;
                set;
            }

            public SourceChar Build()
            {
                return new SourceChar
                {
                    Position = this.Position,
                    Value = this.Value,
                };
            }

        }

        #endregion

        #region Constructors

        private SourceChar()
        {
        }

        #endregion

        #region Properties

        public SourcePosition Position
        {
            get;
            private set;
        }

        public char Value
        {
            get;
            private set;
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

}
