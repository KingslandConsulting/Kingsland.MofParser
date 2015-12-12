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
            Features = new List<ClassFeatureAst>();
        }

        public IdentifierToken ClassName
        {
            get;
            private set;
        }

        public IdentifierToken Superclass
        {
            get;
            private set;
        }

        public List<ClassFeatureAst> Features
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
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// 7.7.3 Class declaration
        ///
        ///     classDeclaration = [ qualifierList ] CLASS className [ superClass ]
        ///                        "{" *classFeature "}" ";"
        ///
        ///     className        = elementName
        ///     superClass       = ":" className
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
                var classFeature = ClassFeatureAst.Parse(stream);
                node.Features.Add(classFeature);
            }

            // "}" ";"
            stream.Read<BlockCloseToken>();
            stream.Read<StatementEndToken>();

            return node;

        }

    }

}
