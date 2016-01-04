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
        internal new static LiteralValueArrayAst Parse(ParserStream stream)
        {
            var node = new LiteralValueArrayAst();
            // "{"
            stream.Read<BlockOpenToken>();
            // [ literalValue *( "," literalValue) ]
            if (stream.Peek<BlockCloseToken>() == null)
            {
                while (!stream.Eof)
                {
                    if (node.Values.Count > 0)
                    {
                        stream.Read<CommaToken>();
                    }
                    node.Values.Add(LiteralValueAst.Parse(stream));
                    if (stream.Peek<BlockCloseToken>() != null)
                    {
                        break;
                    }
                }
            }
            // "}"
            stream.Read<BlockCloseToken>();
            // return the result
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
