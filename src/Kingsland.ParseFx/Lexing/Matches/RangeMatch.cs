namespace Kingsland.ParseFx.Lexing.Matches;

public sealed class RangeMatch : IMatch
{

    #region Constructors

    public RangeMatch(char fromValue, char toValue)
    {
        if (fromValue > toValue)
        {
            throw new ArgumentException($"{nameof(fromValue)} must be less than {nameof(toValue)}.");
        }
        this.FromValue = fromValue;
        this.ToValue = toValue;
    }

    #endregion

    #region Properties

    public char FromValue
    {
        get;
    }

    public char ToValue
    {
        get;
    }

    #endregion

    #region LexerRule Members

    public bool Matches(char value)
    {
        return (value >= this.FromValue) && (value <= this.ToValue);
    }

    #endregion

}
