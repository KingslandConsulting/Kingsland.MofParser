namespace Kingsland.MofParser.Lexing
{

    public interface ILexerStream
    {

        #region Properties

        int Position
        {
            get;
        }

        int Length
        {
            get;
        }

        int LineNumber
        {
            get;
        }

        int Column
        {
            get;
        }

        /// <summary>
        /// Returns true if the current position is beyond the end of the input stream.
        /// </summary>
        bool Eof
        {
            get;
        }

        #endregion

        #region Peek Methods

        /// <summary>
        /// Reads the next character off of the input stream, but does not advance the current position.
        /// </summary>
        /// <returns></returns>
        char Peek();

        /// <summary>
        /// Returns true if the next character off of the input stream matches the specified value.
        /// </summary>
        /// <returns></returns>
        bool PeekChar(char value);

        /// <summary>
        /// Returns true if the next character off of the input stream is a digit.
        /// </summary>
        /// <returns></returns>
        bool PeekDigit();

        /// <summary>
        /// Returns true if the next character off of the input stream is a whitespace character.
        /// </summary>
        /// <returns></returns>
        bool PeekWhitespace();

        #endregion

        #region Read Methods

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// </summary>
        /// <returns></returns>
        char Read();

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character does not match the specified value.
        /// </summary>
        /// <returns></returns>
        char ReadChar(char value);

        /// <summary>
        /// Reads the next string off of the input stream and advances the current position.
        /// Throws an exception if the character does not match the specified value.
        /// </summary>
        /// <returns></returns>
        string ReadString(string str);

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character is not a digit.
        /// </summary>
        /// <returns></returns>
        char ReadDigit();

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character is not a letter.
        /// </summary>
        /// <returns></returns>
        char ReadLetter();

        /// <summary>
        /// Reads the next character off of the input stream and advances the current position.
        /// Throws an exception if the character is not a whitespace character.
        /// </summary>
        /// <returns></returns>
        char ReadWhitespace();

        /// <summary>
        /// Moves the stream position back a token.
        /// </summary>
        void Backtrack();

        #endregion

    }

}
