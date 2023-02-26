using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.3 Enum type value

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.3 Enum type value
    ///
    ///     enumTypeValue = enumValue / enumValueArray
    ///
    /// </remarks>
    public static EnumTypeValueAst ParseEnumTypeValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        if (!stream.TryPeek<BlockOpenToken>())
        {
            // enumValue
            return ParserEngine.ParseEnumValueAst(stream, quirks);
        }
        else
        {
            // enumValueArray
            return ParserEngine.ParseEnumValueArrayAst(stream, quirks);
        }
    }

    #endregion

}
