using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.9 Complex type value

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.9 Complex type value
    ///
    ///     propertySlot      = propertyName "=" propertyValue ";"
    ///
    ///     propertyName      = IDENTIFIER
    ///
    /// </remarks>
    internal static PropertySlotAst ParsePropertySlotAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new PropertySlotAst.Builder();

        // propertyName
        node.PropertyName = stream.ReadIdentifierToken(
            t => StringValidator.IsIdentifier(t.Name)
        );

        // "="
        stream.Read<EqualsOperatorToken>();

        // propertyValue
        node.PropertyValue = ParserEngine.ParsePropertyValueAst(stream, quirks);

        // ";"
        stream.Read<StatementEndToken>();

        return node.Build();

    }

    #endregion

}
