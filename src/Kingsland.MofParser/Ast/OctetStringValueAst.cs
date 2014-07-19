using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class OctetStringValueAst : LiteralValueAst
    {

        private OctetStringValueAst()
        {
        }

        public string Value
        {
            get; 
            private set; 
        }

        internal new static OctetStringValueAst Parse(ParserStream stream)
        {
            return new OctetStringValueAst
            {
                Value = stream.Read<OctetStringLiteralToken>().Value
            };
        }

    }

}
