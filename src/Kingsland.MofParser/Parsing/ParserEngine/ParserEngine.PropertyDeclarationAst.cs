using Kingsland.MofParser.Ast;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.5 Property declaration

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
    /// 7.5.5 Property declaration
    ///
    ///     propertyDeclaration          = [ qualifierList ] ( primitivePropertyDeclaration /
    ///                                    complexPropertyDeclaration /
    ///                                    enumPropertyDeclaration /
    ///                                    referencePropertyDeclaration) ";"
    ///
    /// </remarks>
    public static PropertyDeclarationAst ParsePropertyDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
    {
        return (PropertyDeclarationAst)ParserEngine.ParseMemberDeclarationAst(
            stream, qualifierList, true, false, quirks
        );
    }

    #endregion

}
