using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;

namespace Kingsland.MofParser.Ast
{
    public sealed class ClassDeclarationAst : MofProductionAst
    {

        #region Constructors

        private ClassDeclarationAst()
        {
            Features = new List<ClassFeatureAst>();
        }

        #endregion

        #region Properties

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

        #endregion

        #region Parsing Methods

        internal new static ClassDeclarationAst Parse(ParserStream stream)
        {
            return ClassDeclarationAst.ParseClassAst(stream, null);
        }

        internal static ClassDeclarationAst Parse(ParserStream stream, QualifierListAst qualifiers)
        {
            return ClassDeclarationAst.ParseClassAst(stream, qualifiers);
        }

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
        internal static ClassDeclarationAst ParseClassAst(ParserStream stream, QualifierListAst qualifiers)
        {

            var node = new ClassDeclarationAst();

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
                var classFeature = ClassFeatureAst.Parse(stream);
                node.Features.Add(classFeature);
            }

            // "}" ";"
            stream.Read<BlockCloseToken>();
            stream.Read<StatementEndToken>();

            return node;

        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}
