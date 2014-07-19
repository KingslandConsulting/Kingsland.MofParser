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

        internal new static BooleanValueAst Parse(ParserStream stream)
        {
            return new BooleanValueAst
            {
                Value = stream.Read<BooleanLiteralToken>().Value
            };
        }

    }

}
