using Kingsland.MofParser.CodeGen;

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
    public sealed class QualifierValueInitializerAst : IQualifierInitializerAst
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
                return new QualifierValueInitializerAst
                {
                    Value = this.Value
                };
            }

        }

        #endregion

        #region Constructors

        private QualifierValueInitializerAst()
        {
        }

        #endregion

        #region Properties

        public LiteralValueAst Value
        {
            get;
            private set;
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
