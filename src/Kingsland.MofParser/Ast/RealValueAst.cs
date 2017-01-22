using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class RealValueAst : LiteralValueAst
    {

        #region Constructors

        private RealValueAst()
        {
        }

        #endregion

        #region Properties

        public float Value
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.17.2 Real value
        /// No whitespace is allowed between the elements of the rules in this ABNF section.
        ///
        ///     decimalDigit         = "0" / positiveDecimalDigit
        ///     positiveDecimalDigit = "1"..."9"
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref RealValueAst node, bool throwIfError = false)
        {

            // realValue
            var realValue = default(IntegerLiteralToken);
            if (!parser.TryRead<IntegerLiteralToken>(ref realValue))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }

            // build the result
            var result = new RealValueAst
            {
                Value = realValue.Value
            };

            // return the result
            node = result;
            return true;

        }

        internal new static RealValueAst Parse(Parser parser)
        {
            var node = default(RealValueAst);
            RealValueAst.TryParse(parser, ref node, true);
            return node;
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
