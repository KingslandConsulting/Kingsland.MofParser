using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;

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

            public RealLiteralToken RealLiteralToken
            {
                get;
                set;
            }

            public RealValueAst Build()
            {
                return new RealValueAst(
                    this.RealLiteralToken
                );
            }

        }

        #endregion

        #region Constructors

        public RealValueAst(RealLiteralToken realLiteralToken)
        {
            this.RealLiteralToken = realLiteralToken ?? throw new ArgumentNullException(nameof(RealLiteralToken));
            this.Value = realLiteralToken.Value;
        }

        #endregion

        #region Properties

        public RealLiteralToken RealLiteralToken
        {
            get;
            private set;
        }

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
