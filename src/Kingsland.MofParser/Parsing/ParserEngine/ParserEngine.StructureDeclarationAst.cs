using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.1 Structure declaration

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="qualifierList"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.1 Structure declaration
    ///
    ///     structureDeclaration = [ qualifierList ] STRUCTURE structureName
    ///                            [ superStructure ]
    ///                            "{" *structureFeature "}" ";"
    ///
    ///     structureName        = elementName
    ///
    ///     superStructure       = ":" structureName
    ///
    ///     structureFeature     = structureDeclaration /   ; local structure
    ///                            enumerationDeclaration / ; local enumeration
    ///                            propertyDeclaration
    ///
    ///     STRUCTURE            = "structure" ; keyword: case insensitive
    ///
    /// </remarks>
    public static StructureDeclarationAst ParseStructureDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new StructureDeclarationAst.Builder();

        // [ qualifierList ]
        node.QualifierList = qualifierList;

        // STRUCTURE
        var structureKeyword = stream.ReadIdentifierToken(Constants.STRUCTURE);

        // structureName
        node.StructureName = stream.ReadIdentifierToken(
            t => StringValidator.IsStructureName(t.Name)
        );

        // [ superStructure ]
        {
            // ":"
            if (stream.TryRead<ColonToken>(out var colon))
            {
                // structureName
                node.SuperStructure = stream.ReadIdentifierToken(
                    t => StringValidator.IsStructureName(t.Name)
                );
            }
        }

        // "{"
        var blockOpen = stream.Read<BlockOpenToken>();

        // *structureFeature
        while (!stream.TryPeek<BlockCloseToken>())
        {
            node.StructureFeatures.Add(
                ParserEngine.ParseStructureFeatureAst(stream, quirks)
            );
        }

        // "}"
        var blockClose = stream.Read<BlockCloseToken>();

        // ";"
        var statementEnd = stream.Read<StatementEndToken>();

        return node.Build();

    }

    #endregion

}
