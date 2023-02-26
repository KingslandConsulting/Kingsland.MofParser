using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

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
    ///     ; Note: objectPathValues are URLs and shall conform to RFC 3986 (Uniform
    ///     ; Resource Identifiers(URI): Generic Syntax) and to the following ABNF.
    ///
    ///     objectPathValue  = [namespacePath ":"] instanceId
    ///
    ///     namespacePath    = [serverPath] namespaceName
    ///
    ///     ; Note: The production rules for host and port are defined in IETF
    ///     ; RFC 3986 (Uniform Resource Identifiers (URI): Generic Syntax).
    ///
    ///     serverPath       = (host / LOCALHOST) [ ":" port] "/"
    ///     LOCALHOST        = "localhost" ; Case insensitive
    ///     instanceId       = className "." instanceKeyValue
    ///     instanceKeyValue = keyValue *( "," keyValue )
    ///     keyValue         = propertyName "=" literalValue
    ///
    /// </remarks>
    public static PrimitiveTypeValueAst ParseObjectPathValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {
        throw new NotImplementedException();
    }

    #endregion

}
