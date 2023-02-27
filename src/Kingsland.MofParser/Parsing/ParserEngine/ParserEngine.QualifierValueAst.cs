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
    ///     qualifierValue = qualifierName [ qualifierValueInitializer /
    ///                      qualiferValueArrayInitializer ]
    ///
    /// 7.4 Qualifiers
    ///
    ///     qualifierName  = elementName
    ///
    /// </remarks>
    public static QualifierValueAst ParseQualifierValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new QualifierValueAst.Builder();

        // qualifierName
        node.QualifierName = stream.ReadIdentifierToken(
            t => StringValidator.IsElementName(t.Name)
        );

        // [ qualifierValueInitializer / qualiferValueArrayInitializer ]
        switch (stream.Peek())
        {
            case ParenthesisOpenToken parenthesisOpen:
                // qualifierValueInitializer
                node.Initializer = ParserEngine.ParseQualifierValueInitializer(stream, quirks);
                break;
            case BlockOpenToken blockOpen:
                // qualiferValueArrayInitializer
                node.Initializer = ParserEngine.ParseQualifierValueArrayInitializer(stream, quirks);
                break;
        }

        // see https://github.com/mikeclayton/MofParser/issues/49
        //
        // Pseudo-ABNF for MOF V2 qualifiers:
        //
        //     qualifierList_v2       = "[" qualifierValue_v2 *( "," qualifierValue_v2 ) "]"
        //     qualifierValue_v2      = qualifierName [ qualifierValueInitializer / qualiferValueArrayInitializer ] ":" qualifierFlavourList_v2
        //     qualifierFlavorList_v2 = qualifierFlavorName *( " " qualifierFlavorName )
        //
        var quirkEnabled = quirks.HasFlag(ParserQuirks.AllowMofV2Qualifiers);
        if (quirkEnabled)
        {
            if (stream.TryRead<ColonToken>(out var colon))
            {
                node.Flavors.Add(stream.Read<IdentifierToken>());
                while (stream.TryRead<IdentifierToken>(out var identifier))
                {
                    node.Flavors.Add(identifier!);
                }
            }
        }

        return node.Build();

    }

    #endregion

}
