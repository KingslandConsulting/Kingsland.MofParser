using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Ast
{

    public class ReferenceTypeValueAst : PropertyValueAst
    {

        #region Constructors

        internal ReferenceTypeValueAst()
        {
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.19 Reference type value
        ///
        ///     referenceTypeValue  = referenceValue / referenceValueArray
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref ReferenceTypeValueAst node, bool throwIfError = false)
        {

            // referenceValue
            parser.Descend();
            var referenceValue = default(ReferenceValueAst);
            if (ReferenceValueAst.TryParse(parser, ref referenceValue, false))
            {
                parser.Commit();
                node = referenceValue;
                return true;
            }
            parser.Backtrack();

            // referenceValueArray
            parser.Descend();
            var referenceValueArray = default(ReferenceValueArrayAst);
            if (ReferenceValueArrayAst.TryParse(parser, ref referenceValueArray, false))
            {
                parser.Commit();
                node = referenceValueArray;
                return true;
            }
            parser.Backtrack();

            // unexpected token
            return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);

        }

        internal new static ReferenceTypeValueAst Parse(Parser parser)
        {
            var node = default(ReferenceTypeValueAst);
            ReferenceTypeValueAst.TryParse(parser, ref node, true);
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
