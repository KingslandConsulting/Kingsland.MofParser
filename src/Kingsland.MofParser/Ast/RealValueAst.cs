using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class RealValueAst : LiteralValueAst
    {

        #region Constructors

        private RealValueAst()
        {
        }

        #endregion

        #region Properties

        public decimal Value
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

        internal new static RealValueAst Parse(ParserStream stream)
        {
            return new RealValueAst
            {
                Value = stream.Read<RealLiteralToken>().Value
            };
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}
