using System.IO;

namespace Kingsland.MofParser.Source
{

    public sealed class SourceReader
    {

        #region Builder

        public sealed class Builder
        {

            public SourceStream Stream
            {
                get;
                set;
            }

            public int Position
            {
                get;
                set;
            }

            public SourceReader Build()
            {
                return new SourceReader
                {
                    Stream = this.Stream,
                    Position = this.Position
                };
            }

        }

        #endregion

        #region Constructors

        private SourceReader()
        {
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

        #endregion

        #region Read Methods

        /// <summary>
        /// Reads the current character off of the input stream and advances the current position.
        /// </summary>
        /// <returns></returns>
        public SourceChar Read()
        {
            return this.Peek();
        }

        /// <summary>
        /// Reads the current character off of the input stream and advances the current position.
        /// Throws an exception if the character does not match the specified value.
        /// </summary>
        /// <returns></returns>
        public SourceChar Read(char value)
        {
            var peek = this.Peek();
            if (peek.Value != value)
            {
                throw new UnexpectedCharacterException(peek, value);
            }
            return peek;
        }

        ///// <summary>
        ///// Reads a string off of the input stream and advances the current position beyond the end of the string.
        ///// Throws an exception if the string does not match the specified value.
        ///// </summary>
        ///// <returns></returns>
        //public (ReadOnlyCollection<SourceChar> value, SourceReader nextReader) Read(string value)
        //{
        //    var @string = new List<SourceChar>();
        //    var reader = default(SourceReader);
        //    foreach (var @char in value)
        //    {
        //        var (outChar, outReader) = this.Read(@char);
        //        @string.Add(outChar);
        //        reader = outReader;
        //    }
        //    return (new ReadOnlyCollection<SourceChar>(@string), reader);
        //}

        private SourceReader next;

        public SourceReader Next()
        {
            if (this.next == null)
            {
                this.next = new SourceReader.Builder
                {
                    Stream = this.Stream,
                    Position = this.Position + 1
                }.Build();
            }
            return this.next;
        }

        #endregion

        #region Factory Methods

        public static SourceReader FromTextReader(TextReader value)
        {
            var sourceStream = SourceStream.FromTextReader(value);
            return new SourceReader.Builder
            {
                Stream = sourceStream,
                Position = 0
            }.Build();
        }

        public static SourceReader FromString(string value)
        {
            var sourceStream = SourceStream.FromString(value);
            return new SourceReader.Builder
            {
                Stream = sourceStream,
                Position = 0
            }.Build();
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
