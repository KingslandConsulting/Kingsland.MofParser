using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class DecimalValueAst : LiteralValueAst
    {

        #region Constructors

        private DecimalValueAst()
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

        internal new static DecimalValueAst Parse(ParserStream stream)
        {
            return new DecimalValueAst
            {
                Value = stream.Read<DecimalLiteralToken>().Value
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
