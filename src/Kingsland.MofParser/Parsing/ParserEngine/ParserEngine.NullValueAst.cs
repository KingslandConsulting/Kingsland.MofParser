using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.1.6 Null value

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// 7.6.1.6 Null value
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    ///     nullValue = NULL
    ///
    ///     NULL      = "null" ; keyword: case insensitive
    ///                        ; second
    ///
    /// </remarks>
    public static NullValueAst ParseNullValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        return new NullValueAst.Builder
        {
            Token = stream.Read<NullLiteralToken>()
        }.Build();
    }

    #endregion

}
