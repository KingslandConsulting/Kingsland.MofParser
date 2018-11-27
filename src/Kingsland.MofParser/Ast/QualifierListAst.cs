using Kingsland.MofParser.CodeGen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    public sealed class QualifierListAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Qualifiers = new List<QualifierDeclarationAst>();
            }

            public List<QualifierDeclarationAst> Qualifiers
            {
                get;
                set;
            }

            public QualifierListAst Build()
            {
                return new QualifierListAst(
                    new ReadOnlyCollection<QualifierDeclarationAst>(
                        this.Qualifiers ?? new List<QualifierDeclarationAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        private QualifierListAst(ReadOnlyCollection<QualifierDeclarationAst> qualifiers)
        {
            this.Qualifiers = qualifiers ?? throw new ArgumentNullException(nameof(qualifiers));
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<QualifierDeclarationAst> Qualifiers
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
