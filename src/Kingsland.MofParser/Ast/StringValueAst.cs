using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class StringValueAst : LiteralValueAst
    {

        private StringValueAst()
        {
        }

        public string Value
        {
            get;
            private set;
        }

        internal new static StringValueAst Parse(ParserStream stream)
        {
            return new StringValueAst
            {
                Value = stream.Read<StringLiteralToken>().Value
            };
        }

        public override string ToString()
        {
            return string.Format("\"{0}\"", this.Value);
        }

    }

}
