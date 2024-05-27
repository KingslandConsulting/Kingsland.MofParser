using Kingsland.MofParser.Ast;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.6.4 Reference type value

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.4 Reference type value
    ///
    ///     objectPathValueArray = "{" [ objectPathValue *( "," objectPathValue ) ]
    ///
    /// </remarks>
    public static PrimitiveTypeValueAst ParseObjectPathValueArrayAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        throw new NotImplementedException();
    }

    #endregion

}
