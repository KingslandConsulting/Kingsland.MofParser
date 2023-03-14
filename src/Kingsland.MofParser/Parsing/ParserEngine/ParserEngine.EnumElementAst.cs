using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.4 Enumeration declaration

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="isIntegerEnum"></param>
    /// <param name="isStringEnum"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.4 Enumeration declaration
    ///
    ///     integerEnumElement = [ qualifierList ] enumLiteral "=" integerValue
    ///     stringEnumElement  = [ qualifierList ] enumLiteral [ "=" stringValue ]
    ///
    ///     enumLiteral        = IDENTIFIER
    ///
    /// </remarks>
    public static EnumElementAst ParseEnumElementAst(TokenStream stream, bool isIntegerEnum, bool isStringEnum, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new EnumElementAst.Builder();

        // [ qualifierList ]
        if (stream.TryPeek<AttributeOpenToken>())
        {
            node.QualifierList = ParserEngine.ParseQualifierListAst(stream, quirks);
        }

        // enumLiteral
        node.EnumElementName = stream.Read<IdentifierToken>();

        // "=" integerValue / [ "=" stringValue ]
        if (stream.TryRead<EqualsOperatorToken>(out var equals))
        {
            var enumValue = stream.Peek();
            switch (enumValue)
            {
                case IntegerLiteralToken:
                    // integerValue
                    if (isStringEnum)
                    {
                        throw new UnexpectedTokenException(enumValue);
                    }
                    node.EnumElementValue = ParserEngine.ParseIntegerValueAst(stream, quirks);
                    break;
                case StringLiteralToken:
                    // stringValue
                    if (isIntegerEnum)
                    {
                        throw new UnexpectedTokenException(enumValue);
                    }
                    node.EnumElementValue = ParserEngine.ParseStringValueAst(stream, quirks);
                    break;
                default:
                    throw new UnexpectedTokenException(enumValue);
            }
        }
        else if (isIntegerEnum)
        {
            // "=" is mandatory for integer enums
            throw new UnexpectedTokenException(stream.Peek());
        }

        return node.Build();

    }

    #endregion

}
