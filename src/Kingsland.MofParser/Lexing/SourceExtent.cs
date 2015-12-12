using System;
using System.Collections.Generic;
using System.Linq;

namespace Kingsland.MofParser.Lexing
{

    /// <summary>
    /// Denotes the start and end points of a section of source code.
    /// </summary>
    public sealed class SourceExtent
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
            : this(stream.Position, stream.LineNumber, stream.ColumnNumber, 0, 0, 0, string.Empty)
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

        /// <summary>
        /// Initializes a new SourceExtent with the specified start and end positions.
        /// </summary>
        /// <param name="stream"></param>
        public SourceExtent(List<SourceChar> sourceChars)
        {
            if((sourceChars == null) || (sourceChars.Count < 1))
            {
                throw new ArgumentException("Value must contain one or more items.", "chars");
            }
            var startChar = sourceChars.First();
            var endChar = sourceChars.Last();
            this.StartPosition = startChar.Position;
            this.StartLineNumber = startChar.LineNumber;
            this.StartColumnNumber = startChar.ColumnNumber;
            this.EndPosition = endChar.Position;
            this.EndLineNumber = endChar.LineNumber;
            this.EndColumnNumber = endChar.ColumnNumber;
            this.Text = new string(sourceChars.Select(c => c.Value).ToArray());
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
            return this.WithStartExtent(stream.Position, stream.LineNumber, stream.ColumnNumber);
        }

        internal SourceExtent WithStartExtent(SourceChar sourceChar)
        {
            return new SourceExtent(sourceChar.Position, sourceChar.LineNumber, sourceChar.ColumnNumber, this.EndPosition, this.EndLineNumber, this.EndColumnNumber, this.Text);
        }

        internal SourceExtent WithStartExtent(int startPosition, int startLineNumber, int startColumnNumber)
        {
            return new SourceExtent(startPosition, startLineNumber, startColumnNumber, this.EndPosition, this.EndLineNumber, this.EndColumnNumber, this.Text);
        }

        internal SourceExtent WithEndExtent(ILexerStream stream)
        {
            return this.WithEndExtent(stream.Position, stream.LineNumber, stream.ColumnNumber);
        }

        internal SourceExtent WithEndExtent(SourceChar sourceChar)
        {
            return new SourceExtent(this.StartPosition, this.StartLineNumber, this.StartColumnNumber, sourceChar.Position, sourceChar.LineNumber, sourceChar.ColumnNumber, this.Text);
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

    }

}
