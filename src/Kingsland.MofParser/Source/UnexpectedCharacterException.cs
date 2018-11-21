using System;

namespace Kingsland.MofParser.Source
{

    public sealed class UnexpectedCharacterException : Exception
    {

        #region Constructors

        internal UnexpectedCharacterException()
        {
        }

        internal UnexpectedCharacterException(SourceChar foundChar)
        {
            this.FoundChar = foundChar ?? throw new ArgumentNullException("foundChar");
        }

        internal UnexpectedCharacterException(SourceChar foundChar, char expectedChar)
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
                    return $"Unexpected character '{this.FoundChar.Value}' found at" +
                           $"Position {foundPosition.Position}, " +
                           $"Line Number {foundPosition.LineNumber}, " +
                           $"Column Number {foundPosition.ColumnNumber} " +
                           $"while looking for character '{this.ExpectedChar.Value}'.";
                }
                else
                {
                    return $"Unexpected character '{this.FoundChar.Value}' found at" +
                           $"Position {foundPosition.Position}, " +
                           $"Line Number {foundPosition.LineNumber}, " +
                           $"Column Number {foundPosition.ColumnNumber}";
                }
            }
        }

        #endregion

    }

}
