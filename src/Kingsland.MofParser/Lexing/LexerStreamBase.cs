using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Lexing
{

    public abstract class LexerStreamBase : ILexerStream
    {

        #region Constructors

        protected LexerStreamBase()
        {
            this.LastChar = null;
            this.Position = 0;
            this.LineNumber = 1;
            this.ColumnNumber = 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Used for tracking line breaks
        /// </summary>
        private SourceChar LastChar
        { 
            get;
            set;
        }

        public int Position
        {
            get;
            protected set;
        }

        public abstract int Length
        {
            get;
        }

        public int LineNumber
        {
            get;
            protected set;
        }

        public int ColumnNumber
        {
            get;
            protected set;
        }

        public bool Eof
        {
            get
            {
                return (this.Position >= this.Length);
            }
        }

        #endregion

        #region Peek Methods

        /// <summary>
        /// Reads the next character off of the input stream, but does not advance the current position.
        /// </summary>
        /// <returns></returns>
        public abstract char Peek();

        /// <summary>
        /// Returns true if the next character off of the input stream matches the specified value.
        /// </summary>
        /// <returns></returns>
        public bool PeekChar(char value)
        {
            var peek = this.Peek();
            return (peek == value);
        }

        /// <summary>
        /// Returns true if the next character off of the input stream is a digit.
        /// </summary>
        /// <returns></returns>
        public bool PeekDigit()
        {
            var peek = this.Peek();
            return char.IsDigit(peek);
        }

        /// <summary>
        /// Returns true if the next character off of the input stream is a whitespace character.
        /// </summary>
        /// <returns></returns>
        public bool PeekWhitespace()
        {
            var peek = this.Peek();
            return StringValidator.IsWhitespace(peek);
        }

        #endregion

        #region Read Methods

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// </summary>
        /// <returns></returns>
        public SourceChar Read()
        {
            var sourceChar = new SourceChar(this.Peek(), this.Position, this.LineNumber, this.ColumnNumber);
            switch (sourceChar.Value)
            {
                case '\r':
                    this.LineNumber += 1;
                    this.ColumnNumber = 1;
                    break;
                case '\n':
                    var lastChar = this.LastChar;
                    if((lastChar != null) && (lastChar.Value == '\r'))
                    {
                        sourceChar = new SourceChar(this.Peek(), this.Position, lastChar.LineNumber, lastChar.ColumnNumber + 1);
                    }
                    else
                    {
                        this.LineNumber += 1;
                        this.ColumnNumber = 1;
                    }
                    break;
                default:
                    this.ColumnNumber += 1;
                    break;
            }
            this.Position += 1;
            this.LastChar = sourceChar;
            return sourceChar;
        }

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character does not match the specified value.
        /// </summary>
        /// <returns></returns>
        public SourceChar ReadChar(char value)
        {
            var @char = this.Peek();
            if (@char != value)
            {
                throw new InvalidOperationException(
                    string.Format("Unexpected character '{0}' encountered", @char));
            }
            return this.Read();
        }

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character is not a letter.
        /// </summary>
        /// <returns></returns>
        public SourceChar ReadDigit()
        {
            var @char = this.Peek();
            if (!char.IsDigit(@char))
            {
                throw new InvalidOperationException(
                    string.Format("Unexpected character '{0}' encountered", @char));
            }
            return this.Read();
        }

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character is not a letter.
        /// </summary>
        /// <returns></returns>
        public SourceChar ReadLetter()
        {
            var @char = this.Peek();
            if (!char.IsLetter(@char))
            {
                throw new InvalidOperationException(
                    string.Format("Unexpected character '{0}' encountered", @char));
            }
            return this.Read();
        }

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character is not a whitespace character.
        /// </summary>
        /// <returns></returns>
        public SourceChar ReadWhitespace()
        {
            var @char = this.Peek();
            if (!char.IsWhiteSpace(@char))
            {
                throw new InvalidOperationException(
                    string.Format("Unexpected character '{0}' encountered", @char));
            }
            return this.Read();
        }

        #endregion

    }

}
