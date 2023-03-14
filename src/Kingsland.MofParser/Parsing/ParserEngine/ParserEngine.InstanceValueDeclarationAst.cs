using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
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
    ///     instanceValueDeclaration = INSTANCE OF ( className / associationName )
    ///                                [ alias ]
    ///                                propertyValueList ";"
    ///
    ///     alias                    = AS aliasIdentifier
    ///
    /// </remarks>
    public static InstanceValueDeclarationAst ParseInstanceValueDeclarationAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new InstanceValueDeclarationAst.Builder();

        // INSTANCE
        node.Instance = stream.ReadIdentifierToken(Constants.INSTANCE);

        // OF
        node.Of = stream.ReadIdentifierToken(Constants.OF);

        // ( className / associationName )
        node.TypeName = stream.ReadIdentifierToken(
             t => StringValidator.IsClassName(t.Name) ||
                  StringValidator.IsAssociationName(t.Name)
        );

        // [ alias ]
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
