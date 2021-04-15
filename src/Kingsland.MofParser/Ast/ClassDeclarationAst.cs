using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
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
    ///
    ///     superClass       = ":" className
    ///
    ///     classFeature     = structureFeature /
    ///                        methodDeclaration
    ///
    ///     CLASS            = "class" ; keyword: case insensitive
    ///
    /// </remarks>
    public sealed record ClassDeclarationAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.ClassFeatures = new List<IClassFeatureAst>();
            }

            public QualifierListAst QualifierList
            {
                get;
                set;
            }

            public IdentifierToken ClassName
            {
                get;
                set;
            }

            public IdentifierToken SuperClass
            {
                get;
                set;
            }

            public List<IClassFeatureAst> ClassFeatures
            {
                get;
                set;
            }

            public ClassDeclarationAst Build()
            {
                return new ClassDeclarationAst(
                    this.QualifierList,
                    this.ClassName,
                    this.SuperClass,
                    this.ClassFeatures
                );
            }

        }

        #endregion

        #region Constructors

        internal ClassDeclarationAst(
            QualifierListAst qualifierList,
            IdentifierToken className,
            IdentifierToken superClass,
            IEnumerable<IClassFeatureAst> classFeatures
        )
        {
            this.QualifierList = qualifierList ?? new QualifierListAst();
            this.ClassName = className ?? throw new ArgumentNullException(nameof(className));
            this.SuperClass = superClass;
            this.ClassFeatures = new ReadOnlyCollection<IClassFeatureAst>(
                classFeatures?.ToList() ?? new List<IClassFeatureAst>()
            );
        }

        #endregion

        #region Properties

        public QualifierListAst QualifierList
        {
            get;
            private init;
        }

        public IdentifierToken ClassName
        {
            get;
            private init;
        }

        public IdentifierToken SuperClass
        {
            get;
            private init;
        }

        public ReadOnlyCollection<IClassFeatureAst> ClassFeatures
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertClassDeclarationAst(this);
        }

        #endregion

    }

}
