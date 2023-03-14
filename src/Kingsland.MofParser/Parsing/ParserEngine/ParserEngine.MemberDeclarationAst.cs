using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.Parsing;

internal static partial class ParserEngine
{

    #region 7.5.5 Property declaration

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="qualifierList"></param>
    /// <param name="allowPropertyDeclaration"></param>
    /// <param name="allowMethodDeclaration"></param>
    /// <param name="quirks"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.5 Property declaration
    ///
    ///     propertyDeclaration          = [ qualifierList ] ( primitivePropertyDeclaration /
    ///                                    complexPropertyDeclaration /
    ///                                    enumPropertyDeclaration /
    ///                                    referencePropertyDeclaration) ";"
    ///
    ///     primitivePropertyDeclaration = primitiveType propertyName [ array ]
    ///                                    [ "=" primitiveTypeValue ]
    ///
    ///     complexPropertyDeclaration   = structureOrClassName propertyName [ array ]
    ///                                    [ "=" ( complexTypeValue / aliasIdentifier ) ]
    ///
    ///     enumPropertyDeclaration      = enumName propertyName [ array ]
    ///                                    [ "=" enumTypeValue ]
    ///
    ///     referencePropertyDeclaration = classReference propertyName [ array ]
    ///                                    [ "=" referenceTypeValue ]
    ///
    ///     array                        = "[" "]"
    ///     propertyName                 = IDENTIFIER
    ///     structureOrClassName         = IDENTIFIER
    ///
    ///     classReference               = DT_REFERENCE
    ///     DT_REFERENCE                 = className REF
    ///     REF                          = "ref" ; keyword: case insensitive
    ///
    /// 7.5.6 Method declaration
    ///
    ///     methodDeclaration = [ qualifierList ]
    ///                         ( ( returnDataType [ array ] ) / VOID ) methodName
    ///                         "(" [ parameterList ] ")" ";"
    ///
    ///     returnDataType    = primitiveType /
    ///                         structureOrClassName /
    ///                         enumName /
    ///                         classReference
    ///
    ///     methodName        = IDENTIFIER
    ///     classReference    = DT_REFERENCE
    ///     DT_REFERENCE      = className REF
    ///     VOID              = "void" ; keyword: case insensitive
    ///     parameterList     = parameterDeclaration *( "," parameterDeclaration )
    ///
    /// </remarks>
    public static IClassFeatureAst ParseMemberDeclarationAst(
        TokenStream stream, QualifierListAst qualifierList,
        bool allowPropertyDeclaration, bool allowMethodDeclaration,
        ParserQuirks quirks
    )
    {

        var isMethodDeclaration = false;
        var isPropertyDeclaration = false;

        // [ qualifierList ]
        // note - this has already been read for us and gets passed in as a parameter

        // read the return type of the propertyDeclaration or methodDeclaration
        //
        //     primitivePropertyDeclaration => primitiveType
        //     complexPropertyDeclaration   => structureOrClassName
        //     enumPropertyDeclaration      => enumName
        //     referencePropertyDeclaration => classReference
        //
        //     methodDeclaration            => returnDataType => primitiveType /
        //                                                       structureOrClassName /
        //                                                       enumName /
        //                                                       classReference
        //
        var memberReturnType = stream.Read<IdentifierToken>();
        var memberReturnTypeIsArray = false;

        // if we're reading a:
        //     + referencePropertyDeclaration or a
        //     + methodDeclaration that returns a classReference type
        // then the next token is the REF keyword in the classReference
        stream.TryReadIdentifierToken(Constants.REF, out var memberReturnTypeRef);

        // if we're reading a methodDeclaration then the next token
        // in the methodDeclaration after returnDataType could be [ array ]
        if (stream.TryPeek<AttributeOpenToken>())
        {
            // check we're expecting a methodDeclaration
            if (isPropertyDeclaration || !allowMethodDeclaration)
            {
                throw new UnsupportedTokenException(stream.Peek());
            }
            // [ array ]
            stream.Read<AttributeOpenToken>();
            stream.Read<AttributeCloseToken>();
            memberReturnTypeIsArray = true;
            // we know this is a methodDeclaration now
            isMethodDeclaration = true;
        }

        // propertyName / methodName
        var memberName = stream.Read<IdentifierToken>();

        // if we're reading a propertyDeclaration then the next token
        // after the propertyName could be [ array ]
        if (stream.TryPeek<AttributeOpenToken>())
        {
            // check we're expecting a propertyDeclaration
            if (isMethodDeclaration || !allowPropertyDeclaration)
            {
                throw new UnsupportedTokenException(stream.Peek());
            }
            // [ array ]
            stream.Read<AttributeOpenToken>();
            stream.Read<AttributeCloseToken>();
            memberReturnTypeIsArray = true;
            // we know this is a propertyDeclaration now
            isPropertyDeclaration = true;
        }

        // if we're reading a methodDeclaration, then the next tokens *must*
        // be "(" [ parameterList ] ")"
        var methodParameterDeclarations = new List<ParameterDeclarationAst>();
        if (isMethodDeclaration || stream.TryPeek<ParenthesisOpenToken>())
        {
            // check we're expecting a methodDeclaration
            if (isPropertyDeclaration || !allowMethodDeclaration)
            {
                throw new UnsupportedTokenException(stream.Peek());
            }
            // "("
            var methodParenthesisOpen = stream.Read<ParenthesisOpenToken>();
            //  [ parameterDeclaration *( "," parameterDeclaration ) ]
            if (!stream.TryPeek<ParenthesisCloseToken>())
            {
                // parameterDeclaration
                methodParameterDeclarations.Add(
                    ParserEngine.ParseParameterDeclarationAst(stream, quirks)
                );
                // *( "," parameterDeclaration )
                while (stream.TryRead<CommaToken>(out var comma))
                {
                    methodParameterDeclarations.Add(
                        ParserEngine.ParseParameterDeclarationAst(stream, quirks)
                    );
                }
            }
            // ")"
            var methodParenthesisClose = stream.Read<ParenthesisCloseToken>();
            // we know this is a methodDeclaration now
            isMethodDeclaration = true;
        }
        else
        {
            // check we're expecting a propertyDeclaration
            if (isMethodDeclaration || !allowPropertyDeclaration)
            {
                throw new UnsupportedTokenException(stream.Peek());
            }
            // we know this is a propertyDeclaration now
            isPropertyDeclaration = true;
        }

        // if we're reading a propertyDeclaration, then there *could* be
        // be a property initializer:
        //
        //     primitivePropertyDeclaration => [ "=" primitiveTypeValue ]
        //     complexPropertyDeclaration   => [ "=" ( complexTypeValue / aliasIdentifier ) ]
        //     enumPropertyDeclaration      => [ "=" enumValue ]
        //     referencePropertyDeclaration => [ "=" referenceTypeValue ]
        //
        var propertyInitializer = default(PropertyValueAst);
        if (isPropertyDeclaration)
        {
            if (stream.TryPeek<EqualsOperatorToken>())
            {
                // check we're expecting a propertyDeclaration
                if (isMethodDeclaration || !allowPropertyDeclaration)
                {
                    throw new UnsupportedTokenException(stream.Peek());
                }
                // "="
                var equalsOperator = stream.Read<EqualsOperatorToken>();
                propertyInitializer = ParserEngine.ParsePropertyValueAst(stream, quirks);
            }
        }

        // ";"
        stream.Read<StatementEndToken>();

        if (isPropertyDeclaration)
        {
            // check we're expecting a propertyDeclaration
            if (isMethodDeclaration || !allowPropertyDeclaration)
            {
                throw new InvalidOperationException();
            }
            var node = new PropertyDeclarationAst.Builder
            {
                QualifierList = qualifierList,
                ReturnType = memberReturnType,
                ReturnTypeRef = memberReturnTypeRef,
                PropertyName = memberName,
                ReturnTypeIsArray = memberReturnTypeIsArray,
                Initializer = propertyInitializer
            };
            return node.Build();
        }
        else if (isMethodDeclaration)
        {
            // check we're expecting a methodDeclaration
            if (isPropertyDeclaration || !allowMethodDeclaration)
            {
                throw new InvalidOperationException();
            }
            var node = new MethodDeclarationAst.Builder
            {
                QualifierList = qualifierList,
                ReturnType = memberReturnType,
                ReturnTypeRef = memberReturnTypeRef,
                ReturnTypeIsArray = memberReturnTypeIsArray,
                MethodName = memberName,
                Parameters = methodParameterDeclarations
            };
            return node.Build();
        }
        else
        {
            // we couldn't work out whether this was a propertyDeclaration or a methodDeclaration
            throw new InvalidOperationException();
        }

    }

    #endregion

}
