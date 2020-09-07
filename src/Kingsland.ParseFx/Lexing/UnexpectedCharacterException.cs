using Kingsland.ParseFx.Text;
using System;

namespace Kingsland.ParseFx.Lexing
{

    public sealed class UnexpectedCharacterException : Exception
    {

        #region Constructors

        private UnexpectedCharacterException()
        {
        }

        public UnexpectedCharacterException(SourceChar foundChar)
        {
            this.FoundChar = foundChar ?? throw new ArgumentNullException("foundChar");
        }

        public UnexpectedCharacterException(SourceChar foundChar, char expectedChar)
        {
            this.FoundChar = foundChar ?? throw new ArgumentNullException("foundChar");
            this.ExpectedChar = expectedChar;
        }

        #endregion

        #region Properties

        public SourceChar FoundChar
        {
            get;
            private set;
        }

        public char? ExpectedChar
        {
            get;
            private set;
        }

        public override string Message
        {
            get
            {
                var foundPosition = this.FoundChar.Position;
                if (this.ExpectedChar.HasValue)
                {
                    return $"Unexpected character '{this.FoundChar.Value}' found at " +
                           $"Position {foundPosition.Position}, " +
                           $"Line Number {foundPosition.LineNumber}, " +
                           $"Column Number {foundPosition.ColumnNumber} " +
                           $"while looking for character '{this.ExpectedChar.Value}'.";
                }
                else
                {
                    return $"Unexpected character '{this.FoundChar.Value}' found at " +
                           $"Position {foundPosition.Position}, " +
                           $"Line Number {foundPosition.LineNumber}, " +
                           $"Column Number {foundPosition.ColumnNumber}";
                }
            }
        }

        #endregion

    }

}
