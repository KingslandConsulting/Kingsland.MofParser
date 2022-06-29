using Kingsland.ParseFx.Lexing.Matches;
using Kingsland.ParseFx.Text;

namespace Kingsland.ParseFx.Lexing;

public sealed class Scanner
{

    #region Delegates

    public delegate ScannerResult ScannerAction(SourceReader reader);

    #endregion

    #region Constructors

    internal Scanner(IMatch match, ScannerAction action)
    {
        this.Match = match ?? throw new ArgumentNullException(nameof(match));
        this.Action = action?? throw new ArgumentNullException(nameof(action));
    }

    #endregion

    #region Properties

    public IMatch Match
    {
        get;
        private set;
    }

    public ScannerAction Action
    {
        get;
        private set;
    }

    #endregion

}
