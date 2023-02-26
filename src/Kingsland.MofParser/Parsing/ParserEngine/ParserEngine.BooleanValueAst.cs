using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.1.5 Boolean value

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1.5 Boolean value
    ///
    ///     booleanValue = TRUE / FALSE
    ///
    ///     FALSE        = "false" ; keyword: case insensitive
    ///     TRUE         = "true"  ; keyword: case insensitive
    ///
    /// </remarks>
    public static BooleanValueAst ParseBooleanValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        return new BooleanValueAst(
            stream.Read<BooleanLiteralToken>()
        );
    }

    #endregion

}
