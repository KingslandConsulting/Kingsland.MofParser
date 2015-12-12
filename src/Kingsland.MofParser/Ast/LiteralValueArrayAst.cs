using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class LiteralValueArrayAst : PrimitiveTypeValueAst
    {

        private List<LiteralValueAst> _values;

        private LiteralValueArrayAst()
        {
        }

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

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.1 Value definitions
        ///
        ///     primitiveTypeValue = literalValue / literalValueArray
        ///
        ///     literalValueArray  = "{" [ literalValue *( "," literalValue ) ] "}"
        ///
        ///     literalValue       = integerValue / realValue /
		///		                     stringValue / octetStringValue
		///                          booleanValue /
		///                          nullValue /
		///		                     dateTimeValue
        ///
        /// </remarks>
        internal new static LiteralValueArrayAst Parse(ParserStream stream)
        {
            // complexValueArray =
            var node = new LiteralValueArrayAst();
            // "{"
            stream.Read<BlockOpenToken>();
            // [ literalValue
            node.Values.Add(LiteralValueAst.Parse(stream));
            // *( "," literalValue) ]
            while (stream.Peek<CommaToken>() != null)
            {
                stream.Read<CommaToken>();
                node.Values.Add(LiteralValueAst.Parse(stream));
            }
            // "}"
            stream.Read<BlockCloseToken>();
            // return the result
            return node;
        }

    }

}
