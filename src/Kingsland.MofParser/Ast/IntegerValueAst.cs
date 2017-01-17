using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class IntegerValueAst : LiteralValueAst
    {

        #region Constructors

        private IntegerValueAst()
        {
        }

        #endregion

        #region Properties

        public long Value
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
        /// A.17.1 Integer value
        /// No whitespace is allowed between the elements of the rules in this ABNF section.
        ///
        ///     integerValue = binaryValue / octalValue / hexValue / decimalValue
        ///
        ///     binaryValue = ["+" / "-"] 1*binaryDigit ( "b" / "B" )
        ///     binaryDigit = "0" / "1"
        ///
        ///     octalValue         = [ "+" / "-" ] unsignedOctalValue
        ///     unsignedOctalValue = "0" 1*octalDigit
        ///     octalDigit         = "0" / "1" / "2" / "3" / "4" / "5" / "6" / "7"
        ///
        ///     hexValue = [ "+" / "-" ] ( "0x" / "0X" ) 1*hexDigit
        ///     hexDigit = decimalDigit / "a" / "A" / "b" / "B" / "c" / "C" /
        ///                "d" / "D" / "e" / "E" / "f" / "F"
        ///
        ///     decimalValue         = [ "+" / "-" ] unsignedDecimalValue
        ///     unsignedDecimalValue = positiveDecimalDigit* decimalDigit
        ///
        /// A.17.2 Real value
        /// No whitespace is allowed between the elements of the rules in this ABNF section.
        ///
        ///     decimalDigit         = "0" / positiveDecimalDigit
        ///     positiveDecimalDigit = "1"..."9"
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref IntegerValueAst node, bool throwIfError = false)
        {
            // integerValue
            var integerValue = default(IntegerLiteralToken);
            if (!parser.TryRead<IntegerLiteralToken>(ref integerValue))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }
            // build the node
            node = new IntegerValueAst
            {
                Value = integerValue.Value
            };
            // return the result
            return true;
        }

        internal new static IntegerValueAst Parse(Parser parser)
        {
            var node = default(IntegerValueAst);
            IntegerValueAst.TryParse(parser, ref node, true);
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
