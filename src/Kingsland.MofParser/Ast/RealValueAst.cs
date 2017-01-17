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

        public float Value
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

        internal new static RealValueAst Parse(ParserState state)
        {
            return new RealValueAst
            {
                Value = state.Read<IntegerLiteralToken>().Value
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
