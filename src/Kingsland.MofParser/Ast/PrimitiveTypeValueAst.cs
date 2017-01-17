using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public abstract class PrimitiveTypeValueAst : AstNode
    {

        #region Constructors

        internal PrimitiveTypeValueAst()
        {
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.17 Primitive type values
        ///
        ///     primitiveTypeValue = literalValue / literalValueArray
        ///
        /// </remarks>
        internal static PrimitiveTypeValueAst Parse(Parser parser)
        {
            var state = parser.CurrentState;
            var peek = state.Peek();
            if (LiteralValueAst.IsLiteralValueToken(peek))
            {
                // literalValue
                return LiteralValueAst.Parse(parser);
            }
            else if(peek is BlockOpenToken)
            {
                // literalValueArray
                return LiteralValueArrayAst.Parse(parser);
            }
            else
            {
                throw new UnexpectedTokenException(peek);
            }
        }

        #endregion

    }

}
