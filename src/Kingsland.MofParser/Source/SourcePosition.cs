namespace Kingsland.MofParser.Source
{

    public sealed class SourcePosition
    {

        #region Builder

        public sealed class Builder
        {

            public int Position
            {
                get;
                set;
            }

            public int LineNumber
            {
                get;
                set;
            }

            public int ColumnNumber
            {
                get;
                set;
            }

            public SourcePosition Build()
            {
                return new SourcePosition
                {
                    Position = this.Position,
                    LineNumber = this.LineNumber,
                    ColumnNumber = this.ColumnNumber,
                };
            }

        }

        #endregion

        #region Fields

        public static readonly SourcePosition Empty = new SourcePosition.Builder
        {
            Position = -1,
            LineNumber = -1,
            ColumnNumber = -1
        }.Build();

        #endregion

        #region Constructor

        private SourcePosition()
        {
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

        public bool EqualTo(SourcePosition obj)
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

}
