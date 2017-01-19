using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System.Collections.Generic;

namespace Kingsland.MofParser.Ast
{

    public sealed class LiteralValueArrayAst : PrimitiveTypeValueAst
    {

        #region Fields

        private List<LiteralValueAst> _values;

        #endregion

        #region Constructors

        private LiteralValueArrayAst()
        {
        }

        #endregion

        #region Properties

        public List<LiteralValueAst> Values
        {
            get
            {
                if (_values == null)
                {
                    _values = new List<LiteralValueAst>();
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
        /// A.1 Value definitions
        ///
        ///     literalValueArray  = "{" [ literalValue *( "," literalValue ) ] "}"
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref LiteralValueArrayAst node, bool throwIfError = false)
        {

            // "{"
            var blockOpen = default(BlockOpenToken);
            if (!parser.TryRead(ref blockOpen))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }

            // [ literalValue *( "," literalValue) ]
            var literalValues = new List<LiteralValueAst>();
            while (!parser.Eof && (parser.Peek<BlockCloseToken>() == null))
            {
                var comma = default(CommaToken);
                if ((literalValues.Count > 0) && (!parser.TryRead(ref comma)))
                {
                    return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
                }
                var literalValue = default(LiteralValueAst);
                if (LiteralValueAst.TryParse(parser, ref literalValue, throwIfError))
                {
                    literalValues.Add(literalValue);
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
            var result = new LiteralValueArrayAst();
            result.Values.AddRange(literalValues);

            // return the result
            node = result;
            return true;

        }

        internal new static LiteralValueArrayAst Parse(Parser parser)
        {
            var node = default(LiteralValueArrayAst);
            LiteralValueArrayAst.TryParse(parser, ref node, true);
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
