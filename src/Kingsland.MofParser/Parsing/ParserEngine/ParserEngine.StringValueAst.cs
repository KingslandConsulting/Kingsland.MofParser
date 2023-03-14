using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.1.3 String values

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1.3 String values
    ///
    ///     singleStringValue = DOUBLEQUOTE *stringChar DOUBLEQUOTE
    ///
    ///     stringValue       = singleStringValue *( *WS singleStringValue )
    ///
    /// </remarks>
    public static StringValueAst ParseStringValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new StringValueAst.Builder();

        // singleStringValue
        node.StringLiteralValues.Add(
            stream.Read<StringLiteralToken>()
        );

        // *( *WS singleStringValue )
        while (stream.TryRead<StringLiteralToken>(out var stringLiteral))
        {
            node.StringLiteralValues.Add(stringLiteral!);
        }

        node.Value = string.Join(
            string.Empty,
            node.StringLiteralValues.Select(s => s.Value).ToList()
        );

        return node.Build();

    }

    #endregion

}
