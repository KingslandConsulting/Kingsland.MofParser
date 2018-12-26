using System;

namespace Kingsland.MofParser.Ast
{

    public abstract class ComplexTypeValueAst : MofProductionAst
    {

        #region Constructors

        protected ComplexTypeValueAst(QualifierListAst qualifiers)
        {
            this.Qualifiers = qualifiers ?? throw new ArgumentNullException(nameof(qualifiers));
        }

        #endregion

        #region Properties

        public QualifierListAst Qualifiers
        {
            get;
            private set;
        }

        #endregion

    }

}
