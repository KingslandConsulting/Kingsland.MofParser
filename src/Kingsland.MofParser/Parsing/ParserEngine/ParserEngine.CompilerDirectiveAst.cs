using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.3 Compiler directives

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.3 Compiler directives
    ///
    ///     compilerDirective = PRAGMA ( pragmaName / standardPragmaName )
    ///                         "(" pragmaParameter ")"
    ///
    ///     pragmaName         = directiveName
    ///
    ///     standardPragmaName = INCLUDE
    ///
    ///     pragmaParameter    = stringValue ; if the pragma is INCLUDE,
    ///                                      ; the parameter value
    ///                                      ; shall represent a relative
    ///                                      ; or full file path
    ///
    ///     PRAGMA             = "#pragma"   ; keyword: case insensitive
    ///
    ///     INCLUDE            = "include"   ; keyword: case insensitive
    ///
    ///     directiveName      = org-id "_" IDENTIFIER
    ///
    /// </remarks>
    public static CompilerDirectiveAst ParseCompilerDirectiveAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new CompilerDirectiveAst.Builder();

        // PRAGMA
        node.PragmaKeyword = stream.Read<PragmaToken>();

        // ( pragmaName / standardPragmaName )
        node.PragmaName = stream.Read<IdentifierToken>();

        // "("
        var parenthesisOpen = stream.Read<ParenthesisOpenToken>();

        // pragmaParameter
        node.PragmaParameter = ParserEngine.ParseStringValueAst(stream, quirks);

        // ")"
        var parenthesisClose = stream.Read<ParenthesisCloseToken>();

        return node.Build();

    }

    #endregion

}
