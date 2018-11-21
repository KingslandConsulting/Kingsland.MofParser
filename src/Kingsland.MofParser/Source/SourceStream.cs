using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kingsland.MofParser.Source
{

    public sealed class SourceStream
    {

        #region Builder

        public sealed class Builder
        {
        }

        #endregion

        #region Constructors

        private SourceStream()
        {
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
            var lastChar = this.Buffer.LastOrDefault();
            var nextChar = new SourceChar.Builder
            {
                Value = streamChar,
                Position = SourceStream.GetNextPosition(lastChar, streamChar),
            }.Build();
            this.Buffer.Add(nextChar);
            return true;
        }

        private static SourcePosition GetNextPosition(SourceChar lastChar, char nextChar)
        {
            var lastPosition = lastChar?.Position ?? new SourcePosition.Builder
            {
                Position = -1,
                LineNumber = 1,
                ColumnNumber = 0
            }.Build();
            var nextPosition = new SourcePosition.Builder
            {
                Position = lastPosition.Position + 1
            };
            switch (nextChar)
            {
                case '\r':
                    // start a new line
                    nextPosition.LineNumber = lastPosition.LineNumber + 1;
                    nextPosition.ColumnNumber = 0;
                    break;
                case '\n':
                    if ((lastChar != null) && (lastChar.Value == '\r'))
                    {
                        // this is a "\r\n" pair, so don't move
                        nextPosition.LineNumber = lastPosition.LineNumber;
                        nextPosition.ColumnNumber = lastPosition.ColumnNumber;
                    }
                    else
                    {
                        // start a new line
                        nextPosition.LineNumber = lastPosition.LineNumber + 1;
                        nextPosition.ColumnNumber = 0;
                    }
                    break;
                default:
                    nextPosition.LineNumber = lastPosition.LineNumber;
                    nextPosition.ColumnNumber = lastPosition.ColumnNumber + 1;
                    break;
            }
            return nextPosition.Build();
        }

        #endregion

        #region Factory Methods

        public static SourceStream FromTextReader(TextReader value)
        {
            return new SourceStream
            {
                BaseReader = value,
                Buffer = new List<SourceChar>()
            };
        }

        public static SourceStream FromString(string value)
        {
            return new SourceStream
            {
                BaseReader = new StringReader(value ?? string.Empty),
                Buffer = new List<SourceChar>()
            };
        }

        #endregion

    }

}