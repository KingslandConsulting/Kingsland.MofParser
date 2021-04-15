using Kingsland.MofParser.CodeGen;
using System;

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
    ///     qualifierValueInitializer     = "(" literalValue ")"
    ///
    /// </remarks>
    public sealed record QualifierValueInitializerAst : IQualifierInitializerAst
    {

        #region Builder

        public sealed class Builder
        {

            public LiteralValueAst Value
            {
                get;
                set;
            }

            public QualifierValueInitializerAst Build()
            {
                return new QualifierValueInitializerAst(
                    this.Value
                );
            }

        }

        #endregion

        #region Constructors

        internal QualifierValueInitializerAst(LiteralValueAst value)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion

        #region Properties

        public LiteralValueAst Value
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertQualifierValueInitializerAst(this);
        }

        #endregion

    }

}
