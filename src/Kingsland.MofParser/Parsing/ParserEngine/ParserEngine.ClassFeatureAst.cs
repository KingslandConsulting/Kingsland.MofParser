using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.2 Class declaration

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.2 Class declaration
    ///
    ///     classFeature           = structureFeature / methodDeclaration
    ///
    /// 7.5.1 Structure declaration
    ///
    ///     structureDeclaration   = [ qualifierList ] STRUCTURE structureName
    ///                              [superStructure]
    ///                              "{" *structureFeature "}" ";"
    ///
    ///     structureFeature       = structureDeclaration /   ; local structure
    ///                              enumerationDeclaration / ; local enumeration
    ///                              propertyDeclaration
    ///
    /// 7.5.4 Enumeration declaration
    ///
    ///     enumerationDeclaration = enumTypeHeader enumName ":" enumTypeDeclaration ";"
    ///
    ///     enumTypeHeader         = [qualifierList] ENUMERATION
    ///
    /// </remarks>
    public static IClassFeatureAst ParseClassFeatureAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        // all classFeatures start with an optional "[ qualifierList ]"
        var qualifierList = stream.TryPeek<AttributeOpenToken>()
            ? ParserEngine.ParseQualifierListAst(stream, quirks)
            : new QualifierListAst();

        // we now need to work out if it's a structureDeclaration, enumerationDeclaration,
        // propertyDeclaration or methodDeclaration

        if (!stream.TryPeek<IdentifierToken>(out var identifier))
        {
            throw new UnexpectedTokenException(stream.Peek());
        }

        if (identifier.IsKeyword(Constants.STRUCTURE))
        {
            // structureDeclaration
            return ParserEngine.ParseStructureDeclarationAst(stream, qualifierList, quirks);
        }
        if (identifier.IsKeyword(Constants.ENUMERATION))
        {
            // enumerationDeclaration
            return ParserEngine.ParseEnumerationDeclarationAst(stream, qualifierList, quirks);
        }
        else
        {
            // propertyDeclaration or methodDeclaration
            return ParserEngine.ParseMemberDeclarationAst(stream, qualifierList, true, true, quirks);
        }

    }

    #endregion

}
