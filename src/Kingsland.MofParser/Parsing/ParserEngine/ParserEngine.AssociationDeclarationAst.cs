using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.3 Association declaration

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
    /// 7.5.3 Association declaration
    ///
    ///     associationDeclaration = [ qualifierList ] ASSOCIATION associationName
    ///                              [ superAssociation ]
    ///                              "{" * classFeature "}" ";"
    ///
    ///     associationName        = elementName
    ///
    ///     superAssociation       = ":" elementName
    ///
    ///     ASSOCIATION            = "association" ; keyword: case insensitive
    ///
    /// </remarks>
    public static AssociationDeclarationAst ParseAssociationDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new AssociationDeclarationAst.Builder();

        // [ qualifierList ]
        node.QualifierList = qualifierList;

        // ASSOCIATION
        stream.ReadIdentifierToken(Constants.ASSOCIATION);

        // associationName
        node.AssociationName = stream.ReadIdentifierToken(
            t => StringValidator.IsAssociationName(t.Name)
        );

        // [ superAssociation ]
        if (stream.TryPeek<ColonToken>())
        {
            // ":"
            var colon = stream.Read<ColonToken>();
            // associationName
            node.SuperAssociation = stream.ReadIdentifierToken(
                t => StringValidator.IsAssociationName(t.Name)
            );
        }

        // "{"
        var blockOpen = stream.Read<BlockOpenToken>();

        // *classFeature
        while (!stream.TryPeek<BlockCloseToken>())
        {
            node.ClassFeatures.Add(
                ParserEngine.ParseClassFeatureAst(stream, quirks)
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
