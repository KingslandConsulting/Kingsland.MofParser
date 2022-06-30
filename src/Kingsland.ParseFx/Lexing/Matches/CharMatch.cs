namespace Kingsland.ParseFx.Lexing.Matches;

public sealed class CharMatch : IMatch
{

    #region Constructors

    public CharMatch(char value)
    {
        this.Value = value;
    }

    #endregion

    #region Properties

    public char Value
    {
        get;
    }

    #endregion

    #region LexerRule Members

    public bool Matches(char value)
    {
        return (value == this.Value);
    }

    #endregion

}
