using System;
using System.Collections.Generic;
using System.IO;

namespace Kingsland.MofParser.Source
{

    public sealed class SourceReader
    {

        #region Constructors

        private SourceReader(SourceStream stream, int position)
        {
            this.Stream = stream;
            this.Position = position;
        }

        #endregion

        #region Properties

        public SourceStream Stream
        {
            get;
            private set;
        }

        public int Position
        {
            get;
            private set;
        }

        #endregion

        #region Eof Methods

        public bool Eof()
        {
            return this.Stream.Eof(this.Position);
        }

        #endregion

        #region Peek Methods

        /// <summary>
        /// Reads the current character off of the input stream, but does not advance the current position.
        /// </summary>
        /// <returns></returns>
        public SourceChar Peek()
        {
            if (this.Eof())
            {
                throw new EndOfStreamException();
            }
            return this.Stream.Read(this.Position);
        }

        /// <summary>
        /// Returns true if the current character on the input stream matches the specified value.
        /// </summary>
        /// <returns></returns>
        public bool Peek(char value)
        {
            var peek = this.Peek();
            return (peek.Value == value);
        }

        /// <summary>
        /// Returns true if the current character on the input stream matches the specified predicate.
        /// </summary>
        /// <returns></returns>
        public bool Peek(Func<char, bool> predicate)
        {
            var peek = this.Peek();
            return predicate(peek.Value);
        }

        #endregion

        #region Read Methods

        private SourceReader next;

        public SourceReader Next()
        {
            if (this.next == null)
            {
                this.next = this.Eof() ?
                    throw new UnexpectedEndOfStreamException() :
                    new SourceReader(
                        stream: this.Stream,
                        position: this.Position + 1
                    );
            }
            return this.next;
        }

        /// <summary>
        /// Reads the current character off of the input stream and advances the current position.
        /// </summary>
        /// <returns></returns>
        public (SourceChar SourceChar, SourceReader NextReader) Read()
        {
            return (this.Peek(), this.Next());
        }

        /// <summary>
        /// Reads the current character off of the input stream and advances the current position.
        /// Throws an exception if the character does not match the specified value.
        /// </summary>
        /// <returns></returns>
        public (SourceChar SourceChar, SourceReader NextReader) Read(char value)
        {
            var peek = this.Peek();
            if (peek.Value != value)
            {
                throw new UnexpectedCharacterException(peek, value);
            }
            return (peek, this.Next());
        }

        /// <summary>
        /// Reads the current character off of the input stream and advances the current position.
        /// Throws an exception if the character does not match the specified predicate.
        /// </summary>
        /// <returns></returns>
        public (SourceChar SourceChar, SourceReader NextReader) Read(Func<char, bool> predicate)
        {
            var peek = this.Peek();
            if (!predicate(peek.Value))
            {
                throw new UnexpectedCharacterException(peek);
            }
            return (peek, this.Next());
        }

        /// <summary>
        /// Reads a string off of the input stream and advances the current position beyond the end of the string.
        /// Throws an exception if the string does not match the specified value.
        /// </summary>
        /// <returns></returns>
        public (List<SourceChar> SourceChars, SourceReader NextReader) ReadString(string value)
        {
            var thisReader = this;
            var sourceChar = default(SourceChar);
            var sourceChars = new List<SourceChar>();
            foreach (var expectedChar in value)
            {
                (sourceChar, thisReader) = thisReader.Read(expectedChar);
                sourceChars.Add(sourceChar);
            }
            return (sourceChars, thisReader);
        }

        #endregion

        #region Factory Methods

        public static SourceReader From(TextReader value)
        {
            return new SourceReader(
                stream: SourceStream.From(value),
                position: 0
            );
        }

        public static SourceReader From(string value)
        {
            return new SourceReader(
                stream: SourceStream.From(value),
                position: 0
            );
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return $"Position={this.Position}," +
                   $"Eof={this.Eof()}," +
                   $"Peek={(this.Eof() ? null : this.Peek())}";
        }

        #endregion

    }

}
