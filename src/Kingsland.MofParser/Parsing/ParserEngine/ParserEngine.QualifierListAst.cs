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
    ///     qualifierList = "[" qualifierValue *( "," qualifierValue ) "]"
    ///
    /// </remarks>
    public static QualifierListAst ParseQualifierListAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new QualifierListAst.Builder();

        // "["
        var attributeOpen = stream.Read<AttributeOpenToken>();

        // qualifierValue
        node.QualifierValues.Add(
            ParserEngine.ParseQualifierValueAst(stream, quirks)
        );

        // *( "," qualifierValue )
        while (stream.TryRead<CommaToken>(out var comma))
        {
            node.QualifierValues.Add(
                ParserEngine.ParseQualifierValueAst(stream, quirks)
            );
        }

        // "]"
        var attributeClose = stream.Read<AttributeCloseToken>();

        return node.Build();

    }

    #endregion

}
