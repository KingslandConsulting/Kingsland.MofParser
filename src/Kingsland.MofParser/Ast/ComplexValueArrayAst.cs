using Kingsland.MofParser.CodeGen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    public sealed class ComplexValueArrayAst : ComplexTypeValueAst
    {

        #region Builder

        public class Builder
        {

            public QualifierListAst Qualifiers
            {
                get;
                set;
            }

            public List<ComplexValueAst> Values
            {
                get;
                private set;
            }

            public ComplexValueArrayAst Build()
            {
                return new ComplexValueArrayAst(
                    this.Qualifiers,
                    new ReadOnlyCollection<ComplexValueAst>(
                        this.Values ?? new List<ComplexValueAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        private ComplexValueArrayAst(
            QualifierListAst qualifiers,
            ReadOnlyCollection<ComplexValueAst> values
        ) : base(qualifiers)
        {
            this.Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<ComplexValueAst> Values
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
