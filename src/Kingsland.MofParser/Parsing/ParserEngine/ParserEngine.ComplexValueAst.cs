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
    ///     complexValue = aliasIdentifier /
    ///                    ( VALUE OF
    ///                      ( structureName / className / associationName )
    ///                      propertyValueList )
    ///
    /// </remarks>
    public static ComplexValueAst ParseComplexValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new ComplexValueAst.Builder();

        if (stream.TryPeek<AliasIdentifierToken>())
        {

            // aliasIdentifier
            node.Alias = stream.Read<AliasIdentifierToken>();

        }
        else
        {

            // VALUE
            node.Value = stream.ReadIdentifierToken(Constants.VALUE);

            // OF
            node.Of = stream.ReadIdentifierToken(Constants.OF);

            // ( structureName / className / associationName )
            node.TypeName = stream.ReadIdentifierToken(
                t => StringValidator.IsStructureName(t.Name) &&
                     StringValidator.IsClassName(t.Name) &&
                     StringValidator.IsAssociationName(t.Name)
            );

            // propertyValueList
            node.PropertyValues = ParserEngine.ParsePropertyValueListAst(stream, quirks);

        }

        // return the result
        return node.Build();

    }

    #endregion

}
