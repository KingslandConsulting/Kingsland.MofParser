using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System.Collections.Generic;

namespace Kingsland.MofParser.Ast
{

    public sealed class ReferenceValueArrayAst : ReferenceTypeValueAst
    {

        #region Fields

        private List<ReferenceValueAst> _values;

        #endregion

        #region Constructors

        private ReferenceValueArrayAst()
        {
        }

        #endregion

        #region Properties

        public List<ReferenceValueAst> Values
        {
            get
            {
                if (_values == null)
                {
                    _values = new List<ReferenceValueAst>();
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
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.19 Reference type value
        ///
        ///     referenceTypeValue  = referenceValue / referenceValueArray
        ///     referenceValueArray = "{" [ objectPathValue *( "," objectPathValue ) ]
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref ReferenceValueArrayAst node, bool throwIfError = false)
        {

            // "{"
            var blockOpen = default(BlockOpenToken);
            if (!parser.TryRead(ref blockOpen))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }

            // [ objectPath *( "," objectPath) ]
            var referenceValues = new List<ReferenceValueAst>();
            while (!parser.Eof && (parser.Peek<BlockCloseToken>() == null))
            {
                var comma = default(CommaToken);
                if ((referenceValues.Count > 0) && (!parser.TryRead(ref comma)))
                {
                    return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
                }
                var referenceValue = default(ReferenceValueAst);
                if (ReferenceValueAst.TryParse(parser, ref referenceValue, throwIfError))
                {
                    referenceValues.Add(referenceValue);
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
            var result = new ReferenceValueArrayAst();
            result.Values.AddRange(referenceValues);

            // return the result
            node = result;
            return true;

        }

        internal new static ReferenceValueArrayAst Parse(Parser parser)
        {
            var node = default(ReferenceValueArrayAst);
            ReferenceValueArrayAst.TryParse(parser, ref node, true);
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
