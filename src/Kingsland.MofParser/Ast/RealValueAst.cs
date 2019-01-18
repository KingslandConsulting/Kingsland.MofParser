using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1.2 Real value
    ///
    ///     realValue            = [ "+" / "-" ] * decimalDigit "." 1*decimalDigit
    ///                            [ ("e" / "E") [ "+" / "-" ] 1*decimalDigit ]
    ///
    ///     decimalDigit         = "0" / positiveDecimalDigit
    ///
    ///     positiveDecimalDigit = "1"..."9"
    ///
    /// </remarks>
    public sealed class RealValueAst : LiteralValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public double Value
            {
                get;
                set;
            }

            public RealValueAst Build()
            {
                return new RealValueAst(
                    this.Value
                );
            }

        }

        #endregion

        #region Constructors

        public RealValueAst(double value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public double Value
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertRealValueAst(this);
        }

        #endregion

    }

}
