using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.4.1 QualifierList

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.4.1 QualifierList
    ///
    ///     qualifierValueInitializer = "(" literalValue ")"
    ///
    /// </remarks>
    public static QualifierValueInitializerAst ParseQualifierValueInitializer(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        var node = new QualifierValueInitializerAst.Builder();
        // "("
        var parenthesisOpen = stream.Read<ParenthesisOpenToken>();
        // literalValue
        node.Value = ParserEngine.ParseLiteralValueAst(stream, quirks);
        // ")"
        var parenthesisClose = stream.Read<ParenthesisCloseToken>();
        return node.Build();
    }

    #endregion

}
