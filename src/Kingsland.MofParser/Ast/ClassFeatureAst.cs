using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public abstract class ClassFeatureAst : AstNode
    {

        #region Constructors

        internal ClassFeatureAst()
        {
        }

        #endregion

        #region Properties

        public IdentifierToken Name
        {
            get;
            private set;
        }

        public QualifierListAst Qualifiers
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

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
        ///     structureDeclaration = [ qualifierList ] STRUCTURE structureName
        ///                            [ superstructure ]
        ///                            "{" *structureFeature "}" ";"
        ///
        ///     enumDeclaration = enumTypeHeader
        ///                       enumName ":" enumTypeDeclaration ";"
        ///     enumTypeHeader  = [ qualifierList ] ENUMERATION
        ///
        ///     propertyDeclaration = [ qualifierList ] ( primitivePropertyDeclaration /
        ///                                               complexPropertyDeclaration /
        ///                                               enumPropertyDeclaration
        ///                                               referencePropertyDeclaration ) ";"
        ///
        ///     methodDeclaration = [ qualifierList ] ( ( returnDataType [ array ] ) /
        ///                                             VOID ) methodName
        ///                                             "(" [ parameterList ] ")" ";"
        ///
        /// </remarks>
        internal static ClassFeatureAst Parse(ParserState state)
        {

            // all classFeatures start with an optional "[ qualifierList ]"
            var qualifierList = default(QualifierListAst);
            var peek = state.Peek() as AttributeOpenToken;
            if ((peek as AttributeOpenToken) != null)
            {
                qualifierList = QualifierListAst.Parse(state);
            }

            // we now need to work out if it's a structureDeclaration, enumDeclaration,
            // propertyDeclaration or methodDeclaration
            var identifier = state.Peek<IdentifierToken>();
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
                return ClassFeatureAst.ParseMemberDeclaration(state, qualifierList);
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
        private static ClassFeatureAst ParseMemberDeclaration(ParserState state, QualifierListAst qualifiers)
        {

            // primitiveType / structureOrClassName / enumName / classReference
            var returnType = state.Read<IdentifierToken>();

            var @ref = default(IdentifierToken);
            if (state.PeekIdentifier(Keywords.REF))
            {
                @ref = state.ReadIdentifier(Keywords.REF);
            }

            // [ array ]
            var returnTypeIsArray = false;
            if(state.Peek<AttributeOpenToken>() != null)
            {
                state.Read<AttributeOpenToken>();
                state.Read<AttributeCloseToken>();
                returnTypeIsArray = true;
            }

            // propertyName / methodName
            var memberName = state.Read<IdentifierToken>();

            if ((state.Peek<ParenthesesOpenToken>() != null) && (@ref == null))
            {
                // read the remainder of a methodDeclaration
                var ast = new MethodDeclarationAst
                {
                    Qualifiers = qualifiers,
                    Name = memberName,
                    ReturnType = returnType,
                    ReturnTypeIsArray = returnTypeIsArray
                };
                // "("
                state.Read<ParenthesesOpenToken>();
                //  [ parameterList ]
                if (state.Peek<ParenthesesCloseToken>() == null)
                {
                    while (!state.Eof)
                    {
                        if (ast.Parameters.Count > 0)
                        {
                            state.Read<CommaToken>();
                        }
                        var parameter = ParameterDeclarationAst.Parse(state);
                        ast.Parameters.Add(parameter);
                        if (state.Peek<ParenthesesCloseToken>() != null)
                        {
                            break;
                        }
                    }
                }
                // ")"
                state.Read<ParenthesesCloseToken>();
                // ";"
                state.Read<StatementEndToken>();
                return ast;
            }
            else
            {
                // read the remainder of a propertyDeclaration
                var ast = new PropertyDeclarationAst
                {
                    Qualifiers = qualifiers,
                    Name = memberName,
                    Type = returnType,
                    IsRef = (@ref != null)
                };
                if (state.Peek<AttributeOpenToken>() != null)
                {
                    state.Read<AttributeOpenToken>();
                    state.Read<AttributeCloseToken>();
                    ast.IsArray = true;
                }
                if (state.Peek<EqualsOperatorToken>() != null)
                {
                    state.Read<EqualsOperatorToken>();
                    ast.Initializer = ClassFeatureAst.ReadDefaultValue(state, returnType);
                }
                state.Read<StatementEndToken>();
                return ast;

            }

        }

        internal static PrimitiveTypeValueAst ReadDefaultValue(ParserState state, IdentifierToken returnType)
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
                    return PrimitiveTypeValueAst.Parse(state);
                default:
                    /// structureOrClassName
                    /// enumName
                    /// classReference
                    var peek = state.Peek();
                    if (peek is NullLiteralToken)
                    {
                        return NullValueAst.Parse(state);
                    }
                    throw new UnsupportedTokenException(state.Peek());
            }
        }

        #endregion

    }

}
