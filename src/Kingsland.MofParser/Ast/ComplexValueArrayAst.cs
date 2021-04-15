using Kingsland.MofParser.CodeGen;
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
    /// 7.5.9 Complex type value
    ///
    ///     complexValueArray = "{" [ complexValue *( "," complexValue) ] "}"
    ///
    /// </remarks>
    public sealed record ComplexValueArrayAst : ComplexTypeValueAst
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
                    this.Values
                );
            }

        }

        #endregion

        #region Constructors

        internal ComplexValueArrayAst(
            IEnumerable<ComplexValueAst> values
        )
        {
            this.Values = new ReadOnlyCollection<ComplexValueAst>(
                values?.ToList() ?? new List<ComplexValueAst>()
            );
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<ComplexValueAst> Values
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertComplexValueArrayAst(this);
        }

        #endregion

    }

}
