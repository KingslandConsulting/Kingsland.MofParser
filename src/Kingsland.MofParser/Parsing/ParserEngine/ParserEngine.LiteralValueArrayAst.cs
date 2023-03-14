using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.1 Primitive type value

    /// <summary>
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1 Primitive type value
    ///
    ///     literalValueArray = "{" [ literalValue *( "," literalValue ) ] "}"
    ///
    /// </remarks>
    public static LiteralValueArrayAst ParseLiteralValueArrayAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        var node = new LiteralValueArrayAst.Builder();
        // "{"
        stream.Read<BlockOpenToken>();
        // [ literalValue *( "," literalValue ) ]
        if (!stream.TryPeek<BlockCloseToken>())
        {
            // literalValue
            node.Values.Add(
                ParserEngine.ParseLiteralValueAst(stream, quirks)
            );
            // *( "," literalValue )
            while (stream.TryRead<CommaToken>(out var comma))
            {
                node.Values.Add(
                    ParserEngine.ParseLiteralValueAst(stream, quirks)
                );
            }
        }
        // "}"
        stream.Read<BlockCloseToken>();
        // return the result
        return node.Build();
    }

    #endregion

}
