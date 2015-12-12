using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Parsing
{

    public sealed class UnexpectedTokenException : Exception
    {

        #region Constructors

        internal UnexpectedTokenException()
        {
        }

        internal UnexpectedTokenException(Token foundToken)
        {
            if(foundToken == null)
            {
                throw new ArgumentNullException("foundToken");
            }
            this.FoundToken = foundToken;
        }

        #endregion

        #region Properties

        public Token FoundToken
        {
            get;
            private set;
        }

        public override String Message
        {
            get
            {
                if (this.FoundToken == null)
                {
                    var message = "Unexpected token found.";
                    return message;
                }
                else
                {
                    var template = "Unexpected token found at Position {0}, Line Number {1}, Column Number {2}.\r\n" +
                                   "Token Type: '{3}'\r\n" +
                                   "Token Text: '{4}'";
                    var token = this.FoundToken;
                    var extent = this.FoundToken.Extent;
                    var message = string.Format(template, extent.StartPosition, extent.StartLineNumber, extent.StartColumnNumber, token.GetType().Name, token);
                    return message;
                }
            }
        }

        #endregion

    }

}
