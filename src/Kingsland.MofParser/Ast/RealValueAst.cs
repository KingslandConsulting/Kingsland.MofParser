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

        internal new static RealValueAst Parse(ParserStream stream)
        {
            return new RealValueAst
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
