using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Parsing
{

    internal sealed class ParserEngine
    {

        #region A.1 Value definitions

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
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.1 Value definitions
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

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.1 Value definitions
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

        #endregion

        #region A.2 MOF specification

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.2 MOF specification
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
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.2 MOF specification
        ///
        ///     mofProduction = compilerDirective /
        ///                     structureDeclaration /
        ///                     classDeclaration /
        ///                     associationDeclaration /
        ///                     enumerationDeclaration /
        ///                     instanceDeclaration /
        ///                     qualifierDeclaration
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

            // all other mofProduction structures can start with an optional qualifierList
            var qualifiers = default(QualifierListAst);
            if (peek is AttributeOpenToken)
            {
                qualifiers = ParserEngine.ParseQualifierListAst(stream);
            }

            var identifier = stream.Peek<IdentifierToken>();
            switch (identifier.GetNormalizedName())
            {

                case Keywords.STRUCTURE:
                    // structureDeclaration
                    throw new UnsupportedTokenException(identifier);

                case Keywords.CLASS:
                    // classDeclaration
                    return ParserEngine.ParseClassDeclarationAst(stream, qualifiers);

                case Keywords.ASSOCIATION:
                    // associationDeclaration
                    throw new UnsupportedTokenException(identifier);

                case Keywords.ENUMERATION:
                    // enumerationDeclaration
                    throw new UnsupportedTokenException(identifier);

                case Keywords.INSTANCE:
                case Keywords.VALUE:
                    // instanceDeclaration
                    return ParserEngine.ParseComplexTypeValueAst(stream);

                case Keywords.QUALIFIER:
                    // qualifierDeclaration
                    throw new UnsupportedTokenException(identifier);

                default:
                    throw new UnexpectedTokenException(peek);

            }

        }

        #endregion

        #region A.5 Class declaration

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.5 Class declaration
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
            stream.ReadIdentifier(Keywords.CLASS);

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
                var peek = stream.Peek() as BlockCloseToken;
                if (peek != null)
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
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.5 Class declaration
        ///
        ///     classFeature     = structureFeature / methodDeclaration
        ///
        ///     structureFeature = structureDeclaration / ; local structure
        ///                        enumDeclaration /      ; local enumeration
        ///                        propertyDeclaration
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

            // we now need to work out if it's a structureDeclaration, enumDeclaration,
            // propertyDeclaration or methodDeclaration
            var identifier = stream.Peek<IdentifierToken>();
            var identifierName = identifier.GetNormalizedName();
            if (identifier == null)
            {
                throw new UnexpectedTokenException(peek);
            }
            else if (identifierName == Keywords.STRUCTURE)
            {
                // structureDeclaration
                throw new UnsupportedTokenException(identifier);
            }
            else if (identifierName == Keywords.ENUMERATION)
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
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.10 Property declaration
        /// Whitespace as defined in 5.2 is allowed between the elements of the rules in this ABNF section.
        ///
        ///     propertyDeclaration = [ qualifierList ] ( primitivePropertyDeclaration /
        ///                                               complexPropertyDeclaration /
        ///                                               enumPropertyDeclaration /
        ///                                               referencePropertyDeclaration) ";"
        ///
        ///     primitivePropertyDeclaration = primitiveType propertyName [ array ]
        ///                                    [ "=" primitiveTypeValue]
        ///     complexPropertyDeclaration   = structureOrClassName propertyName [ array ]
        ///                                    [ "=" ( complexTypeValue / aliasIdentifier ) ]
        ///     enumPropertyDeclaration      = enumName propertyName [ array ]
        ///                                    [ "=" enumTypeValue]
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
        /// A.11 Method declaration
        /// Whitespace as defined in 5.2 is allowed between the elements of the rules in this ABNF section.
        ///
        ///     methodDeclaration = [ qualifierList ] ( ( returnDataType [ array ] ) /
        ///                         VOID ) methodName
        ///                         "(" [ parameterList ] ")" ";"
        ///
        ///     returnDataType    = primitiveType /
        ///                         structureOrClassName /
        ///                         enumName /
        ///                         classReference
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
            if (stream.PeekIdentifier(Keywords.REF))
            {
                @ref = stream.ReadIdentifier(Keywords.REF);
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
                    Name = memberName,
                    Type = returnType,
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
                case Keywords.DT_UINT8:
                case Keywords.DT_UINT16:
                case Keywords.DT_UINT32:
                case Keywords.DT_UINT64:
                case Keywords.DT_SINT8:
                case Keywords.DT_SINT16:
                case Keywords.DT_SINT32:
                case Keywords.DT_SINT64:
                case Keywords.DT_REAL32:
                case Keywords.DT_REAL64:
                case Keywords.DT_STRING:
                case Keywords.DT_DATETIME:
                case Keywords.DT_BOOLEAN:
                case Keywords.DT_OCTECTSTRING:
                    // primitiveType
                    return PrimitiveTypeValueAst.Parse(stream);
                default:
                    /// structureOrClassName
                    /// enumName
                    /// classReference
                    var peek = stream.Peek();
                    if (peek is NullLiteralToken)
                    {
                        return NullValueAst.Parse(stream);
                    }
                    throw new UnsupportedTokenException(stream.Peek());
            }
        }

        #endregion

        #region A.7.1 Compiler directives

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.7.1 Compiler directives
        ///
        ///     compilerDirective = PRAGMA directiveName "(" stringValue ")"
        ///     PRAGMA = "#pragma"                        ; keyword: case insensitive
        ///     directiveName = IDENTIFIER
        ///
        /// </remarks>
        public static CompilerDirectiveAst ParseCompilerDirectiveAst(ParserStream stream)
        {

            var node = new CompilerDirectiveAst.Builder();

            // PRAGMA
            stream.Read<PragmaToken>();

            // directiveName
            node.Pragma = stream.Read<IdentifierToken>().Name;

            // "("
            stream.Read<ParenthesesOpenToken>();

            // stringValue
            node.Argument = stream.Read<StringLiteralToken>().Value;

            // ")"
            stream.Read<ParenthesesCloseToken>();

            return node.Build();

        }

        #endregion

        #region A.7.2 Qualifiers

        public static QualifierDeclarationAst ParseQualifierDeclarationAst(ParserStream stream)
        {

            var node = new QualifierDeclarationAst.Builder();

            node.Name = stream.Read<IdentifierToken>();

            if (stream.Peek<ParenthesesOpenToken>() != null)
            {
                stream.Read<ParenthesesOpenToken>();
                node.Initializer = LiteralValueAst.Parse(stream);
                stream.Read<ParenthesesCloseToken>();
            }
            else if (stream.Peek<BlockOpenToken>() != null)
            {
                node.Initializer = LiteralValueArrayAst.Parse(stream);
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

        #region A.9 Qualifier list

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.9 Qualifier list
        ///
        ///     qualifierList                = "[" qualifierValue *( "," qualifierValue ) "]"
        ///     qualifierValue               = qualifierName [ qualifierValueInitializer /
        ///                                    qualiferValueArrayInitializer ]
        ///     qualifierValueInitializer     = "(" literalValue ")"
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

        #region A.12 Parameter declaration

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.12 Parameter declaration
        ///
        ///     parameterDeclaration = [ qualifierList ] ( primitiveParamDeclaration /
        ///                            complexParamDeclaration /
        ///                            enumParamDeclaration /
        ///                            referenceParamDeclaration )
        ///
        ///     primitiveParamDeclaration = primitiveType parameterName [ array ]
        ///                                 [ "=" primitiveTypeValue ]
        ///     complexParamDeclaration   = structureOrClassName parameterName [ array ]
        ///                                 [ "=" ( complexTypeValue / aliasIdentifier ) ]
        ///     enumParamDeclaration      = enumName parameterName [ array ]
        ///                                 [ "=" enumValue ]
        ///     referenceParamDeclaration = classReference parameterName [ array ]
        ///                                 [ "=" referenceTypeValue ]
        ///
        ///     parameterName = IDENTIFIER
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
            if (stream.PeekIdentifier(Keywords.REF))
            {
                stream.ReadIdentifier(Keywords.REF);
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

        #region A.14 Complex type value

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.14 Complex type value
        ///
        ///     complexTypeValue  = complexValue / complexValueArray
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
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.14 Complex type value
        ///
        ///     complexValue      = ( INSTANCE / VALUE ) OF
        ///                         ( structureName / className / associationName )
        ///                         [ alias ] propertyValueList ";"
        ///     propertyValueList = "{" *propertySlot "}"
        ///     propertySlot      = propertyName "=" propertyValue ";"
        ///     propertyValue     = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
        ///     alias             = AS aliasIdentifier
        ///     INSTANCE          = "instance" ; keyword: case insensitive
        ///     VALUE             = "value"    ; keyword: case insensitive
        ///     AS                = "as"       ; keyword: case insensitive
        ///     OF                = "of"       ; keyword: case insensitive
        ///
        ///     propertyName      = IDENTIFIER
        ///
        /// </remarks>
        public static ComplexValueAst ParseComplexValueAst(ParserStream stream)
        {

            // complexValue =
            var node = new ComplexValueAst.Builder();

            // ( INSTANCE / VALUE )
            var keyword = stream.ReadIdentifier();
            switch (keyword.GetNormalizedName())
            {
                case Keywords.INSTANCE:
                    node.IsInstance = true;
                    node.IsValue = false;
                    break;
                case Keywords.VALUE:
                    node.IsInstance = false;
                    node.IsValue = true;
                    break;
                default:
                    throw new UnexpectedTokenException(keyword);
            }

            // OF
            stream.ReadIdentifier(Keywords.OF);

            // ( structureName / className / associationName )
            node.TypeName = stream.Read<IdentifierToken>().Name;
            if (!StringValidator.IsStructureName(node.TypeName) &&
                !StringValidator.IsClassName(node.TypeName) &&
                !StringValidator.IsAssociationName(node.TypeName))
            {
                throw new InvalidOperationException("Identifer is not a structureName, className or associationName");
            }

            // [ alias ]
            if (stream.PeekIdentifier(Keywords.AS))
            {
                stream.ReadIdentifier(Keywords.AS);
                var aliasName = stream.Read<AliasIdentifierToken>().Name;
                if (!StringValidator.IsIdentifier(aliasName))
                {
                    throw new InvalidOperationException("Value is not a valid aliasIdentifier");
                }
                node.Alias = aliasName;
            }

            // propertyValueList
            stream.Read<BlockOpenToken>();
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
                node.Properties.Add(propertyName, propertyValue);
            }

            // "}"
            stream.Read<BlockCloseToken>();

            // ";"
            stream.Read<StatementEndToken>();

            // return the result
            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.14 Complex type value
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
            // [ complexValue
            node.Values.Add(ParserEngine.ParseComplexValueAst(stream));
            // *( "," complexValue) ]
            while (stream.Peek<CommaToken>() != null)
            {
                stream.Read<CommaToken>();
                node.Values.Add(ParserEngine.ParseComplexValueAst(stream));
            }
            // "}"
            stream.Read<BlockCloseToken>();
            // return the result
            return node.Build();
        }

        #region Parsing Methods

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.14 Complex type value
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
                node.Value = PrimitiveTypeValueAst.Parse(stream);
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
                    node.Value = LiteralValueArrayAst.Parse(stream);
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


        #endregion

        #region A.17 Primitive type values

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.17 Primitive type values
        ///
        ///     primitiveTypeValue = literalValue / literalValueArray
        ///
        /// </remarks>
        public static PrimitiveTypeValueAst Parse(ParserStream stream)
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

        #endregion

        #region A.17.6 Boolean value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.17.6 Boolean value
        ///
        ///     booleanValue = TRUE / FALSE
        ///     FALSE        = "false" ; keyword: case insensitive
        ///     TRUE         = "true"  ; keyword: case insensitive
        ///
        /// </remarks>
        public static BooleanValueAst ParseBooleanValueAst(ParserStream stream)
        {
            var token = stream.Read<BooleanLiteralToken>();
            return new BooleanValueAst(token, token.Value);
        }

        #endregion

        #region A.17.7 Null value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// A.17.7 Null value
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        ///
        ///     nullValue = NULL
        ///     NULL = "null" ; keyword: case insensitive
        ///                   ; second
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

        #region A.17.1 Integer value

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.17.1 Integer value
        ///
        /// No whitespace is allowed between the elements of the rules in this ABNF section.
        ///
        ///     integerValue         = binaryValue / octalValue / hexValue / decimalValue
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

        #region A.17.2 Real value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.17.2 Real value
        ///
        ///     realValue            = ["+" / "-"] * decimalDigit "." 1*decimalDigit
        ///                            [ ("e" / "E") [ "+" / "-" ] 1*decimalDigit ]
        ///     decimalDigit         = "0" / positiveDecimalDigit
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

        #region A.17.3 String values

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.17.3 String values
        ///
        /// Unless explicitly specified via ABNF rule WS, no whitespace is allowed between the elements of the rules
        /// in this ABNF section.
        ///
        ///     stringValue   = DOUBLEQUOTE *stringChar DOUBLEQUOTE
        ///                     *( *WS DOUBLEQUOTE *stringChar DOUBLEQUOTE )
        ///     stringChar    = stringUCSchar / stringEscapeSequence
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

        #region A.19 Reference type value

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.19 Reference type value
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
