using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System.Text;
using System.Linq;

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
        internal new static ComplexValueArrayAst Parse(ParserStream stream)
        {
            // complexValueArray =
            var node = new ComplexValueArrayAst();
            // "{"
            stream.Read<BlockOpenToken>();
            // [ complexValue
            node.Values.Add(ComplexValueAst.Parse(stream));
            // *( "," complexValue) ]
            while (stream.Peek<CommaToken>() != null)
            {
                stream.Read<CommaToken>();
                node.Values.Add(ComplexValueAst.Parse(stream));
            }
            // "}"
            stream.Read<BlockCloseToken>();
            // return the result
            return node;
        }

        #endregion

        #region AstNode Members

        public override string GetMofSource()
        {
            return string.Format("{{{0}}}", string.Join(", ", this.Values.Select(v => v.ToString()).ToArray()));
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return this.GetMofSource();
        }

        #endregion

    }

}
