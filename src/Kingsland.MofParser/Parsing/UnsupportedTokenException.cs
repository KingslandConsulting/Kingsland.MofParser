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
                    var template = "Unhandled token '{0}' found at Position {1}, Line Number {2}, Column Number {3}.";
                    var token = this.FoundToken;
                    var extent = this.FoundToken.Extent;
                    var message = string.Format(template, token.GetType().Name, extent.StartPosition, extent.StartLineNumber, extent.StartColumnNumber);
                    return message;
                }
            }
        }

        #endregion

    }

}
