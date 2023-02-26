using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.7 Parameter declaration

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.7 Parameter declaration
    ///
    ///     parameterDeclaration      = [ qualifierList ] ( primitiveParamDeclaration /
    ///                                 complexParamDeclaration /
    ///                                 enumParamDeclaration /
    ///                                 referenceParamDeclaration )
    ///
    ///     primitiveParamDeclaration = primitiveType parameterName [ array ]
    ///                                 [ "=" primitiveTypeValue ]
    ///
    ///     complexParamDeclaration   = structureOrClassName parameterName [ array ]
    ///                                 [ "=" ( complexTypeValue / aliasIdentifier ) ]
    ///
    ///     enumParamDeclaration      = enumName parameterName [ array ]
    ///                                 [ "=" enumValue ]
    ///
    ///     referenceParamDeclaration = classReference parameterName [ array ]
    ///                                 [ "=" referenceTypeValue ]
    ///
    ///     parameterName             = IDENTIFIER
    ///
    /// 7.5.6 Method declaration
    ///
    ///     classReference            = DT_REFERENCE
    ///
    /// 7.5.10 Reference type declaration
    ///
    ///     DT_REFERENCE              = className REF
    ///
    /// </remarks>
    public static ParameterDeclarationAst ParseParameterDeclarationAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
    {

        // [ qualifierList ]
        var qualifierList = stream.TryPeek<AttributeOpenToken>()
            ? ParserEngine.ParseQualifierListAst(stream, quirks)
            : new QualifierListAst();

        // read the type of the parameter
        //
        //     primitiveParamDeclaration => primitiveType
        //     complexParamDeclaration   => structureOrClassName
        //     enumParamDeclaration      => enumName
        //     referenceParamDeclaration => classReference
        //
        var parameterTypeName = stream.Read<IdentifierToken>();

        // if we're reading a referenceParamDeclaration then the next token
        // is the 'ref' keyword
        stream.TryReadIdentifierToken(Constants.REF, out var parameterRef);

        // parameterName
        var parameterName = stream.Read<IdentifierToken>();

        // [ array ]
        var parameterIsArray = false;
        if (stream.TryPeek<AttributeOpenToken>())
        {
            stream.Read<AttributeOpenToken>();
            stream.Read<AttributeCloseToken>();
            parameterIsArray = true;
        }

        // read the default value if there is one
        //
        //     primitiveParamDeclaration => [ "=" primitiveTypeValue ]
        //     complexParamDeclaration   => [ "=" ( complexTypeValue / aliasIdentifier ) ]
        //     enumParamDeclaration      => [ "=" enumValue ]
        //     referenceParamDeclaration => [ "=" referenceTypeValue ]
        //
        var parameterDefaultValue = default(PropertyValueAst);
        if (stream.TryRead<EqualsOperatorToken>(out var equals))
        {
            parameterDefaultValue = ParserEngine.ParsePropertyValueAst(stream, quirks);
        }

        return new ParameterDeclarationAst.Builder {
            QualifierList = qualifierList,
            ParameterType = parameterTypeName,
            ParameterRef = parameterRef,
            ParameterName = parameterName,
            ParameterIsArray = parameterIsArray,
            DefaultValue = parameterDefaultValue
        }.Build();

    }

    #endregion

}
