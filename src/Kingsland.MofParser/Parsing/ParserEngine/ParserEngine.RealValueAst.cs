using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.1.2 Real value

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    ///
    ///     realValue            = [ "+" / "-" ] * decimalDigit "." 1*decimalDigit
    ///                            [ ("e" / "E") [ "+" / "-" ] 1*decimalDigit ]
    ///
    ///     decimalDigit         = "0" / positiveDecimalDigit
    ///
    ///     positiveDecimalDigit = "1"..."9"
    ///
    /// </remarks>
    public static RealValueAst ParseRealValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        var node = new RealValueAst.Builder();
        node.RealLiteralToken = stream.Read<RealLiteralToken>();
        return node.Build();
    }

    #endregion

}
