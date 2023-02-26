using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.1 Primitive type value

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1 Primitive type value
    ///
    ///     primitiveTypeValue = literalValue / literalValueArray
    ///
    /// </remarks>
    public static PrimitiveTypeValueAst ParsePrimitiveTypeValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        if (!stream.TryPeek<BlockOpenToken>())
        {
            // literalValue
            return ParserEngine.ParseLiteralValueAst(stream, quirks);
        }
        else
        {
            // literalValueArray
            return ParserEngine.ParseLiteralValueArrayAst(stream, quirks);
        }
    }

    #endregion

}
