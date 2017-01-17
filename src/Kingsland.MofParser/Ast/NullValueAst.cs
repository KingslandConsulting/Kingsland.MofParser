using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class NullValueAst : LiteralValueAst
    {

        #region Constructors

        private NullValueAst()
        {
        }

        #endregion

        #region Properties

        public NullLiteralToken Token
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
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        ///
        /// Section A.17.7 - Null value
        ///
        ///     nullValue = NULL
        ///     NULL = "null" ; keyword: case insensitive
        ///                   ; second
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref NullValueAst node, bool throwIfError = false)
        {
            // nullValue
            var nullValue = default(NullLiteralToken);
            if (!parser.TryRead<NullLiteralToken>(ref nullValue))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }
            // build the node
            node = new NullValueAst
            {
                Token = nullValue
            };
            // return the result
            return true;
        }

        internal new static NullValueAst Parse(Parser parser)
        {
            var node = default(NullValueAst);
            NullValueAst.TryParse(parser, ref node, true);
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
