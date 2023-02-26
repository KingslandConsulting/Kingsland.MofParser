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
    ///     complexValueArray = "{" [ complexValue *( "," complexValue) ] "}"
    ///
    /// </remarks>
    public static ComplexValueArrayAst ParseComplexValueArrayAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        // complexValueArray =
        var node = new ComplexValueArrayAst.Builder();

        // "{"
        stream.Read<BlockOpenToken>();
        if (!stream.TryPeek<BlockCloseToken>())
        {

            // complexValue
            node.Values.Add(
                ParserEngine.ParseComplexValueAst(stream, quirks)
            );

            // *( "," complexValue)
            while (stream.TryRead<CommaToken>(out var comma))
            {
                node.Values.Add(
                    ParserEngine.ParseComplexValueAst(stream, quirks)
                );
            }

        }

        // "}"
        stream.Read<BlockCloseToken>();

        // return the result
        return node.Build();

    }

    #endregion

}
