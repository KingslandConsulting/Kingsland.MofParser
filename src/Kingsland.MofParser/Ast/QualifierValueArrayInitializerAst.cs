﻿using Kingsland.MofParser.CodeGen;
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
    /// 7.4.1 QualifierList
    ///
    ///     qualiferValueArrayInitializer = "{" literalValue *( "," literalValue ) "}"
    ///
    /// </remarks>

    public sealed class QualifierValueArrayInitializerAst : IQualifierInitializerAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Values = new List<LiteralValueAst>();
            }

            public List<LiteralValueAst> Values
            {
                get;
                private set;
            }

            public QualifierValueArrayInitializerAst Build()
            {
                return new QualifierValueArrayInitializerAst
                {
                    Values = new ReadOnlyCollection<LiteralValueAst>(
                        this.Values ?? new List<LiteralValueAst>()
                    )
                };
            }

        }

        #endregion

        #region Constructors

        private QualifierValueArrayInitializerAst()
        {
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<LiteralValueAst> Values
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertQualifierValueArrayInitializerAst(this);
        }

        #endregion

    }

}
