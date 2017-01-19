using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class BooleanValueAst : LiteralValueAst
    {

        #region Constructors

        private BooleanValueAst()
        {
        }

        #endregion

        #region Properties

        public BooleanLiteralToken Token
        {
            get;
            private set;
        }

        public new bool Value
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.17.6 Boolean value
        ///
        ///     booleanValue = TRUE / FALSE
        ///     FALSE        = "false" ; keyword: case insensitive
        ///     TRUE         = "true"  ; keyword: case insensitive
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref BooleanValueAst node, bool throwIfError = false)
        {

            // booleanValue
            var booleanValue = default(BooleanLiteralToken);
            if (!parser.TryRead<BooleanLiteralToken>(ref booleanValue))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }

            // build the result
            var result =  new BooleanValueAst
            {
                Value = booleanValue.Value
            };

            // return the result
            node = result;
            return true;
        }

        internal new static BooleanValueAst Parse(Parser parser)
        {
            var node = default(BooleanValueAst);
            BooleanValueAst.TryParse(parser, ref node, true);
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
