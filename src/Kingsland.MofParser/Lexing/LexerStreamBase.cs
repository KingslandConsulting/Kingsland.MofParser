using System;

namespace Kingsland.MofParser.Lexing
{

    public abstract class LexerStreamBase : ILexerStream
    {

        #region Constructors

        protected LexerStreamBase()
        {
            this.Position = 0;
            this.LineNumber = 0;
            this.Column = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Used for tracking line breaks
        /// </summary>
        private char LastChar
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

        public int Column
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
            return char.IsWhiteSpace(peek);
        }

        #endregion

        #region Read Methods

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// </summary>
        /// <returns></returns>
        public char Read()
        {
            var value = this.Peek();
            switch (value)
            {
                case '\r':
                    this.LineNumber += 1;
                    this.Column = 0;
                    break;
                case '\n':
                    if ((this.Position == 0) || (this.LastChar != '\r'))
                    {
                        this.LineNumber += 1;
                        this.Column = 0;
                    }
                    break;
                default:
                    this.Column += 1;
                    break;
            }
            this.Position += 1;
            this.LastChar = value;
            return value;
        }

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character does not match the specified value.
        /// </summary>
        /// <returns></returns>
        public char ReadChar(char value)
        {
            var @char = this.Read();
            if (@char != value)
            {
                throw new InvalidOperationException(
                    string.Format("Unexpected character '{0}' encountered", @char));
            }
            return @char;
        }

        /// <summary>
        /// Reads the next string off of the input stream and advances the current position.
        /// Throws an exception if the character does not match the specified value.
        /// </summary>
        /// <returns></returns>
        public string ReadString(string str)
        {
            foreach (var c in str)
            {
                ReadChar(c);
            }

            return str;
        }

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character is not a letter.
        /// </summary>
        /// <returns></returns>
        public char ReadDigit()
        {
            var @char = this.Read();
            if (!char.IsDigit(@char))
            {
                throw new InvalidOperationException(
                    string.Format("Unexpected character '{0}' encountered", @char));
            }
            return @char;
        }

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character is not a letter.
        /// </summary>
        /// <returns></returns>
        public char ReadLetter()
        {
            var @char = this.Read();
            if (!char.IsLetter(@char))
            {
                throw new InvalidOperationException(
                    string.Format("Unexpected character '{0}' encountered", @char));
            }
            return @char;
        }

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character is not a whitespace character.
        /// </summary>
        /// <returns></returns>
        public char ReadWhitespace()
        {
            var @char = this.Read();
            if (!char.IsWhiteSpace(@char))
            {
                throw new InvalidOperationException(
                    string.Format("Unexpected character '{0}' encountered", @char));
            }
            return @char;
        }

        /// <summary>
        /// Moves the stream position back a character.
        /// </summary>
        public void Backtrack()
        {
            if (this.Position == 0)
            {
                throw new InvalidOperationException();
            }
            this.Position -= 1;
            // BUGBUG: this needs to handle line breaks properly, otherwise we get out of sync.
            //         probably need a stack of { position, line, column } tuples so we can re-hydrate
            this.Column -= 1;
        }

        #endregion

    }

}
