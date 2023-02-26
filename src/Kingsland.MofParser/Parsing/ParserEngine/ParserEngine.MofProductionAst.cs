using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.2 MOF specification

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.2 MOF specification
    ///
    ///     mofProduction          = compilerDirective /
    ///                              structureDeclaration /
    ///                              classDeclaration /
    ///                              associationDeclaration /
    ///                              enumerationDeclaration /
    ///                              instanceValueDeclaration /
    ///                              structureValueDeclaration /
    ///                              qualifierTypeDeclaration
    ///
    /// 7.3 Compiler directives
    ///
    ///     compilerDirective      = PRAGMA ( pragmaName / standardPragmaName )
    ///                              "(" pragmaParameter ")"
    ///
    /// 7.5.1 Structure declaration
    ///
    ///     structureDeclaration   = [ qualifierList ] STRUCTURE structureName
    ///                              [ superStructure ]
    ///                              "{" *structureFeature "}" ";"
    ///
    /// 7.5.2 Class declaration
    ///
    ///     classDeclaration       = [ qualifierList ] CLASS className [ superClass ]
    ///                              "{" *classFeature "}" ";"
    ///
    /// 7.5.3 Association declaration
    ///
    ///     associationDeclaration = [ qualifierList ] ASSOCIATION associationName
    ///                              [ superAssociation ]
    ///                              "{" * classFeature "}" ";"
    ///
    /// 7.5.4 Enumeration declaration
    ///
    ///     enumerationDeclaration = enumTypeHeader enumName ":" enumTypeDeclaration ";"
    ///
    ///     enumTypeHeader         = [qualifierList] ENUMERATION
    ///
    /// 7.6.2 Complex type value
    ///
    ///     instanceValueDeclaration  = INSTANCE OF ( className / associationName )
    ///                                 [ alias ]
    ///                                 propertyValueList ";"
    ///
    ///     structureValueDeclaration = VALUE OF
    ///                                 ( className / associationName / structureName )
    ///                                 alias
    ///                                 propertyValueList ";"
    ///
    /// 7.4 Qualifiers
    ///
    ///     qualifierTypeDeclaration  = [ qualifierList ] QUALIFIER qualifierName ":"
    ///                                 qualifierType qualifierScope
    ///                                 [ qualifierPolicy ] ";"
    ///
    /// </remarks>
    public static MofProductionAst ParseMofProductionAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var peek = stream.Peek();

        // compilerDirective
        if (peek is PragmaToken)
        {
            return ParserEngine.ParseCompilerDirectiveAst(stream, quirks);
        }

        if (peek is IdentifierToken valueOrInstance)
        {
            if (valueOrInstance.IsKeyword(Constants.VALUE))
            {
                // structureValueDeclaration
                return ParserEngine.ParseStructureValueDeclarationAst(stream, quirks);
            }
            else if (valueOrInstance.IsKeyword(Constants.INSTANCE))
            {
                // instanceValueDeclaration
                return ParserEngine.ParseInstanceValueDeclarationAst(stream, quirks);
            }
        }

        // all other mofProduction elements start with [ qualifieList ]
        var qualifierList = (peek is AttributeOpenToken)
            ? ParserEngine.ParseQualifierListAst(stream, quirks)
            : new QualifierListAst();

        var productionType = stream.Peek<IdentifierToken>();
        if (productionType == null)
        {
            throw new UnexpectedTokenException(stream.Peek());
        }
        else if (productionType.IsKeyword(Constants.STRUCTURE))
        {
            // structureDeclaration
            return ParserEngine.ParseStructureDeclarationAst(stream, qualifierList, quirks);
        }
        else if (productionType.IsKeyword(Constants.CLASS))
        {
            // classDeclaration
            return ParserEngine.ParseClassDeclarationAst(stream, qualifierList, quirks);
        }
        else if (productionType.IsKeyword(Constants.ASSOCIATION))
        {
            // associationDeclaration
            return ParserEngine.ParseAssociationDeclarationAst(stream, qualifierList, quirks);
        }
        else if (productionType.IsKeyword(Constants.ENUMERATION))
        {
            // enumerationDeclaration
            return ParserEngine.ParseEnumerationDeclarationAst(stream, qualifierList, quirks);
        }
        else if (productionType.IsKeyword(Constants.QUALIFIER))
        {
            // qualifierTypeDeclaration
            return ParserEngine.ParseQualifierTypeDeclarationAst(stream, qualifierList, quirks);
        }
        else
        {
            throw new UnexpectedTokenException(productionType);
        }

    }

    #endregion

}
