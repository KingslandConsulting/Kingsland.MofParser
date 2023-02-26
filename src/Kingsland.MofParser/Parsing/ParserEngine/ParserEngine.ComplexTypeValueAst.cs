using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.9 Complex type value

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.9 Complex type value
    ///
    ///     complexTypeValue = complexValue / complexValueArray
    ///
    /// </remarks>
    public static ComplexTypeValueAst ParseComplexTypeValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        if (!stream.TryPeek<BlockOpenToken>())
        {
            // complexValue
            return ParserEngine.ParseComplexValueAst(stream, quirks);
        }
        else
        {
            // complexValueArray
            return ParserEngine.ParseComplexValueArrayAst(stream, quirks);
        }
    }

    #endregion

}
