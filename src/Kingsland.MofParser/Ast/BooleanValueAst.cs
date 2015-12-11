using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class BooleanValueAst : LiteralValueAst
    {

        private BooleanValueAst()
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
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// Section A.17.6 - Boolean value
        ///
        ///     booleanValue = TRUE / FALSE
        ///     FALSE        = "false" ; keyword: case insensitive
        ///     TRUE         = "true"  ; keyword: case insensitive
        ///
        /// </remarks>
        internal new static BooleanValueAst Parse(ParserStream stream)
        {
            return new BooleanValueAst
            {
                Value = stream.Read<BooleanLiteralToken>().Value
            };
        }

    }

}
