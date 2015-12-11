using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class IntegerValueAst : LiteralValueAst
    {

        private IntegerValueAst()
        {
        }

        public int Value
        {
            get;
            private set;
        }

        internal new static IntegerValueAst Parse(ParserStream stream)
        {
            return new IntegerValueAst
            {
                Value = stream.Read<IntegerLiteralToken>().Value
            };
        }

    }

}
