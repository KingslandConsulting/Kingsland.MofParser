using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.2 Complex type value

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// 7.6.2 Complex type value
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    ///     structureValueDeclaration = VALUE OF
    ///                                 ( className / associationName / structureName )
    ///                                 alias
    ///                                 propertyValueList ";"
    ///
    ///     alias                     = AS aliasIdentifier
    ///
    /// </remarks>
    public static StructureValueDeclarationAst ParseStructureValueDeclarationAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new StructureValueDeclarationAst.Builder();

        // VALUE
        node.Value = stream.ReadIdentifierToken(Constants.VALUE);

        // OF
        node.Of = stream.ReadIdentifierToken(Constants.OF);

        // ( className / associationName / structureName )
        node.TypeName = stream.ReadIdentifierToken(
             t => StringValidator.IsClassName(t.Name) ||
                  StringValidator.IsAssociationName(t.Name) ||
                  StringValidator.IsStructureName(t.Name)
        );

        // [alias]
        {
            // AS
            if (stream.TryReadIdentifierToken(Constants.AS, out var @as))
            {
                node.As = @as;
                // aliasIdentifier
                node.Alias = stream.Read<AliasIdentifierToken>();
            }
        }

        // propertyValueList
        node.PropertyValues = ParserEngine.ParsePropertyValueListAst(stream, quirks);

        // ";"
        node.StatementEnd = stream.Read<StatementEndToken>();

        return node.Build();

    }

    #endregion

}
