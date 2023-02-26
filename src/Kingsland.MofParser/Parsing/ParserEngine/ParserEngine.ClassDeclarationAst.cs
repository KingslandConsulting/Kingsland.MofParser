using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.2 Class declaration

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
    /// 7.5.2 Class declaration
    ///
    ///     classDeclaration = [ qualifierList ] CLASS className [ superClass ]
    ///                        "{" *classFeature "}" ";"
    ///
    ///     className        = elementName
    ///
    ///     superClass       = ":" className
    ///
    ///     classFeature     = structureFeature /
    ///                        methodDeclaration
    ///
    ///     CLASS            = "class" ; keyword: case insensitive
    ///
    /// </remarks>
    public static ClassDeclarationAst ParseClassDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new ClassDeclarationAst.Builder();

        // [ qualifierList ]
        node.QualifierList = qualifierList;

        // CLASS
        var classKeyword = stream.ReadIdentifierToken(Constants.CLASS);

        // className
        node.ClassName = stream.ReadIdentifierToken(
            t => StringValidator.IsClassName(t.Name)
        );

        // [ superClass ]
        {
            // ":"
            if (stream.TryRead<ColonToken>(out var colon))
            {
                // className
                node.SuperClass = stream.ReadIdentifierToken(
                    t => StringValidator.IsClassName(t.Name)
                );
            }
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
