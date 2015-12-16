using System;
using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System.Text;

namespace Kingsland.MofParser.Ast
{
    public sealed class ClassAst : MofProductionAst
    {

        #region Constructors

        private ClassAst()
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

        #endregion

        #region AstNode Members

        public override string GetMofSource()
        {
            var source = new StringBuilder();
            if ((this.Qualifiers != null) && this.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendLine(this.Qualifiers.GetMofSource());
            }
            if(this.Superclass == null)
            {
                source.AppendFormat("class {0}\r\n", this.ClassName.Name);
            }
            else
            {
                source.AppendFormat("class {0} : {1}\r\n", this.ClassName.Name, this.Superclass.Name);
            }
            source.AppendLine("{");
            foreach(var feature in this.Features)
            {
                source.AppendFormat("\t{0}\r\n", feature.GetMofSource());
            }
            source.AppendLine("};");
            return source.ToString();
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return this.GetMofSource();
        }

        #endregion

    }

}
