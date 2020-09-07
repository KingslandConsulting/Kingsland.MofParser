﻿using Kingsland.ParseFx.Lexing;
using System.Collections.Generic;
using System.IO;

namespace Kingsland.ParseFx.Text
{

    public sealed class SourceStream
    {

        #region Constructors

        private SourceStream(TextReader baseReader)
        {
            this.BaseReader = baseReader;
            this.Buffer = new List<SourceChar>();
        }

        #endregion

        #region Properties

        private TextReader BaseReader
        {
            get;
            set;
        }

        private List<SourceChar> Buffer
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public bool Eof(int index)
        {
            return !this.PopulateBufferToPosition(index);
        }

        /// <summary>
        /// Returns the character at the specified index of the input stream.
        /// </summary>
        /// <returns></returns>
        public SourceChar Read(int index)
        {
            if (!this.PopulateBufferToPosition(index))
            {
                throw new UnexpectedEndOfStreamException();
            };
            return this.Buffer[index];
        }

        private bool PopulateBufferToPosition(int position)
        {
            while (this.Buffer.Count <= position)
            {
                if (!this.PopulateBufferChar())
                {
                    return false;
                }
            }
            return true;
        }

        private bool PopulateBufferChar()
        {
            // read the next char from the stream
            var streamRead = this.BaseReader.Read();
            if (streamRead == -1)
            {
                return false;
            }
            var streamChar = (char)streamRead;
            // append a char to the buffer
            var lastChar = (this.Buffer?.Count == 0) ? null : this.Buffer[this.Buffer.Count - 1];
            var nextChar = new SourceChar(
                SourceStream.GetNextPosition(lastChar, streamChar),
                streamChar
            );
            this.Buffer.Add(nextChar);
            return true;
        }

        private static SourcePosition GetNextPosition(SourceChar lastChar, char nextChar)
        {
            var lastPosition = lastChar?.Position;
            if (lastPosition == null)
            {
                return SourceStream.StartOfStream();
            }
            switch (lastChar.Value)
            {
                case '\r':
                    return (nextChar == '\n') ?
                        SourceStream.MoveToNext(lastPosition) :
                        SourceStream.StartNewLine(lastPosition);
                case '\n':
                    return SourceStream.StartNewLine(lastPosition);
                default:
                    return SourceStream.MoveToNext(lastPosition);
            }
        }

        private static SourcePosition StartOfStream()
        {
            return new SourcePosition(
                position: 0,
                lineNumber: 1,
                columnNumber: 1
            );
        }

        private static SourcePosition StartNewLine(SourcePosition lastPosition)
        {
            return new SourcePosition(
                position: lastPosition.Position + 1,
                lineNumber: lastPosition.LineNumber + 1,
                columnNumber: 1
            );
        }

        private static SourcePosition MoveToNext(SourcePosition lastPosition)
        {
            return new SourcePosition(
                position: lastPosition.Position + 1,
                lineNumber: lastPosition.LineNumber,
                columnNumber: lastPosition.ColumnNumber + 1
            );
        }

        #endregion

        #region Factory Methods

        public static SourceStream From(TextReader value)
        {
            return new SourceStream(value);
        }

        public static SourceStream From(string value)
        {
            return new SourceStream(
                new StringReader(value ?? string.Empty)
            );
        }

        #endregion

    }

}