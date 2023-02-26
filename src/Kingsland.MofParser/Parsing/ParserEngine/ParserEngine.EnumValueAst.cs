using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

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
    ///     enumValue   = [ enumName "." ] enumLiteral
    ///
    ///     enumLiteral = IDENTIFIER
    ///
    /// 7.5.4 Enumeration declaration
    ///
    ///     enumName    = elementName
    ///
    /// 7.7.1 Names
    ///
    ///     elementName         = localName / schemaQualifiedName
    ///     localName           = IDENTIFIER
    ///     IDENTIFIER          = firstIdentifierChar* (nextIdentifierChar )
    ///     firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE
    ///     nextIdentifierChar  = firstIdentifierChar / decimalDigit
    ///
    /// 7.7.2 Schema-qualified name
    ///
    ///     schemaQualifiedName = schemaName UNDERSCORE IDENTIFIER
    ///     schemaName          = firstSchemaChar *( nextSchemaChar )
    ///     firstSchemaChar     = UPPERALPHA / LOWERALPHA
    ///     nextSchemaChar      = firstSchemaChar / decimalDigit
    ///
    /// </remarks>
    public static EnumValueAst ParseEnumValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new EnumValueAst.Builder();

        // read the first token and try to determine whether we have a
        //
        //     [ enumName "." ] enumLiteral
        //
        // or just a plain old
        //
        //     enumLiteral

        // [ enumName "." ] / enumLiteral
        var enumIdentifier = stream.Read<IdentifierToken>();

        // look at the next token to see if it's a "."
        if (stream.TryRead<DotOperatorToken>(out var _))
        {
            // [ enumName "." ]
            node.EnumName = enumIdentifier;
            // enumLiteral
            node.EnumLiteral = stream.Read<IdentifierToken>();
        }
        else
        {
            // enumLiteral
            node.EnumLiteral = enumIdentifier;
        }

        return node.Build();

    }

    #endregion

}
