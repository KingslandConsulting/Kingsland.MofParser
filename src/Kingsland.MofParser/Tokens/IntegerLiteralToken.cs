using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Interfaces;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IntegerLiteralToken : NumericLiteralToken, ILiteralValueToken
    {

        internal IntegerLiteralToken(SourceExtent extent, long value)
            : base(extent)
        {
            this.Value = value;
        }

        public long Value
        {
            get;
            private set;
        }

        LiteralValueAst ILiteralValueToken.ToLiteralValueAst(ParserStream stream)
        {
            return IntegerValueAst.Parse(stream);
        }
    }

}
