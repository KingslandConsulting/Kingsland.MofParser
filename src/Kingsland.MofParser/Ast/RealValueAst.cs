using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class RealValueAst : LiteralValueAst
    {

        private RealValueAst()
        {
        }

        public float Value
        {
            get;
            private set;
        }

        internal new static RealValueAst Parse(ParserStream stream)
        {
            return new RealValueAst
            {
                Value = stream.Read<IntegerLiteralToken>().Value
            };
        }

    }

}
