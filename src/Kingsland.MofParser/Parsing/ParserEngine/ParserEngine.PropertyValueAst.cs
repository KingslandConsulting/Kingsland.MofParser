using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;
using Kingsland.ParseFx.Syntax;

// Resharper disable once CheckNamespace
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
    ///     propertyValue = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
    ///
    /// 7.6.1 Primitive type value
    ///
    ///     primitiveTypeValue = literalValue / literalValueArray
    ///     literalValueArray  = "{" [ literalValue *( "," literalValue ) ] "}"
    ///
    /// 7.5.9 Complex type value
    ///
    ///     complexTypeValue  = complexValue / complexValueArray
    ///     complexValueArray = "{" [ complexValue *( "," complexValue) ] "}"
    ///
    /// 7.6.4 Reference type value
    ///
    ///     referenceTypeValue   = objectPathValue / objectPathValueArray
    ///     objectPathValueArray = "{" [ objectPathValue *( "," objectPathValue ) ]
    ///                            "}"
    /// 7.6.3 Enum type value
    ///
    ///     enumTypeValue  = enumValue / enumValueArray
    ///     enumValueArray = "{" [ enumName *( "," enumName ) ] "}"
    ///     enumValue      = [ enumName "." ] enumLiteral
    ///     enumLiteral    = IDENTIFIER
    ///
    /// </remarks>
    internal static PropertyValueAst ParsePropertyValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        bool IsPrimitiveValueToken(SyntaxToken token)
        {
            return (token is IntegerLiteralToken) ||
                   (token is RealLiteralToken) ||
                   //(token is DateTimeLiteralToken) ||
                   (token is StringLiteralToken) ||
                   (token is BooleanLiteralToken) ||
                   //(token is OctetStringLiteralToken) ||
                   (token is NullLiteralToken);
        }

        bool IsComplexValueToken(SyntaxToken token)
        {
            return (token is AliasIdentifierToken) ||
                   ((token is IdentifierToken identifier) && (identifier.IsKeyword(Constants.VALUE)));
        }

        bool IsReferenceValueToken(SyntaxToken token)
        {
            // TODO: not implemented
            return false;
        }

        // if we've got an array we need to read the next item before we can determine the type
        var itemValue = stream.Peek();
        if (itemValue is BlockOpenToken)
        {
            stream.Read();
            itemValue = stream.Peek();
            stream.Backtrack();
            if (itemValue is BlockCloseToken)
            {
                // this is an empty array, so just pick a default type for now.
                // (we probably need a "public sealed class UnknownTypeValue : PropertyValueAst"
                // if we ever start doing type analysis on the ast).
                return ParserEngine.ParsePrimitiveTypeValueAst(stream, quirks);
            }
        }

        // primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
        if (IsPrimitiveValueToken(itemValue))
        {
            // primitiveTypeValue = literalValue / literalValueArray
            return ParserEngine.ParsePrimitiveTypeValueAst(stream, quirks);
        }
        else if (IsComplexValueToken(itemValue))
        {
            // complexTypeValue = complexValue / complexValueArray
            return ParserEngine.ParseComplexTypeValueAst(stream, quirks);
        }
        else if (IsReferenceValueToken(itemValue))
        {
            // referenceTypeValue = objectPathValue / objectPathValueArray
            return ParserEngine.ParseReferenceTypeValueAst(stream, quirks);
        }
        else
        {
            // enumTypeValue = enumValue / enumValueArray
            return ParserEngine.ParseEnumTypeValueAst(stream, quirks);
        }

    }

    #endregion

}
