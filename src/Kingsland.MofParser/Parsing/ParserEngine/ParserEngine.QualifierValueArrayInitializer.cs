using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.4.1 QualifierList

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.4.1 QualifierList
    ///
    ///     qualiferValueArrayInitializer = "{" literalValue *( "," literalValue ) "}"
    ///
    /// </remarks>
    public static QualifierValueArrayInitializerAst ParseQualifierValueArrayInitializer(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new QualifierValueArrayInitializerAst.Builder();

        // "{"
        var blockOpen = stream.Read<BlockOpenToken>();

        // check if we allow empty qualifier arrays
        // see https://github.com/mikeclayton/MofParser/issues/51
        var quirkEnabled = quirks.HasFlag(ParserQuirks.AllowEmptyQualifierValueArrays);
        if (quirkEnabled && stream.TryPeek<BlockCloseToken>())
        {
            // this is an empty array, and the quirk to allow empty arrays is enabled,
            // so skip trying to read the array values
        }
        else
        {
            // literalValue
            node.Values.Add(
                ParserEngine.ParseLiteralValueAst(stream, quirks)
            );
            // *( "," literalValue )
            while (stream.TryRead<CommaToken>(out var comma))
            {
                node.Values.Add(
                    ParserEngine.ParseLiteralValueAst(stream, quirks)
                );
            }
        }

        // "}"
        var blockClose = stream.Read<BlockCloseToken>();

        return node.Build();

    }

    #endregion

}
