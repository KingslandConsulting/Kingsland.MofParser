using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
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
    ///     propertyValueList = "{" *propertySlot "}"
    ///
    /// </remarks>
    internal static PropertyValueListAst ParsePropertyValueListAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new PropertyValueListAst.Builder();

        // "{"
        stream.Read<BlockOpenToken>();

        // *propertySlot
        while (!stream.TryPeek<BlockCloseToken>())
        {
            node.PropertySlots.Add(
                ParserEngine.ParsePropertySlotAst(
                    stream, quirks
                )
            );
        }

        // "}"
        stream.Read<BlockCloseToken>();

        return node.Build();

    }

    #endregion

}
