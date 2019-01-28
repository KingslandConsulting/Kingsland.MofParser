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

        public override string Message
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
                    var token = this.FoundToken;
                    var extent = this.FoundToken.Extent;
                    var startPosition = extent.StartPosition;
                    var endPosition = extent.EndPosition;
                    return $"Unhandled token found at Position {startPosition.Position}, Line Number {startPosition.LineNumber}, Column Number {startPosition.ColumnNumber}.\r\n" +
                           $"Token Type: '{token.GetType().Name}'\r\n" +
                           $"Token Text: '{extent.Text}'";
                }
            }
        }

        #endregion

    }

}
