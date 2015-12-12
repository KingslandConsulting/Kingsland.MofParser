using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public abstract class MemberAst : MofProductionAst
    {

        public string Name
        {
            get;
            private set;
        }

        public QualifierListAst Qualifiers
        {
            get;
            private set;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        ///
        /// A.10 Property declaration 927
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
        /// A.11 Method declaration 945
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
        internal static MemberAst Parse(ParserStream stream, QualifierListAst qualifiers)
        {

            // primitiveType / structureOrClassName / enumName / classReference
            var returnType = stream.Read<IdentifierToken>();

            var @ref = default(IdentifierToken);
            if (stream.PeekKeyword(Keywords.REF))
            {
                @ref = stream.ReadKeyword(Keywords.REF);
            }

            // [ array ]
            var returnTypeIsArray = false;
            if(stream.Peek<AttributeOpenToken>() != null)
            {
                stream.Read<AttributeOpenToken>();
                stream.Read<AttributeCloseToken>();
                returnTypeIsArray = true;
            }

            // propertyName / methodName
            var memberName = stream.Read<IdentifierToken>();

            if ((stream.Peek<ParenthesesOpenToken>() != null) && (@ref == null))
            {

                // this is a methodDeclaration

                var ast = new MethodAst
                {
                    Qualifiers = qualifiers,
                    Name = memberName.Name,
                    ReturnType = returnType.Name,
                    ReturnTypeIsArray = returnTypeIsArray
                };

                // "("
                stream.Read<ParenthesesOpenToken>();

                //  [ parameterList ]
                if (stream.Peek<ParenthesesCloseToken>() == null)
                {
                    while (!stream.Eof)
                    {
                        if (ast.Arguments.Count > 0)
                        {
                            stream.Read<CommaToken>();
                        }
                        QualifierListAst argQualifiers = null;
                        if (stream.Peek<AttributeOpenToken>() != null)
                        {
                            argQualifiers = QualifierListAst.Parse(stream);
                        }
                        var argument = new MethodAst.Argument
                        {
                            Qualifiers = argQualifiers
                        };
                        argument.Type = stream.Read<IdentifierToken>().Name;
                        if (stream.PeekKeyword(Keywords.REF))
                        {
                            stream.ReadKeyword(Keywords.REF);
                            argument.IsRef = true;
                        }
                        else
                        {
                            argument.IsRef = false;
                        }
                        argument.Name = stream.Read<IdentifierToken>().Name;
                        if (stream.Peek<AttributeOpenToken>() != null)
                        {
                            stream.Read<AttributeOpenToken>();
                            stream.Read<AttributeCloseToken>();
                        }
                        if (stream.Peek<EqualsOperatorToken>() != null)
                        {
                            stream.Read<EqualsOperatorToken>();
                            argument.DefaultValue = MemberAst.ReadDefaultValue(stream, returnType);
                        }
                        ast.Arguments.Add(argument);
                        if (stream.Peek<ParenthesesCloseToken>() != null)
                        {
                            break;
                        }
                    }
                }

                // ")" ";"
                stream.Read<ParenthesesCloseToken>();
                stream.Read<StatementEndToken>();

                return ast;

            }
            else
            {

                // this is a propertyDeclaration

                var ast = new FieldAst
                {
                    Qualifiers = qualifiers,
                    Name = memberName.Name,
                    Type = returnType.Name,
                    IsRef = (@ref == null)
                };

                if (stream.Peek<AttributeOpenToken>() != null)
                {
                    stream.Read<AttributeOpenToken>();
                    stream.Read<AttributeCloseToken>();
                    ast.IsArray = true;
                }

                if (stream.Peek<EqualsOperatorToken>() != null)
                {
                    stream.Read<EqualsOperatorToken>();
                    ast.Initializer = MemberAst.ReadDefaultValue(stream, returnType);
                }

                stream.Read<StatementEndToken>();

                return ast;
            }


        }

        private static PrimitiveTypeValueAst ReadDefaultValue(ParserStream stream, IdentifierToken returnType)
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

    }

}
