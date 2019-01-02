using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1.1 Integer value
    ///
    /// No whitespace is allowed between the elements of the rules in this ABNF section.
    ///
    ///     integerValue = binaryValue / octalValue / hexValue / decimalValue
    ///
    /// </remarks>
    public sealed class IntegerValueAst : LiteralValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public long Value
            {
                get;
                set;
            }

            public IntegerKind Kind
            {
                get;
                set;
            }

            public IntegerValueAst Build()
            {
                return new IntegerValueAst(
                    this.Kind,
                    this.Value
                );
            }

        }

        #endregion

        #region Constructors

        public IntegerValueAst(IntegerKind kind, long value)
        {
            this.Kind = kind;
            this.Value = value;
        }

        #endregion

        #region Properties

        public IntegerKind Kind
        {
            get;
            private set;
        }

        public long Value
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertIntegerValueAst(this);
        }

        #endregion

    }

}
