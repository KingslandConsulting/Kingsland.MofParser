using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Interfaces;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class RealLiteralToken : NumericLiteralToken, ILiteralValueToken
    {

        internal RealLiteralToken(SourceExtent extent, decimal value)
            : base(extent)
        {
            this.Value = value;
        }

        public decimal Value
        {
            get;
            private set;
        }

        LiteralValueAst ILiteralValueToken.ToLiteralValueAst(ParserStream stream)
        {
            return RealValueAst.Parse(stream);
        }
    }

}
