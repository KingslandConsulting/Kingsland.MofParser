using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{

    public sealed class IntegerValueAst : LiteralValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public long Value
            {
                get;
                set;
            }

            public IntegerValueAst Build()
            {
                return new IntegerValueAst(
                    this.Value
                );
            }

        }

        #endregion

        #region Constructors

        private IntegerValueAst(long value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public long Value
        {
            get;
            private set;
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
