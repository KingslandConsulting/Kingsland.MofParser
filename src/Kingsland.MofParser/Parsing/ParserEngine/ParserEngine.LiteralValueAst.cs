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
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1 Primitive type value
    ///
    ///     literalValue = integerValue /
    ///                    realValue /
    ///                    booleanValue /
    ///                    nullValue /
    ///                    stringValue
    ///                      ; NOTE stringValue covers octetStringValue and
    ///                      ; dateTimeValue
    ///
    /// </remarks>
    public static LiteralValueAst ParseLiteralValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        var peek = stream.Peek();
        return peek switch
        {
            IntegerLiteralToken =>
                // integerValue
                ParserEngine.ParseIntegerValueAst(stream, quirks),
            RealLiteralToken =>
                // realValue
                ParserEngine.ParseRealValueAst(stream, quirks),
            BooleanLiteralToken =>
                // booleanValue
                ParserEngine.ParseBooleanValueAst(stream, quirks),
            NullLiteralToken =>
                // nullValue
                ParserEngine.ParseNullValueAst(stream, quirks),
            StringLiteralToken =>
                // stringValue
                ParserEngine.ParseStringValueAst(stream, quirks),
            _ =>
                throw new UnexpectedTokenException(peek)
        };
    }

    #endregion

}
