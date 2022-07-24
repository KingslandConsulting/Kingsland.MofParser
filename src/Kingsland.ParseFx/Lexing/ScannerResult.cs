using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.ParseFx.Lexing;

public sealed class ScannerResult
{

    #region Constructors

    public ScannerResult(SyntaxToken token, SourceReader reader)
    {
        this.Token = token ?? throw new ArgumentNullException(nameof(token));
        this.NextReader = reader ?? throw new ArgumentNullException(nameof(reader));
    }

    #endregion

    #region Properties

    public SyntaxToken Token
    {
        get;
    }

    public SourceReader NextReader
    {
        get;
    }

    #endregion

}
