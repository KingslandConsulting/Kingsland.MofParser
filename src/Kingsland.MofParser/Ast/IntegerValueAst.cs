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
    /// 7.6.1.1 Integer value
    ///
    ///     integerValue = binaryValue / octalValue / hexValue / decimalValue
    ///
    /// </remarks>
    public sealed record IntegerValueAst : LiteralValueAst, IEnumElementValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public IntegerLiteralToken? IntegerLiteralToken
            {
                get;
                set;
            }

            public IntegerValueAst Build()
            {
                return new IntegerValueAst(
                    this.IntegerLiteralToken ?? throw new InvalidOperationException(
                        $"{nameof(this.IntegerLiteralToken)} property must be set before calling {nameof(Build)}."
                    )
                );
            }

        }

        #endregion

        #region Constructors

        internal IntegerValueAst(
            IntegerLiteralToken integerLiteralToken
        )
        {
            this.IntegerLiteralToken = integerLiteralToken;
            this.Kind = integerLiteralToken.Kind;
            this.Value = integerLiteralToken.Value;
        }

        #endregion

        #region Properties

        public IntegerLiteralToken IntegerLiteralToken
        {
            get;
            private init;
        }

        public IntegerKind Kind
        {
            get;
            private init;
        }

        public long Value
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertIntegerValueAst(this);
        }

        #endregion

    }

}
