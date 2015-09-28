﻿using System;

namespace Kingsland.MofParser.Lexing
{

    public class UnexpectedCharacterException : Exception
    {

        #region Constructors

        internal UnexpectedCharacterException()
        {
        }

        internal UnexpectedCharacterException(SourceChar foundChar)
        {
            if(foundChar == null)
            {
                throw new ArgumentNullException("foundChar");
            }
            this.FoundChar = foundChar;
        }

        internal UnexpectedCharacterException(SourceChar foundChar, char expectedChar)
        {
            if (foundChar == null)
            {
                throw new ArgumentNullException("foundChar");
            }
            this.FoundChar = foundChar;
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

        public override String Message
        {
            get
            {
                var message = default(string);
                if (this.ExpectedChar.HasValue)
                {
                    var template = "Unexpected character '{0}' found at Position {1}, Line Number {2}, Column Number {3} while looking for character '{4}.";
                    message = string.Format(template, this.FoundChar.Value, this.FoundChar.Position, this.FoundChar.LineNumber, this.FoundChar.ColumnNumber, this.ExpectedChar.Value);
                }
                else
                {
                    var template = "Unexpected character '{0}' found at Position {1}, Line Number {2}, Column Number {3}.";
                    message = string.Format(template, this.FoundChar.Value, this.FoundChar.Position, this.FoundChar.LineNumber, this.FoundChar.ColumnNumber);
                }
                return message;
            }
        }

        #endregion

    }

}
