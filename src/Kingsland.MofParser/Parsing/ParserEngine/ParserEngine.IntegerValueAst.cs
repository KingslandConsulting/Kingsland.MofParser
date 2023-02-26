using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.1.1 Integer value

    /// <summary>
    ///
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1.1 Integer value
    ///
    ///     integerValue = binaryValue / octalValue / hexValue / decimalValue
    ///
    /// </remarks>
    public static IntegerValueAst ParseIntegerValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        var node = new IntegerValueAst.Builder();
        node.IntegerLiteralToken = stream.Read<IntegerLiteralToken>();
        return node.Build();
    }

    #endregion

}
