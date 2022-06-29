namespace Kingsland.ParseFx.Lexing.Matches;

public sealed class CharArrayMatch : IMatch
{

    #region Constructors

    public CharArrayMatch(char[] values)
    {
        this.Values = values;
    }

    #endregion

    #region Properties

    public char[] Values
    {
        get;
        private set;
    }

    #endregion

    #region LexerRule Members

    public bool Matches(char value)
    {
        return this.Values.Contains(value);
    }

    #endregion

}
