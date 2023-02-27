using Kingsland.MofParser.Ast;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.6 Method declaration

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.6 Method declaration
    ///
    ///     methodDeclaration = [ qualifierList ]
    ///                         ( ( returnDataType [ array ] ) / VOID ) methodName
    ///                         "(" [ parameterList ] ")" ";"
    ///
    ///     returnDataType    = primitiveType /
    ///                         structureOrClassName /
    ///                         enumName /
    ///                         classReference
    ///
    ///     methodName        = IDENTIFIER
    ///     classReference    = DT_REFERENCE
    ///     DT_REFERENCE      = className REF
    ///     VOID              = "void" ; keyword: case insensitive
    ///     parameterList     = parameterDeclaration *( "," parameterDeclaration )
    ///
    /// 7.5.5 Property declaration
    ///
    ///    array             = "[" "]"
    ///
    /// </remarks>
    public static PropertyDeclarationAst ParseMethodDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
    {
        return (PropertyDeclarationAst)ParserEngine.ParseMemberDeclarationAst(stream, qualifierList, false, true, quirks);
    }

    #endregion

}
