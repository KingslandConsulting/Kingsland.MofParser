using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.3 Enum type value

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.3 Enum type value
    ///
    ///     enumValueArray = "{" [ enumName *( "," enumName ) ] "}"
    ///
    /// </remarks>
    public static EnumValueArrayAst ParseEnumValueArrayAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new EnumValueArrayAst.Builder();

        // "{"
        var blockOpen = stream.Read<BlockOpenToken>();

        // see  https://github.com/mikeclayton/MofParser/issues/25
        var quirkEnabled = quirks.HasFlag(ParserQuirks.EnumValueArrayContainsEnumValuesNotEnumNames);
        if (quirkEnabled)
        {

            // [ enumValue *( "," enumValue ) ]
            if (!stream.TryPeek<BlockCloseToken>())
            {
                // enumValue
                node.Values.Add(
                    ParserEngine.ParseEnumValueAst(stream, quirks)
                );
                // *( "," enumValue )
                while (stream.TryRead<CommaToken>(out var comma))
                {
                    node.Values.Add(
                        ParserEngine.ParseEnumValueAst(stream, quirks)
                    );
                }
            }

        }
        else
        {

            // [ enumName *( "," enumName ) ]
            if (!stream.TryPeek<BlockCloseToken>())
            {
                // enumName
                node.Values.Add(
                    new EnumValueAst.Builder
                    {
                        EnumLiteral = stream.Read<IdentifierToken>()
                    }.Build()
                );
                // *( "," enumName )
                while (stream.TryRead<CommaToken>(out var comma))
                {
                    node.Values.Add(
                        new EnumValueAst.Builder
                        {
                            EnumLiteral = stream.Read<IdentifierToken>()
                        }.Build()
                   );
                }
            }

        }

        // "}"
        var blockClose = stream.Read<BlockCloseToken>();

        // return the result
        return node.Build();

    }

    #endregion

}
