using Kingsland.ParseFx.Syntax;
using System;

namespace Kingsland.ParseFx.Parsing
{

    public sealed class UnexpectedTokenException : Exception
    {

        #region Constructors

        internal UnexpectedTokenException()
        {
        }

        public UnexpectedTokenException(SyntaxToken foundToken)
        {
            this.FoundToken = foundToken ?? throw new ArgumentNullException("foundToken");
        }

        #endregion

        #region Properties

        public SyntaxToken FoundToken
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
                    return $"Unexpected token found at Position {startPosition.Position}, Line Number {startPosition.LineNumber}, Column Number {startPosition.ColumnNumber}.\r\n" +
                           $"Token Type: '{token.GetType().Name}'\r\n" +
                           $"Token Text: '{token.Extent.Text}'";
                }
            }
        }

        #endregion

    }

}
