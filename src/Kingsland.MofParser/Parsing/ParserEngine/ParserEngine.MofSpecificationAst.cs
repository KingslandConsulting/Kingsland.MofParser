using Kingsland.MofParser.Ast;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
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
    ///     mofSpecification = *mofProduction
    ///
    /// </remarks>
    public static MofSpecificationAst ParseMofSpecificationAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        var node = new MofSpecificationAst.Builder();
        while (!stream.Eof)
        {
            node.Productions.Add(
                ParserEngine.ParseMofProductionAst(stream, quirks)
            );
        }
        return node.Build();
    }

    #endregion

}
