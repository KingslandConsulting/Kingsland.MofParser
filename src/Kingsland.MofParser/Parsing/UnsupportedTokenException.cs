using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Parsing
{

    public sealed class UnsupportedTokenException : Exception
    {

        #region Constructors

        internal UnsupportedTokenException()
        {
        }

        internal UnsupportedTokenException(Token foundToken)
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
                    var message = "Unhandled token found.";
                    return message;
                }
                else
                {
                    var template = "Unhandled token found at Position {0}, Line Number {1}, Column Number {2}.\r\n" +
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
