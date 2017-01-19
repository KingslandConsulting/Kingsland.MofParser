using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System.Collections.Generic;

namespace Kingsland.MofParser.Ast
{

    public sealed class ComplexValueArrayAst : ComplexTypeValueAst
    {

        #region Fields

        private List<ComplexValueAst> _values;

        #endregion

        #region Constructors

        private ComplexValueArrayAst()
        {
        }

        #endregion

        #region Properties

        public List<ComplexValueAst> Values
        {
            get
            {
                if (_values == null)
                {
                    _values = new List<ComplexValueAst>();
                }
                return _values;
            }
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.14 Complex type value
        ///
        ///     complexValueArray = "{" [ complexValue *( "," complexValue) ] "}"
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref ComplexValueArrayAst node, bool throwIfError = false)
        {

            // "{"
            var blockOpen = default(BlockOpenToken);
            if (!parser.TryRead(ref blockOpen))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }

            // [ complexValue *( "," complexValue) ]
            var complexValues = new List<ComplexValueAst>();
            while (!parser.Eof && (parser.Peek<BlockCloseToken>() == null))
            {
                var comma = default(CommaToken);
                if ((complexValues.Count > 0) && (!parser.TryRead(ref comma)))
                {
                    return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
                }
                var complexValue = default(ComplexValueAst);
                if (ComplexValueAst.TryParse(parser, ref complexValue, throwIfError))
                {
                    complexValues.Add(complexValue);
                }
                else
                {
                    return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
                }
            }

            // "}"
            var blockClose = default(BlockCloseToken);
            if (!parser.TryRead(ref blockClose))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }

            // build the result
            var result = new ComplexValueArrayAst();
            result.Values.AddRange(complexValues);

            // return the result
            node = result;
            return true;

        }

        internal new static ComplexValueArrayAst Parse(Parser parser)
        {
            var node = default(ComplexValueArrayAst);
            ComplexValueArrayAst.TryParse(parser, ref node, true);
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
