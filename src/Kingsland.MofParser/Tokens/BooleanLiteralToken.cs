using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Interfaces;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BooleanLiteralToken : Token, ILiteralValueToken
    {

        internal BooleanLiteralToken(SourceExtent extent, bool value)
            : base(extent)
        {
            this.Value = value;
        }

        public bool Value
        {
            get;
            private set;
        }

        LiteralValueAst ILiteralValueToken.ToLiteralValueAst(ParserStream stream)
        {
            return BooleanValueAst.Parse(stream);
        }
    }

}
