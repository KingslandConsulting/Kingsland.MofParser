using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.4 Reference type value

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.4 Reference type value
    ///
    ///     referenceTypeValue = objectPathValue / objectPathValueArray
    ///
    /// </remarks>
    public static PrimitiveTypeValueAst ParseReferenceTypeValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        if (stream.TryPeek<BlockOpenToken>())
        {
            // objectPathValueArray
            return ParserEngine.ParseObjectPathValueArrayAst(stream, quirks);
        }
        else
        {
            // objectPathValue
            return ParserEngine.ParseObjectPathValueAst(stream, quirks);
        }
    }

    #endregion

}
