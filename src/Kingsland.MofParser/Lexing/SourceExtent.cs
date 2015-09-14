using System.Collections.Generic;

namespace Kingsland.MofParser.Lexing
{

    /// <summary>
    /// Denotes the start and end points of a section of source code.
    /// </summary>
    public class SourceExtent
    {

        #region Constructors

        /// <summary>
        /// Initializes a new, empty SourceExtent.
        /// </summary>
        internal SourceExtent()
            : this(0, 0, 0, 0, 0, 0, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new SourceExtent with the start position at the current position in the stream.
        /// </summary>
        /// <param name="stream"></param>
        internal SourceExtent(ILexerStream stream)
            : this(stream.Position, stream.LineNumber, stream.Column, 0, 0, 0, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new SourceExtent with the specified start and end positions.
        /// </summary>
        /// <param name="stream"></param>
        public SourceExtent(int startPosition, int startLineNumber, int startColumnNumber, int endPosition, int endLineNumber, int endColumnNumber, string text)
        {
            this.StartPosition = startPosition;
            this.StartLineNumber = startLineNumber;
            this.StartColumnNumber = startColumnNumber;
            this.EndPosition = endPosition;
            this.EndLineNumber = endLineNumber;
            this.EndColumnNumber = endColumnNumber;
            this.Text = text;
        }

        #endregion

        #region Properties

        public int StartPosition
        {
            get;
            private set;
        }

        public int StartLineNumber
        {
            get;
            private set;
        }

        public int StartColumnNumber
        {
            get;
            private set;
        }

        public int EndPosition
        {
            get;
            private set;
        }

        public int EndLineNumber
        {
            get;
            private set;
        }

        public int EndColumnNumber
        {
            get;
            private set;
        }

        public string Text
        {
            get;
            private set;
        }

        #endregion

        #region Fluent Methods

        internal SourceExtent WithStartExtent(ILexerStream stream)
        {
            return this.WithStartExtent(stream.Position, stream.LineNumber, stream.Column);
        }

        internal SourceExtent WithStartExtent(int startPosition, int startLineNumber, int startColumnNumber)
        {
            return new SourceExtent(startPosition, startLineNumber, startColumnNumber, this.EndPosition, this.EndLineNumber, this.EndColumnNumber, this.Text);
        }

        internal SourceExtent WithEndExtent(ILexerStream stream)
        {
            return this.WithEndExtent(stream.Position, stream.LineNumber, stream.Column);
        }

        internal SourceExtent WithEndExtent(int endPosition, int endLineNumber, int endColumnNumber)
        {
            return new SourceExtent(this.StartPosition, this.StartLineNumber, this.StartColumnNumber, endPosition, endLineNumber, endColumnNumber, this.Text);
        }

        internal SourceExtent WithText(List<char> chars)
        {
            var sourceText = new string(chars.ToArray());
            return this.WithText(sourceText);
        }

        internal SourceExtent WithText(string text)
        {
            return new SourceExtent(this.StartPosition, this.StartLineNumber, this.StartColumnNumber, this.EndPosition, this.EndLineNumber, this.EndColumnNumber, text);
        }

        #endregion

        public new bool Equals(object obj)
        {
            var compare = obj as SourceExtent;
            if (compare == null) { return false; }
            if (object.ReferenceEquals(compare, this)) { return true; }
            return (compare.StartPosition ==  this.StartPosition) &&
                   (compare.StartLineNumber == this.StartLineNumber) &&
                   (compare.StartColumnNumber == this.StartColumnNumber) &&
                   (compare.EndPosition == this.EndPosition) &&
                   (compare.EndLineNumber == this.EndLineNumber) &&
                   (compare.EndColumnNumber == this.EndColumnNumber) &&
                   (compare.Text == this.Text);
        }

    }

}
