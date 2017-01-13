using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Interfaces;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class StringLiteralToken : Token, ILiteralValueToken
    {
        internal StringLiteralToken(SourceExtent extent, string value)
            : base(extent)
        {
            this.Value = value;
        }

        public string Value
        {
            get;
            private set;
        }
        
        LiteralValueAst ILiteralValueToken.ToLiteralValueAst(ParserStream stream)
        {
            return StringValueAst.Parse(stream);
        }
    }

}
