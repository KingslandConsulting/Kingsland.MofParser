using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Parsing
{

    internal sealed class ParserEngine
    {

        #region 7.2 MOF specification

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.2 MOF specification
        ///
        ///     mofSpecification = *mofProduction
        ///
        /// </remarks>
        public static MofSpecificationAst ParseMofSpecificationAst(ParserStream stream)
        {
            var node = new MofSpecificationAst.Builder();
            while (!stream.Eof)
            {
                var production = ParserEngine.ParseMofProductionAst(stream);
                node.Productions.Add(production);
            }
            return node.Build();
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.2 MOF specification
        ///
        ///     mofProduction = compilerDirective /
        ///                     structureDeclaration /
        ///                     classDeclaration /
        ///                     associationDeclaration /
        ///                     enumerationDeclaration /
        ///                     instanceValueDeclaration /
        ///                     structureValueDeclaration /
        ///                     qualifierTypeDeclaration
        ///
        /// </remarks>
        public static MofProductionAst ParseMofProductionAst(ParserStream stream)
        {

            var peek = stream.Peek();

            // compilerDirective
            if (peek is PragmaToken)
            {
                return ParserEngine.ParseCompilerDirectiveAst(stream);
            }

            var identifier = stream.Peek<IdentifierToken>();
            switch (identifier.GetNormalizedName())
            {
                case Constants.INSTANCE:
                    // instanceValueDeclaration
                    return ParserEngine.ParseInstanceValueDeclarationAst(stream);
                case Constants.VALUE:
                    // structureValueDeclaration
                    return ParserEngine.ParseStructureValueDeclarationAst(stream);
            }

            // all other mofProduction structures can start with an optional qualifierList
            var qualifiers = default(QualifierListAst);
            if (peek is AttributeOpenToken)
            {
                qualifiers = ParserEngine.ParseQualifierListAst(stream);
            }

            identifier = stream.Peek<IdentifierToken>();
            switch (identifier.GetNormalizedName())
            {
                case Constants.STRUCTURE:
                    // structureDeclaration
                    throw new UnsupportedTokenException(identifier);
                case Constants.CLASS:
                    // classDeclaration
                    return ParserEngine.ParseClassDeclarationAst(stream, qualifiers);
                case Constants.ASSOCIATION:
                    // associationDeclaration
                    throw new UnsupportedTokenException(identifier);
                case Constants.ENUMERATION:
                    // enumerationDeclaration
                    throw new UnsupportedTokenException(identifier);
                case Constants.QUALIFIER:
                    // qualifierTypeDeclaration
                    throw new UnsupportedTokenException(identifier);
                default:
                    throw new UnexpectedTokenException(peek);
            }

        }

        #endregion

        #region 7.3 Compiler directives

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.3 Compiler directives
        ///
        /// Compiler directives direct the processing of MOF files. Compiler directives do not create, modify, or
        /// annotate the language elements.
        ///
        /// Compiler directives shall conform to the format defined by ABNF rule compilerDirective (whitespace
        /// as defined in 5.2 is allowed between the elements of the rules in this ABNF section):
        ///
        ///     compilerDirective = PRAGMA ( pragmaName / standardPragmaName )
        ///                         "(" pragmaParameter ")"
        ///
        ///     pragmaName         = directiveName
        ///     standardPragmaName = INCLUDE
        ///     pragmaParameter    = stringValue ; if the pragma is INCLUDE,
        ///                                      ; the parameter value
        ///                                      ; shall represent a relative
        ///                                      ; or full file path
        ///     PRAGMA             = "#pragma"  ; keyword: case insensitive
        ///     INCLUDE            = "include"  ; keyword: case insensitive
        ///
        ///     directiveName      = org-id "_" IDENTIFIER
        ///
        /// </remarks>
        public static CompilerDirectiveAst ParseCompilerDirectiveAst(ParserStream stream)
        {

            var node = new CompilerDirectiveAst.Builder();

            // PRAGMA
            stream.Read<PragmaToken>();

            // ( pragmaName / standardPragmaName )
            node.PragmaName = stream.Read<IdentifierToken>().Name;

            // "("
            stream.Read<ParenthesesOpenToken>();

            // stringValue
            node.Argument = stream.Read<StringLiteralToken>().Value;

            // ")"
            stream.Read<ParenthesesCloseToken>();

            return node.Build();

        }

        #endregion

        #region 7.4 Qualifiers

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.4 Qualifiers
        ///
        /// </returns>
        public static QualifierTypeDeclarationAst ParseQualifierDeclarationAst(ParserStream stream)
        {

            var node = new QualifierTypeDeclarationAst.Builder();

            node.Name = stream.Read<IdentifierToken>();

            if (stream.Peek<ParenthesesOpenToken>() != null)
            {
                stream.Read<ParenthesesOpenToken>();
                node.Initializer = ParserEngine.ParseLiteralValueAst(stream);
                stream.Read<ParenthesesCloseToken>();
            }
            else if (stream.Peek<BlockOpenToken>() != null)
            {
                node.Initializer = ParserEngine.ParseLiteralValueArrayAst(stream);
            }

            if (stream.Peek<ColonToken>() != null)
            {
                stream.Read<ColonToken>();
                while (stream.Peek<IdentifierToken>() != null)
                {
                    node.Flavors.Add(stream.Read<IdentifierToken>().Name);
                }
            }

            return node.Build();

        }

        #endregion

        #region 7.4.1 QualifierList

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.4.1 QualifierList
        ///
        ///     qualifierList                 = "[" qualifierValue *( "," qualifierValue ) "]"
        ///
        ///     qualifierValue                = qualifierName [ qualifierValueInitializer /
        ///                                     qualiferValueArrayInitializer ]
        ///
        ///     qualifierValueInitializer     = "(" literalValue ")"
        ///
        ///     qualiferValueArrayInitializer = "{" literalValue *( "," literalValue ) "}"
        ///
        /// </remarks>
        public static QualifierListAst ParseQualifierListAst(ParserStream stream)
        {

            var node = new QualifierListAst.Builder();

            // "["
            stream.Read<AttributeOpenToken>();

            // qualifierValue *( "," qualifierValue )
            while (!stream.Eof)
            {
                node.Qualifiers.Add(ParserEngine.ParseQualifierDeclarationAst(stream));
                if (stream.Peek<CommaToken>() == null)
                {
                    break;
                }
                stream.Read<CommaToken>();
            }

            // "]"
            stream.Read<AttributeCloseToken>();

            return node.Build();

        }

        #endregion

        #region 7.5.2 Class declaration

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.2 Class declaration
        ///
        ///     classDeclaration = [ qualifierList ] CLASS className [ superClass ]
        ///                        "{" *classFeature "}" ";"
        ///
        ///     className        = elementName
        ///     superClass       = ":" className
        ///     classFeature     = structureFeature /
        ///                        methodDeclaration
        ///     CLASS            = "class" ; keyword: case insensitive
        ///
        /// </remarks>
        public static ClassDeclarationAst ParseClassDeclarationAst(ParserStream stream, QualifierListAst qualifiers)
        {

            var node = new ClassDeclarationAst.Builder();

            // [ qualifierList ]
            node.Qualifiers = qualifiers;

            // CLASS
            stream.ReadIdentifier(Constants.CLASS);

            // className
            var className = stream.Read<IdentifierToken>();
            if (!StringValidator.IsClassName(className.Name))
            {
                throw new InvalidOperationException("Identifer is not a valid class name.");
            }
            node.ClassName = className;

            // [ superClass ]
            if (stream.Peek<ColonToken>() != null)
            {
                stream.Read<ColonToken>();
                var superclass = stream.Read<IdentifierToken>();
                if (!StringValidator.IsClassName(className.Name))
                {
                    throw new InvalidOperationException("Identifer is not a valid superclass name.");
                }
                node.Superclass = superclass;
            }

            // "{"
            stream.Read<BlockOpenToken>();

            // *classFeature
            while (!stream.Eof)
            {
                if (stream.Peek() is BlockCloseToken)
                {
                    break;
                }
                var classFeature = ParserEngine.ParseClassFeatureAst(stream);
                node.Features.Add(classFeature);
            }

            // "}" ";"
            stream.Read<BlockCloseToken>();
            stream.Read<StatementEndToken>();

            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.2 Class declaration
        ///
        ///     classFeature     = structureFeature / methodDeclaration
        ///
        ///     structureFeature = structureDeclaration /   ; local structure
        ///                        enumerationDeclaration / ; local enumeration
        ///                        propertyDeclaration
        ///
        /// 7.5.1 Structure declaration
        ///
        ///     structureDeclaration   = [ qualifierList ] STRUCTURE structureName
        ///                              [superStructure]
        ///                              "{" *structureFeature "}" ";"
        ///
        /// 7.5.4 Enumeration declaration
        ///
        ///     enumerationDeclaration = enumTypeHeader enumName ":" enumTypeDeclaration ";"
        ///     enumTypeHeader         = [qualifierList] ENUMERATION
        ///
        /// 7.5.5 Property declaration
        ///
        ///     propertyDeclaration    = [ qualifierList ] ( primitivePropertyDeclaration /
        ///                              complexPropertyDeclaration /
        ///                              enumPropertyDeclaration /
        ///                              referencePropertyDeclaration ) ";"
        ///
        /// 7.5.6 Method declaration
        ///
        ///     methodDeclaration      = [ qualifierList ]
        ///                              ((returnDataType[array] ) / VOID ) methodName
        ///                              "(" [parameterList] ")" ";"
        ///
        /// </remarks>
        public static ClassFeatureAst ParseClassFeatureAst(ParserStream stream)
        {

            // all classFeatures start with an optional "[ qualifierList ]"
            var qualifierList = default(QualifierListAst);
            var peek = stream.Peek() as AttributeOpenToken;
            if ((peek as AttributeOpenToken) != null)
            {
                qualifierList = ParserEngine.ParseQualifierListAst(stream);
            }

            // we now need to work out if it's a structureDeclaration, enumerationDeclaration,
            // propertyDeclaration or methodDeclaration
            var identifier = stream.Peek<IdentifierToken>();
            if (identifier == null)
            {
                throw new UnexpectedTokenException(peek);
            }
            var identifierName = identifier.GetNormalizedName();
            if (identifierName == Constants.STRUCTURE)
            {
                // structureDeclaration
                throw new UnsupportedTokenException(identifier);
            }
            else if (identifierName == Constants.ENUMERATION)
            {
                // enumDeclaration
                throw new UnsupportedTokenException(identifier);
            }
            else
            {
                // propertyDeclaration or methodDeclaration
                return ParserEngine.ParseMemberDeclaration(stream, qualifierList);
            }

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.5 Property declaration
        ///
        ///     propertyDeclaration = [ qualifierList ] ( primitivePropertyDeclaration /
        ///                                               complexPropertyDeclaration /
        ///                                               enumPropertyDeclaration /
        ///                                               referencePropertyDeclaration) ";"
        ///
        ///     primitivePropertyDeclaration = primitiveType propertyName [ array ]
        ///                                    [ "=" primitiveTypeValue]
        ///
        ///     complexPropertyDeclaration   = structureOrClassName propertyName [ array ]
        ///                                    [ "=" ( complexTypeValue / aliasIdentifier ) ]
        ///
        ///     enumPropertyDeclaration      = enumName propertyName [ array ]
        ///                                    [ "=" enumTypeValue]
        ///
        ///     referencePropertyDeclaration = classReference propertyName [ array ]
        ///                                    [ "=" referenceTypeValue ]
        ///
        ///     array                        = "[" "]"
        ///     propertyName                 = IDENTIFIER
        ///     structureOrClassName         = IDENTIFIER
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
        ///     array             = "[" "]"
        ///     methodName        = IDENTIFIER
        ///     classReference    = DT_REFERENCE
        ///     DT_REFERENCE      = className REF
        ///     REF               = "ref" ; keyword: case insensitive
        ///     VOID              = "void" ; keyword: case insensitive
        ///     parameterList     = parameterDeclaration *( "," parameterDeclaration )
        ///
        public static ClassFeatureAst ParseMemberDeclaration(ParserStream stream, QualifierListAst qualifiers)
        {

            // primitiveType / structureOrClassName / enumName / classReference
            var returnType = stream.Read<IdentifierToken>();

            var @ref = default(IdentifierToken);
            if (stream.PeekIdentifier(Constants.REF))
            {
                @ref = stream.ReadIdentifier(Constants.REF);
            }

            // [ array ]
            var returnTypeIsArray = false;
            if (stream.Peek<AttributeOpenToken>() != null)
            {
                stream.Read<AttributeOpenToken>();
                stream.Read<AttributeCloseToken>();
                returnTypeIsArray = true;
            }

            // propertyName / methodName
            var memberName = stream.Read<IdentifierToken>();

            if ((stream.Peek<ParenthesesOpenToken>() != null) && (@ref == null))
            {
                // read the remainder of a methodDeclaration
                var node = new MethodDeclarationAst.Builder()
                {
                    Qualifiers = qualifiers,
                    Name = memberName,
                    ReturnType = returnType,
                    ReturnTypeIsArray = returnTypeIsArray
                };
                // "("
                stream.Read<ParenthesesOpenToken>();
                //  [ parameterList ]
                if (stream.Peek<ParenthesesCloseToken>() == null)
                {
                    while (!stream.Eof)
                    {
                        if (node.Parameters.Count > 0)
                        {
                            stream.Read<CommaToken>();
                        }
                        var parameter = ParserEngine.ParseParameterDeclarationAst(stream);
                        node.Parameters.Add(parameter);
                        if (stream.Peek<ParenthesesCloseToken>() != null)
                        {
                            break;
                        }
                    }
                }
                // ")" ";"
                stream.Read<ParenthesesCloseToken>();
                stream.Read<StatementEndToken>();
                return node.Build();
            }
            else
            {
                // read the remainder of a propertyDeclaration
                var node = new PropertyDeclarationAst.Builder()
                {
                    Qualifiers = qualifiers,
                    ReturnType = returnType,
                    PropertyName = memberName,
                    IsRef = (@ref != null)
                };
                if (stream.Peek<AttributeOpenToken>() != null)
                {
                    stream.Read<AttributeOpenToken>();
                    stream.Read<AttributeCloseToken>();
                    node.IsArray = true;
                }
                if (stream.Peek<EqualsOperatorToken>() != null)
                {
                    stream.Read<EqualsOperatorToken>();
                    node.Initializer = ParserEngine.ReadClassFeatureAstDefaultValue(stream, returnType);
                }
                stream.Read<StatementEndToken>();
                return node.Build();

            }

        }

        public static PrimitiveTypeValueAst ReadClassFeatureAstDefaultValue(ParserStream stream, IdentifierToken returnType)
        {
            switch (returnType.GetNormalizedName())
            {
                case Constants.DT_INTEGER:
                case Constants.DT_REAL32:
                case Constants.DT_REAL64:
                case Constants.DT_STRING:
                case Constants.DT_DATETIME:
                case Constants.DT_BOOLEAN:
                case Constants.DT_OCTECTSTRING:
                    // primitiveType
                    return ParserEngine.ParsePrimitiveTypeValueAst(stream);
                default:
                    /// structureOrClassName
                    /// enumName
                    /// classReference
                    var peek = stream.Peek();
                    if (peek is NullLiteralToken)
                    {
                        return ParserEngine.ParseNullValueAst(stream);
                    }
                    throw new UnsupportedTokenException(stream.Peek());
            }
        }

        #endregion

        #region 7.5.7 Parameter declaration

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
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
        ///                                 [ "=" primitiveTypeValue
        ///                                 ]
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
        /// </remarks>
        public static ParameterDeclarationAst ParseParameterDeclarationAst(ParserStream stream)
        {
            var node = new ParameterDeclarationAst.Builder();
            var qualifiers = default(QualifierListAst);
            if (stream.Peek<AttributeOpenToken>() != null)
            {
                qualifiers = ParserEngine.ParseQualifierListAst(stream);
            }
            node.Qualifiers = qualifiers;
            node.Type = stream.Read<IdentifierToken>();
            if (stream.PeekIdentifier(Constants.REF))
            {
                stream.ReadIdentifier(Constants.REF);
                node.IsRef = true;
            }
            else
            {
                node.IsRef = false;
            }
            node.Name = stream.Read<IdentifierToken>();
            if (stream.Peek<AttributeOpenToken>() != null)
            {
                stream.Read<AttributeOpenToken>();
                stream.Read<AttributeCloseToken>();
                node.IsArray = true;
            }
            if (stream.Peek<EqualsOperatorToken>() != null)
            {
                stream.Read<EqualsOperatorToken>();
                node.DefaultValue = ParserEngine.ReadClassFeatureAstDefaultValue(stream, node.Type);
            }
            return node.Build();
        }

        #endregion

        #region 7.5.9 Complex type value

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.9 Complex type value
        ///
        ///     complexTypeValue = complexValue / complexValueArray
        ///
        /// </remarks>
        public static ComplexTypeValueAst ParseComplexTypeValueAst(ParserStream stream)
        {
            var peek = stream.Peek();
            if (peek is IdentifierToken)
            {
                // complexValue
                return ParserEngine.ParseComplexValueAst(stream);
            }
            else if (peek is BlockOpenToken)
            {
                // complexValueArray
                return ParserEngine.ParseComplexValueArrayAst(stream);
            }
            else
            {
                throw new UnexpectedTokenException(peek);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.9 Complex type value
        ///
        ///     complexValueArray = "{" [ complexValue *( "," complexValue) ] "}"
        ///
        /// </remarks>
        public static ComplexValueArrayAst ParseComplexValueArrayAst(ParserStream stream)
        {
            // complexValueArray =
            var node = new ComplexValueArrayAst.Builder();
            // "{"
            stream.Read<BlockOpenToken>();
            if (stream.Peek<BlockCloseToken>() == null)
            {
                // [ complexValue
                node.Values.Add(ParserEngine.ParseComplexValueAst(stream));
                // *( "," complexValue) ]
                while (stream.Peek<CommaToken>() != null)
                {
                    stream.Read<CommaToken>();
                    node.Values.Add(ParserEngine.ParseComplexValueAst(stream));
                }
            }
            // "}"
            stream.Read<BlockCloseToken>();
            // return the result
            return node.Build();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.9 Complex type value
        ///
        ///     complexValue      = aliasIdentifier /
        ///                         ( VALUE OF
        ///                           ( structureName / className / associationName )
        ///                           propertyValueList )
        ///
        /// </remarks>
        public static ComplexValueAst ParseComplexValueAst(ParserStream stream)
        {

            // complexValue =
            var node = new ComplexValueAst.Builder();

            // aliasIdentifier /
            if (stream.Peek<AliasIdentifierToken>() != null)
            {
                var aliasIdentifierToken = stream.Read<AliasIdentifierToken>();
                node.IsAlias = true;
                node.Alias = aliasIdentifierToken;
                return node.Build();
            }

            // ( VALUE OF
            var valueKeyword = stream.ReadIdentifier(Constants.VALUE);
            var ofKeyword = stream.ReadIdentifier(Constants.OF);
            node.IsValue = true;

            // ( structureName / className / associationName )
            if (stream.Peek<IdentifierToken>() != null)
            {
                var identifierToken = stream.Read<IdentifierToken>();
                if (!StringValidator.IsStructureName(identifierToken.Name) &&
                    !StringValidator.IsClassName(identifierToken.Name) &&
                    !StringValidator.IsAssociationName(identifierToken.Name))
                {
                    throw new InvalidOperationException("Identifer is not a structureName, className or associationName");
                }
                node.TypeName = identifierToken;
            }

            // propertyValueList )
            node.Properties = ParserEngine.ParsePropertyValueListAst(stream);

            // return the result
            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.9 Complex type value
        ///
        ///     propertyValueList = "{" *propertySlot "}"
        ///     propertySlot      = propertyName "=" propertyValue ";"
        ///     propertyValue     = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
        ///     propertyName      = IDENTIFIER
        ///
        /// </remarks>
        internal static PropertyValueListAst ParsePropertyValueListAst(ParserStream stream)
        {
            var node = new PropertyValueListAst.Builder();
            // "{"
            stream.Read<BlockOpenToken>();
            // *propertySlot
            while (!stream.Eof && (stream.Peek<BlockCloseToken>() == null))
            {
                // propertyName
                var propertyName = stream.Read<IdentifierToken>().Name;
                if (!StringValidator.IsIdentifier(propertyName))
                {
                    throw new InvalidOperationException("Value is not a valid property name.");
                }
                // "="
                stream.Read<EqualsOperatorToken>();
                // propertyValue
                var propertyValue = ParserEngine.ParsePropertyValueAst(stream);
                // ";"
                stream.Read<StatementEndToken>();
                node.PropertyValues.Add(propertyName, propertyValue);
            }
            // "}"
            stream.Read<BlockCloseToken>();
            return node.Build();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.9 Complex type value
        ///
        ///     propertyValue = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
        ///
        /// </remarks>
        internal static PropertyValueAst ParsePropertyValueAst(ParserStream stream)
        {
            var node = new PropertyValueAst.Builder();
            var peek = stream.Peek();
            // propertyValue = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
            if (ParserEngine.IsLiteralValueToken(peek))
            {
                // primitiveTypeValue -> literalValue
                node.Value = ParserEngine.ParsePrimitiveTypeValueAst(stream);
            }
            else if (peek is BlockOpenToken)
            {
                // we need to read the subsequent token to work out whether
                // this is a complexValueArray, literalValueArray, referenceValueArray or enumValueArray
                stream.Read();
                peek = stream.Peek();
                if (ParserEngine.IsLiteralValueToken(peek))
                {
                    // literalValueArray
                    stream.Backtrack();
                    node.Value = ParserEngine.ParseLiteralValueArrayAst(stream);
                }
                else
                {
                    // complexValueType
                    stream.Backtrack();
                    node.Value = ParserEngine.ParseComplexValueArrayAst(stream);
                }
            }
            else if (peek is AliasIdentifierToken)
            {
                // referenceTypeValue
                node.Value = ParserEngine.ParseReferenceTypeValueAst(stream);
            }
            else
            {
                throw new UnexpectedTokenException(peek);
            }
            // return the result
            return node.Build();
        }

        #endregion

        #region 7.6.1 Primitive type value

        internal static bool IsLiteralValueToken(Token token)
        {
            return (token is IntegerLiteralToken) ||
                   //(token is RealLiteralToken) ||
                   //(token is DateTimeLiteralToken) ||
                   (token is StringLiteralToken) ||
                   (token is BooleanLiteralToken) ||
                   //(token is OctetStringLiteralToken) ||
                   (token is NullLiteralToken);
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1 Primitive type value
        ///
        ///     primitiveTypeValue = literalValue / literalValueArray
        ///
        /// </remarks>
        public static PrimitiveTypeValueAst ParsePrimitiveTypeValueAst(ParserStream stream)
        {
            var peek = stream.Peek();
            if (ParserEngine.IsLiteralValueToken(peek))
            {
                // literalValue
                return ParserEngine.ParseLiteralValueAst(stream);
            }
            else if (peek is BlockOpenToken)
            {
                // literalValueArray
                return ParserEngine.ParseLiteralValueArrayAst(stream);
            }
            else
            {
                throw new UnexpectedTokenException(peek);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1 Primitive type value
        ///
        ///     literalValueArray  = "{" [ literalValue *( "," literalValue ) ] "}"
        ///
        /// </remarks>
        public static LiteralValueArrayAst ParseLiteralValueArrayAst(ParserStream stream)
        {
            var node = new LiteralValueArrayAst.Builder();
            // "{"
            stream.Read<BlockOpenToken>();
            // [ literalValue *( "," literalValue) ]
            if (stream.Peek<BlockCloseToken>() == null)
            {
                while (!stream.Eof)
                {
                    if (node.Values.Count > 0)
                    {
                        stream.Read<CommaToken>();
                    }
                    node.Values.Add(ParserEngine.ParseLiteralValueAst(stream));
                    if (stream.Peek<BlockCloseToken>() != null)
                    {
                        break;
                    }
                }
            }
            // "}"
            stream.Read<BlockCloseToken>();
            // return the result
            return node.Build();
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1 Primitive type value
        ///
        ///     literalValue = integerValue / realValue /
        ///                    stringValue / octetStringValue
        ///                    booleanValue /
        ///                    nullValue /
        ///                    dateTimeValue
        ///
        /// </remarks>
        public static LiteralValueAst ParseLiteralValueAst(ParserStream stream)
        {
            var peek = stream.Peek();
            if (peek is IntegerLiteralToken)
            {
                // integerValue
                return ParserEngine.ParseIntegerValueAst(stream);
            }
            else if (peek is StringLiteralToken)
            {
                // stringValue
                return ParserEngine.ParseStringValueAst(stream);
            }
            else if (peek is BooleanLiteralToken)
            {
                // booleanValue
                return ParserEngine.ParseBooleanValueAst(stream);
            }
            else if (peek is NullLiteralToken)
            {
                // nullValue
                return ParserEngine.ParseNullValueAst(stream);
            }
            else
            {
                throw new UnexpectedTokenException(peek);
            }
        }

        #endregion

        #region 7.6.1.1 Integer value

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1.1 Integer value
        ///
        /// No whitespace is allowed between the elements of the rules in this ABNF section.
        ///
        ///     integerValue = binaryValue / octalValue / hexValue / decimalValue
        ///
        /// </remarks>
        public static IntegerValueAst ParseIntegerValueAst(ParserStream stream)
        {
            return new IntegerValueAst.Builder()
            {
                Value = stream.Read<IntegerLiteralToken>().Value
            }.Build();
        }

        #endregion

        #region 7.6.1.2 Real value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1.2 Real value
        ///
        ///     realValue            = ["+" / "-"] * decimalDigit "." 1*decimalDigit
        ///                            [ ("e" / "E") [ "+" / "-" ] 1*decimalDigit ]
        ///
        ///     decimalDigit         = "0" / positiveDecimalDigit
        ///
        ///     positiveDecimalDigit = "1"..."9"
        ///
        /// </remarks>
        public static RealValueAst ParseRealValueAst(ParserStream stream)
        {
            return new RealValueAst.Builder
            {
                Value = stream.Read<IntegerLiteralToken>().Value
            }.Build();
        }

        #endregion

        #region 7.6.1.3 String values

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1.3 String values
        ///
        /// Unless explicitly specified via ABNF rule WS, no whitespace is allowed between the elements of the rules
        /// in this ABNF section.
        ///
        ///     singleStringValue = DOUBLEQUOTE *stringChar DOUBLEQUOTE
        ///     stringValue       = singleStringValue *( *WS singleStringValue )
        ///
        ///     stringChar        = stringUCSchar / stringEscapeSequence
        ///     stringUCSchar     = U+0020...U+0021 / U+0023...U+D7FF /
        ///                         U+E000...U+FFFD / U+10000...U+10FFFF
        ///                         ; Note that these UCS characters can be
        ///                         ; represented in XML without any escaping
        ///                         ; (see W3C XML).
        ///
        ///     stringEscapeSequence = BACKSLASH ( BACKSLASH / DOUBLEQUOTE / SINGLEQUOTE /
        ///                            BACKSPACE_ESC / TAB_ESC / LINEFEED_ESC /
        ///                            FORMFEED_ESC / CARRIAGERETURN_ESC /
        ///                            escapedUCSchar )
        ///
        ///     BACKSPACE_ESC      = "b" ; escape for back space (U+0008)
        ///     TAB_ESC            = "t" ; escape for horizontal tab(U+0009)
        ///     LINEFEED_ESC       = "n" ; escape for line feed(U+000A)
        ///     FORMFEED_ESC       = "f" ; escape for form feed(U+000C)
        ///     CARRIAGERETURN_ESC = "r" ; escape for carriage return (U+000D)
        ///
        ///     escapedUCSchar     = ( "x" / "X" ) 1*6(hexDigit ) ; escaped UCS
        ///                          ; character with a UCS code position that is
        ///                          ; the numeric value of the hex number
        ///
        /// The following special characters are also used in other ABNF rules in this specification:
        ///
        ///     BACKSLASH   = U+005C ; \
        ///     DOUBLEQUOTE = U+0022 ; "
        ///     SINGLEQUOTE = U+0027 ; '
        ///     UPPERALPHA  = U+0041...U+005A ; A ... Z
        ///     LOWERALPHA  = U+0061...U+007A ; a ... z
        ///     UNDERSCORE  = U+005F ; _
        ///
        /// </remarks>
        public static StringValueAst ParseStringValueAst(ParserStream stream)
        {
            return new StringValueAst.Builder()
            {
                Value = stream.Read<StringLiteralToken>().Value
            }.Build();
        }

        #endregion

        #region 7.6.1.5 Boolean value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1.5 Boolean value
        ///
        ///     booleanValue = TRUE / FALSE
        ///     FALSE        = "false" ; keyword: case insensitive
        ///     TRUE         = "true"  ; keyword: case insensitive
        ///
        /// </remarks>
        public static BooleanValueAst ParseBooleanValueAst(ParserStream stream)
        {
            var token = stream.Read<BooleanLiteralToken>();
            return new BooleanValueAst(token);
        }

        #endregion

        #region 7.6.1.6 Null value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// 7.6.1.6 Null value
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        ///     nullValue = NULL
        ///     NULL      = "null" ; keyword: case insensitive
        ///                        ; second
        ///
        /// </remarks>
        public static NullValueAst ParseNullValueAst(ParserStream stream)
        {
            var token = stream.Read<NullLiteralToken>();
            return new NullValueAst.Builder
            {
                Token = token
            }.Build();
        }

        #endregion

        #region 7.6.2 Complex type value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// 7.6.2 Complex type value
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        ///     instanceValueDeclaration = INSTANCE OF ( className / associationName )
        ///                                [alias]
        ///                                propertyValueList ";"
        ///
        ///     alias                    = AS aliasIdentifier
        ///
        /// </remarks>
        public static InstanceValueDeclarationAst ParseInstanceValueDeclarationAst(ParserStream stream)
        {

            var node = new InstanceValueDeclarationAst.Builder();

            // INSTANCE
            stream.ReadIdentifier(Constants.INSTANCE);

            // OF
            stream.ReadIdentifier(Constants.OF);

            // ( className / associationName )
            var nameToken = stream.Read<IdentifierToken>();
            if (!StringValidator.IsClassName(nameToken.Name) &&
                !StringValidator.IsAssociationName(nameToken.Name))
            {
                throw new InvalidOperationException("Identifer is not a className or associationName");
            }
            node.TypeName = nameToken;

            // [alias]
            if (stream.PeekIdentifier(Constants.AS))
            {

                stream.ReadIdentifier(Constants.AS);

                var aliasIdentifierToken = stream.Read<AliasIdentifierToken>();
                node.Alias = aliasIdentifierToken;

            }

            // propertyValueList
            node.PropertyValues = ParserEngine.ParsePropertyValueListAst(stream);

            // ";"
            stream.Read<StatementEndToken>();

            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// 7.6.2 Complex type value
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        ///     structureValueDeclaration = VALUE OF
        ///                                 ( className / associationName / structureName )
        ///                                 alias
        ///                                 propertyValueList ";"
        ///
        ///     alias                     = AS aliasIdentifier
        ///
        /// </remarks>
        public static StructureValueDeclarationAst ParseStructureValueDeclarationAst(ParserStream stream)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region 7.6.4 Reference type value

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.4 Reference type value
        ///
        ///     referenceTypeValue  = referenceValue / referenceValueArray
        ///     referenceValueArray = "{" [ objectPathValue *( "," objectPathValue ) ]
        ///
        /// No whitespace is allowed between the elements of the rules in this ABNF section.
        ///
        ///     objectPathValue = [namespacePath ":"] instanceId
        ///     namespacePath   = [serverPath] namespaceName
        ///
        /// ; Note: The production rules for host and port are defined in IETF
        /// ; RFC 3986 (Uniform Resource Identifiers (URI): Generic Syntax).
        ///
        ///     serverPath       = (host / LOCALHOST) [ ":" port] "/"
        ///     LOCALHOST        = "localhost" ; Case insensitive
        ///     instanceId       = className "." instanceKeyValue
        ///     instanceKeyValue = keyValue *( "," keyValue )
        ///     keyValue         = propertyName "=" literalValue
        ///
        /// </remarks>
        internal static ReferenceTypeValueAst ParseReferenceTypeValueAst(ParserStream stream)
        {
            var node = new ReferenceTypeValueAst.Builder();
            // referenceValue = objectPathValue
            node.Name = stream.Read<AliasIdentifierToken>().Name;
            return node.Build();
        }

        #endregion

    }

}
