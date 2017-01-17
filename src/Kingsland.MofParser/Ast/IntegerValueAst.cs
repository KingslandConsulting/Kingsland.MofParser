using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class IntegerValueAst : LiteralValueAst
    {

        #region Constructors

        private IntegerValueAst()
        {
        }

        #endregion

        #region Properties

        public long Value
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

        internal new static IntegerValueAst Parse(Parser parser)
        {
            var state = parser.CurrentState;
            return new IntegerValueAst
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
