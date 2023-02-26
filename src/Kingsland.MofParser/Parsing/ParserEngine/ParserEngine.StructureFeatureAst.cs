using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.1 Structure declaration

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.1 Structure declaration
    ///
    ///     structureFeature       = structureDeclaration /   ; local structure
    ///                              enumerationDeclaration / ; local enumeration
    ///                              propertyDeclaration
    ///
    ///     structureDeclaration   = [ qualifierList ] STRUCTURE structureName
    ///                              [ superStructure ]
    ///                              "{" *structureFeature "}" ";"
    ///
    /// 7.5.4 Enumeration declaration
    ///
    ///     enumerationDeclaration = enumTypeHeader enumName ":" enumTypeDeclaration ";"
    ///
    ///     enumTypeHeader         = [qualifierList] ENUMERATION
    ///
    /// 7.5.5 Property declaration
    ///
    ///     propertyDeclaration    = [ qualifierList ] ( primitivePropertyDeclaration /
    ///                              complexPropertyDeclaration /
    ///                              enumPropertyDeclaration /
    ///                              referencePropertyDeclaration ) ";"
    ///
    ///     primitivePropertyDeclaration = primitiveType propertyName [ array ]
    ///                                    [ "=" primitiveTypeValue ]
    ///
    ///     complexPropertyDeclaration   = structureOrClassName propertyName [ array ]
    ///                                    [ "=" ( complexTypeValue / aliasIdentifier ) ]
    ///
    ///     enumPropertyDeclaration      = enumName propertyName [ array ]
    ///                                    [ "=" enumTypeValue ]
    ///
    ///     referencePropertyDeclaration = classReference propertyName [ array ]
    ///                                    [ "=" referenceTypeValue ]
    ///
    /// </remarks>
    public static IStructureFeatureAst ParseStructureFeatureAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        // all structureFeatures start with an optional "[ qualifierList ]"
        var qualifierList = (stream.TryPeek<AttributeOpenToken>())
            ? ParserEngine.ParseQualifierListAst(stream, quirks)
            : new QualifierListAst();

        // we now need to work out if it's a structureDeclaration, enumerationDeclaration,
        // or propertyDeclaration

        // structureDeclaration   => STRUCTURE
        // enumerationDeclaration => ENUMERATION
        // propertyDeclaration    => primitiveType / structureOrClassName / enumName / classReference

        if (!stream.TryPeek<IdentifierToken>(out var identifier))
        {
            throw new UnexpectedTokenException(stream.Peek());
        }
        else if (identifier.IsKeyword(Constants.STRUCTURE))
        {
            // structureDeclaration
            return ParserEngine.ParseStructureDeclarationAst(stream, qualifierList, quirks);
        }
        else if (identifier.IsKeyword(Constants.ENUMERATION))
        {
            // enumerationDeclaration
            return ParserEngine.ParseEnumerationDeclarationAst(stream, qualifierList, quirks);
        }
        else
        {
            // propertyDeclaration
            return ParserEngine.ParsePropertyDeclarationAst(stream, qualifierList, quirks);
        }

    }

    #endregion

}
