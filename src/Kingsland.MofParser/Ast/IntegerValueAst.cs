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

        internal new static IntegerValueAst Parse(ParserStream stream)
        {
            return new IntegerValueAst
            {
                Value = stream.Read<IntegerLiteralToken>().Value
            };
        }

        #endregion

        #region AstNode Members

        public override string GetMofSource()
        {
            return this.Value.ToString();
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return this.GetMofSource();
        }

        #endregion

    }

}
