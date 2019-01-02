using Kingsland.MofParser.CodeGen;
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
    /// 7.5.9 Complex type value
    ///
    ///     complexValueArray = "{" [ complexValue *( "," complexValue) ] "}"
    ///
    /// </remarks>
    public sealed class ComplexValueArrayAst : ComplexTypeValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Values = new List<ComplexValueAst>();
            }

            public List<ComplexValueAst> Values
            {
                get;
                private set;
            }

            public ComplexValueArrayAst Build()
            {
                return new ComplexValueArrayAst(
                    new ReadOnlyCollection<ComplexValueAst>(
                        this.Values ?? new List<ComplexValueAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public ComplexValueArrayAst(
            ReadOnlyCollection<ComplexValueAst> values
        )
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
            return MofGenerator.ConvertComplexValueArrayAst(this);
        }

        #endregion

    }

}
