using Kingsland.ParseFx.Syntax;

namespace Kingsland.ParseFx.Parsing;

public sealed class UnsupportedTokenException : Exception
{

    #region Constructors

    internal UnsupportedTokenException()
    {
    }

    public UnsupportedTokenException(SyntaxToken foundToken)
    {
        this.FoundToken = foundToken ?? throw new ArgumentNullException(nameof(foundToken));
    }

    #endregion

    #region Properties

    public SyntaxToken? FoundToken
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
                var newline = Environment.NewLine;
                var token = this.FoundToken;
                var extent = this.FoundToken.Extent;
                var startPosition = extent.StartPosition;
                return $"Unhandled token found at Position {startPosition.Position}, Line Number {startPosition.LineNumber}, Column Number {startPosition.ColumnNumber}.{newline}" +
                       $"Token Type: '{token.GetType().Name}'{newline}" +
                       $"Token Text: '{extent.Text}'";
            }
        }
    }

    #endregion

}
