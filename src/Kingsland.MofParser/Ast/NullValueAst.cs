using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class NullValueAst : LiteralValueAst
    {

        private NullValueAst()
        {
        }

        public bool Value
        {
            get;
            private set;
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
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
        internal new static NullValueAst Parse(ParserStream stream)
        {
			stream.Read<NullLiteralToken>();
			return new NullValueAst();
        }

    }

}
