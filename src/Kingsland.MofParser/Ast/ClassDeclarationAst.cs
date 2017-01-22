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

        internal new static ClassDeclarationAst Parse(Parser parser)
        {
            return ClassDeclarationAst.ParseClassAst(parser, null);
        }

        internal static ClassDeclarationAst Parse(Parser parser, QualifierListAst qualifiers)
        {
            return ClassDeclarationAst.ParseClassAst(parser, qualifiers);
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
        internal static ClassDeclarationAst ParseClassAst(Parser parser, QualifierListAst qualifiers)
        {

            var node = new ClassDeclarationAst();

            // [ qualifierList ]
            node.Qualifiers = qualifiers;

            // CLASS
            parser.ReadIdentifier(Keywords.CLASS);

            // className
            var className = parser.Read<IdentifierToken>();
            if (!StringValidator.IsClassName(className.Name))
            {
                throw new InvalidOperationException("Identifer is not a valid class name.");
            }
            node.ClassName = className;

            // [ superClass ]
            if (parser.Peek<ColonToken>() != null)
            {
                parser.Read<ColonToken>();
                var superclass = parser.Read<IdentifierToken>();
                if (!StringValidator.IsClassName(className.Name))
                {
                    throw new InvalidOperationException("Identifer is not a valid superclass name.");
                }
                node.Superclass = superclass;
            }

            // "{"
            parser.Read<BlockOpenToken>();

            // *classFeature
            while (!parser.Eof)
            {
                var peek = parser.Peek() as BlockCloseToken;
                if (peek != null)
                {
                    break;
                }
                var classFeature = ClassFeatureAst.Parse(parser);
                node.Features.Add(classFeature);
            }

            // "}"
            parser.Read<BlockCloseToken>();

            // ";"
            parser.Read<StatementEndToken>();

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
