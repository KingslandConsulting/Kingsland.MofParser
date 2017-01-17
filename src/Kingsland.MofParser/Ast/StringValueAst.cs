using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class StringValueAst : LiteralValueAst
    {

        #region Constructors

        private StringValueAst()
        {
        }

        #endregion

        #region Properties

        public string Value
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
        /// A.17.3 String values
        /// Unless explicitly specified via ABNF rule WS, no whitespace is allowed between the elements of the rules
        /// in this ABNF section.
        ///
        ///     stringValue = DOUBLEQUOTE *stringChar DOUBLEQUOTE
        ///                   *( *WS DOUBLEQUOTE *stringChar DOUBLEQUOTE )
        ///     stringChar  = stringUCSchar / stringEscapeSequence
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref StringValueAst node, bool throwIfError = false)
        {
            var state = parser.CurrentState;
            // stringValue
            var stringValue = default(StringLiteralToken);
            if (!state.TryRead<StringLiteralToken>(ref stringValue))
            {
                return AstNode.HandleUnexpectedToken(state.Peek(), throwIfError);
            }
            // build the node
            node = new StringValueAst
            {
                Value = stringValue.Value
            };
            // return the result
            return true;
        }

        internal new static StringValueAst Parse(Parser parser)
        {
            var node = default(StringValueAst);
            StringValueAst.TryParse(parser, ref node, true);
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
