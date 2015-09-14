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

        internal new static NullValueAst Parse(ParserStream stream)
        {
			stream.Read<NullLiteralToken>(); // read the 'NULL' token
			return new NullValueAst();
        }

    }

}
