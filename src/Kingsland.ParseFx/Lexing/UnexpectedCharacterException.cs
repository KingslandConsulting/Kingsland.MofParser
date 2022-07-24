using Kingsland.ParseFx.Text;

namespace Kingsland.ParseFx.Lexing;

public sealed class UnexpectedCharacterException : Exception
{

    #region Constructors

    public UnexpectedCharacterException(SourceChar foundChar)
    {
        this.FoundChar = foundChar;
    }

    public UnexpectedCharacterException(SourceChar foundChar, char expectedChar)
    {
        this.FoundChar = foundChar;
        this.ExpectedChar = expectedChar;
    }

    #endregion

    #region Properties

    public SourceChar FoundChar
    {
        get;
    }

    public char? ExpectedChar
    {
        get;
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
