using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.4 Enumeration declaration

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="qualifierList"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.4 Enumeration declaration
    ///
    ///     enumerationDeclaration = enumTypeHeader enumName ":" enumTypeDeclaration ";"
    ///
    ///     enumTypeHeader         = [ qualifierList ] ENUMERATION
    ///
    ///     enumName               = elementName
    ///
    ///     enumTypeDeclaration    = ( DT_INTEGER / integerEnumName ) integerEnumDeclaration /
    ///                              ( DT_STRING / stringEnumName ) stringEnumDeclaration
    ///
    ///     integerEnumName        = enumName
    ///     stringEnumName         = enumName
    ///
    ///     integerEnumDeclaration = "{" [ integerEnumElement *( "," integerEnumElement ) ] "}"
    ///     stringEnumDeclaration  = "{" [ stringEnumElement *( "," stringEnumElement ) ] "}"
    ///
    ///     ENUMERATION            = "enumeration" ; keyword: case insensitive
    ///
    /// </remarks>
    public static EnumerationDeclarationAst ParseEnumerationDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
    {

        var node = new EnumerationDeclarationAst.Builder();

        var isIntegerEnum = false;
        var isStringEnum = false;

        // [ qualifierList ]
        // note - this has already been read for us and gets passed in as a parameter
        node.QualifierList = qualifierList;

        // ENUMERATION
        var enumeration = stream.ReadIdentifierToken(Constants.ENUMERATION);

        // enumName
        node.EnumName = stream.ReadIdentifierToken(
            t => StringValidator.IsEnumName(t.Name)
        );

        // ":"
        var colon = stream.Read<ColonToken>();

        // ( DT_INTEGER / integerEnumName ) / ( DT_STRING / stringEnumName )
        var enumTypeDeclaration = stream.Peek<IdentifierToken>();
        if (enumTypeDeclaration == null)
        {
            throw new UnexpectedTokenException(stream.Peek());
        }
        else if (enumTypeDeclaration.IsKeyword(Constants.DT_INTEGER))
        {
            isIntegerEnum = true;
        }
        else if (enumTypeDeclaration.IsKeyword(Constants.DT_STRING))
        {
            isStringEnum = true;
        }
        else
        {
            // check if we allow deprecated integer subtypes
            // see https://github.com/mikeclayton/MofParser/issues/52
            var quirksEnabled = quirks.HasFlag(ParserQuirks.AllowDeprecatedMof300IntegerTypesAsEnumerationDeclarationsBase);
            if (quirksEnabled &&
                enumTypeDeclaration.IsKeyword(new [] {
                    Constants.DT_UINT8, Constants.DT_UINT16, Constants.DT_UINT32, Constants.DT_UINT64,
                    Constants.DT_SINT8, Constants.DT_SINT16, Constants.DT_SINT32, Constants.DT_SINT64,
                })
            )
            {
                isIntegerEnum = true;
            }
            else
            {
                // this enumerationDeclaration is inheriting from a base enum.
                // as a result, we don't know whether this is an integer or
                // string enum until we inspect the type of the base enum
                if (!StringValidator.IsEnumName(enumTypeDeclaration.Name))
                {
                    throw new UnexpectedTokenException(enumTypeDeclaration);
                }
            }
        }
        node.EnumType = stream.Read<IdentifierToken>();

        // "{"
        stream.Read<BlockOpenToken>();

        // [ integerEnumElement *( "," integerEnumElement ) ]
        // [ stringEnumElement *( "," stringEnumElement ) ]
        if (!stream.TryPeek<BlockCloseToken>())
        {
            // integerEnumElement / stringEnumElement
            node.EnumElements.Add(
                ParserEngine.ParseEnumElementAst(stream, isIntegerEnum, isStringEnum, quirks)
            );
            // *( "," integerEnumElement ) / *( "," stringEnumElement )
            while (stream.TryRead<CommaToken>(out var comma))
            {
                node.EnumElements.Add(
                    ParserEngine.ParseEnumElementAst(stream, isIntegerEnum, isStringEnum, quirks)
                );
            }
        }

        // "}"
        var blockClose = stream.Read<BlockCloseToken>();

        // ";"
        var statementEnd = stream.Read<StatementEndToken>();

        return node.Build();

    }

    #endregion

}
