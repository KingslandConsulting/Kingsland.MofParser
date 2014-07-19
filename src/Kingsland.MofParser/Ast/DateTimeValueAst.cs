using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class DateTimeValueAst : LiteralValueAst
    {

        private DateTimeValueAst()
        {
        }

        public float Value
        {
            get; 
            private set; 
        }

        internal new static DateTimeValueAst Parse(ParserStream stream)
        {
            return new DateTimeValueAst
            {
                Value = stream.Read<DateTimeLiteralToken>().Value
            };
        }

    }

}
