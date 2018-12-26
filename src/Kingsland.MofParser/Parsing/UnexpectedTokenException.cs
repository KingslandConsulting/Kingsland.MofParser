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
                    var token = this.FoundToken;
                    var extent = this.FoundToken.Extent;
                    var startPosition = extent.StartPosition;
                    var endPostition = extent.EndPosition;
                    return $"Unexpected token found at Position {startPosition.Position}, Line Number {startPosition.LineNumber}, Column Number {startPosition.ColumnNumber}.\r\n" +
                           $"Token Type: '{token.GetType().Name}'\r\n" +
                           $"Token Text: '{token.Extent.Text}'";
                }
            }
        }

        #endregion

    }

}
