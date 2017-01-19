using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Ast
{

    public abstract class PrimitiveTypeValueAst : PropertyValueAst
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
        internal static bool TryParse(Parser parser, ref PrimitiveTypeValueAst node, bool throwIfError = false)
        {

            // literalValue
            parser.Descend();
            var literalValue = default(LiteralValueAst);
            if (LiteralValueAst.TryParse(parser, ref literalValue, false))
            {
                parser.Commit();
                node = literalValue;
                return true;
            }
            parser.Backtrack();

            // literalValueArray
            parser.Descend();
            var literalValueArray = default(LiteralValueArrayAst);
            if (LiteralValueArrayAst.TryParse(parser, ref literalValueArray, false))
            {
                parser.Commit();
                node = literalValueArray;
                return true;
            }
            parser.Backtrack();

            // unexpected token
            return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);

        }

        internal new static PrimitiveTypeValueAst Parse(Parser parser)
        {
            var node = default(PrimitiveTypeValueAst);
            PrimitiveTypeValueAst.TryParse(parser, ref node, true);
            return node;
        }

        #endregion

    }

}
