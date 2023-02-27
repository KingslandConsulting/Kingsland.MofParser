using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.1.2 Real value
///
///     realValue            = [ "+" / "-" ] * decimalDigit "." 1*decimalDigit
///                            [ ("e" / "E") [ "+" / "-" ] 1*decimalDigit ]
///
///     decimalDigit         = "0" / positiveDecimalDigit
///
///     positiveDecimalDigit = "1"..."9"
///
/// </remarks>
public sealed record RealValueAst : LiteralValueAst
{

    #region Builder

    public sealed class Builder
    {

        public RealLiteralToken? RealLiteralToken
        {
            get;
            set;
        }

        public RealValueAst Build()
        {
            return new(
                this.RealLiteralToken ?? throw new InvalidOperationException(
                    $"{nameof(this.RealLiteralToken)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal RealValueAst(
        RealLiteralToken realLiteralToken
    )
    {
        this.RealLiteralToken = realLiteralToken ?? throw new ArgumentNullException(nameof(realLiteralToken));
        this.Value = realLiteralToken.Value;
    }

    #endregion

    #region Properties

    public RealLiteralToken RealLiteralToken
    {
        get;
    }

    public double Value
    {
        get;
    }

    #endregion

}
