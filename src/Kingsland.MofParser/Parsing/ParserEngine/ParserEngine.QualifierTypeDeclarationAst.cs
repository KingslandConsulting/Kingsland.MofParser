using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.4 Qualifiers

    /// <summary>
    ///
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="qualifierList"></param>
    /// <param name="quirks"></param>
    /// <returns>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.4 Qualifiers
    ///
    ///     qualifierTypeDeclaration = [ qualifierList ] QUALIFIER qualifierName ":"
    ///                                qualifierType qualifierScope
    ///                                [ qualifierPolicy ] ";"
    ///
    ///     qualifierName            = elementName
    ///
    ///     qualifierType            = primitiveQualifierType / enumQualiferType
    ///
    ///     primitiveQualifierType   = primitiveType [ array ]
    ///                                [ "=" primitiveTypeValue ] ";"
    ///
    ///     enumQualiferType         = enumName [ array ] "=" enumTypeValue ";"
    ///
    ///     qualifierScope           = SCOPE "(" ANY / scopeKindList ")"
    ///
    /// </returns>
    public static QualifierTypeDeclarationAst ParseQualifierTypeDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new QualifierTypeDeclarationAst.Builder();

        // [ qualifierList ]
        node.QualifierList = qualifierList;

        // QUALIFIER
        node.QualifierKeyword = stream.ReadIdentifierToken(Constants.QUALIFIER);

        // qualifierName
        node.QualifierName = stream.Read<IdentifierToken>();

        // ":"
        var colon = stream.Read<ColonToken>();

        throw new NotImplementedException();

        // qualifierType

        // qualifierScope

        // [qualifierPolicy]

        // ";"
        //stream.Read<StatementEndToken>();

        //return node.Build();

    }

    #endregion

}
