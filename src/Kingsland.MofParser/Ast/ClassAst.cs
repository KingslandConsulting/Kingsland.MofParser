using System;
using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{
    public sealed class ClassAst : MofProductionAst
    {

        private ClassAst()
        {
            Members = new List<MemberAst>();
        }

        public string Name
        {
            get;
            private set;
        }

        public string BaseClass
        {
            get;
            private set;
        }

        public List<MemberAst> Members
        {
            get;
            private set;
        }

        public QualifierListAst Qualifiers
        {
            get;
            private set;
        }

        internal new static ClassAst Parse(ParserStream stream)
        {
            return ClassAst.ParseClassAst(stream, null);
        }

        internal static ClassAst Parse(ParserStream stream, QualifierListAst qualifiers)
        {
            return ClassAst.ParseClassAst(stream, qualifiers);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// 7.7.3 Class declaration
        ///
        ///     classDeclaration = [ qualifierList ] CLASS className [ superClass ]
        ///                        "{" *classFeature "}" ";"
        ///     className        = elementName
        ///     superClass       = ":" className
        ///     classFeature     = structureFeature / methodDeclaration
        ///     CLASS            = "class" ; keyword: case insensitive
        ///
        /// </remarks>
        internal static ClassAst ParseClassAst(ParserStream stream, QualifierListAst qualifiers)
        {

            var node = new ClassAst();

            // [ qualifierList ]
            node.Qualifiers = qualifiers;

            // CLASS
            stream.ReadKeyword(Keywords.CLASS);

            // className
            node.Name = stream.Read<IdentifierToken>().Name;
            if (!StringValidator.IsClassName(node.Name))
            {
                throw new InvalidOperationException("Identifer is not a valid class name.");
            }

            // [ superClass ]
            if (stream.Peek<ColonToken>() != null)
            {
                stream.Read<ColonToken>();
                node.BaseClass = stream.Read<IdentifierToken>().Name;
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
                var classFeature = ClassAst.ParseClassFeature(stream);
                node.Members.Add(classFeature);
            }

            // "}" ";"
            stream.Read<BlockCloseToken>();
            stream.Read<StatementEndToken>();

            return node;

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// 7.7.3 Class declaration
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
        internal static MemberAst ParseClassFeature(ParserStream stream)
        {

            // all classFeatures start with an optional "[ qualifierList ]"
            var qualifierList = default(QualifierListAst);
            var peek = stream.Peek() as AttributeOpenToken;
            if ((peek as AttributeOpenToken) != null)
            {
                qualifierList = QualifierListAst.Parse(stream);
            }

            // we now need to work out if it's a structureDeclaration, enumDeclaration,
            // propertyDeclaration or methodDeclaration
            var identifier = stream.Peek<IdentifierToken>();
            var identifierName = identifier.GetNormalizedName();
            if (identifier == null)
            {
                throw new InvalidOperationException("Expected an IdentifierToken.");
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
                return MemberAst.Parse(stream, qualifierList);
            }

        }

    }

}
