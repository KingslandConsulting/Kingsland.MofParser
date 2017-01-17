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

        internal new static ClassDeclarationAst Parse(ParserState state)
        {
            return ClassDeclarationAst.ParseClassAst(state, null);
        }

        internal static ClassDeclarationAst Parse(ParserState state, QualifierListAst qualifiers)
        {
            return ClassDeclarationAst.ParseClassAst(state, qualifiers);
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
        internal static ClassDeclarationAst ParseClassAst(ParserState state, QualifierListAst qualifiers)
        {

            var node = new ClassDeclarationAst();

            // [ qualifierList ]
            node.Qualifiers = qualifiers;

            // CLASS
            state.ReadIdentifier(Keywords.CLASS);

            // className
            var className = state.Read<IdentifierToken>();
            if (!StringValidator.IsClassName(className.Name))
            {
                throw new InvalidOperationException("Identifer is not a valid class name.");
            }
            node.ClassName = className;

            // [ superClass ]
            if (state.Peek<ColonToken>() != null)
            {
                state.Read<ColonToken>();
                var superclass = state.Read<IdentifierToken>();
                if (!StringValidator.IsClassName(className.Name))
                {
                    throw new InvalidOperationException("Identifer is not a valid superclass name.");
                }
                node.Superclass = superclass;
            }

            // "{"
            state.Read<BlockOpenToken>();

            // *classFeature
            while (!state.Eof)
            {
                var peek = state.Peek() as BlockCloseToken;
                if (peek != null)
                {
                    break;
                }
                var classFeature = ClassFeatureAst.Parse(state);
                node.Features.Add(classFeature);
            }

            // "}"
            state.Read<BlockCloseToken>();

            // ";"
            state.Read<StatementEndToken>();

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
