using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{
    public sealed class ClassDeclarationAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public IdentifierToken ClassName
            {
                get;
                set;
            }

            public IdentifierToken Superclass
            {
                get;
                set;
            }

            public List<ClassFeatureAst> Features
            {
                get;
                set;
            }

            public QualifierListAst Qualifiers
            {
                get;
                set;
            }

            public ClassDeclarationAst Build()
            {
                return new ClassDeclarationAst(
                    this.ClassName,
                    this.Superclass,
                    new ReadOnlyCollection<ClassFeatureAst>(
                        this.Features ?? new List<ClassFeatureAst>()
                    ),
                    this.Qualifiers
                );
            }

        }

        #endregion

        #region Constructors

        private ClassDeclarationAst()
        {
        }

        internal ClassDeclarationAst(IdentifierToken className, IdentifierToken superClass, ReadOnlyCollection<ClassFeatureAst> features, QualifierListAst qualifiers)
        {
            this.ClassName = className ?? throw new ArgumentNullException(nameof(className));
            this.Superclass = superClass ?? throw new ArgumentNullException(nameof(superClass));
            this.Features = features ?? throw new ArgumentNullException(nameof(features));
            this.Qualifiers = qualifiers ?? throw new ArgumentNullException(nameof(qualifiers));
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

        public ReadOnlyCollection<ClassFeatureAst> Features
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

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}
