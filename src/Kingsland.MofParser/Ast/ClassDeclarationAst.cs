using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
    public sealed class ClassDeclarationAst : MofProductionAst
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
                    new ReadOnlyCollection<IClassFeatureAst>(
                        this.ClassFeatures ?? new List<IClassFeatureAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public ClassDeclarationAst(QualifierListAst qualifierList, IdentifierToken className, IdentifierToken superClass, ReadOnlyCollection<IClassFeatureAst> classFeatures)
        {
            this.QualifierList = qualifierList ?? new QualifierListAst.Builder().Build();
            this.ClassName = className ?? throw new ArgumentNullException(nameof(className));
            this.SuperClass = superClass;
            this.ClassFeatures = classFeatures ?? new ReadOnlyCollection<IClassFeatureAst>(new List<IClassFeatureAst>());
        }

        #endregion

        #region Properties

        public QualifierListAst QualifierList
        {
            get;
            private set;
        }

        public IdentifierToken ClassName
        {
            get;
            private set;
        }

        public IdentifierToken SuperClass
        {
            get;
            private set;
        }

        public ReadOnlyCollection<IClassFeatureAst> ClassFeatures
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertClassDeclarationAst(this);
        }

        #endregion

    }

}
