using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{

    public sealed class StringValueAst : LiteralValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public string Value
            {
                get;
                set;
            }

            public StringValueAst Build()
            {
                return new StringValueAst(
                    this.Value
                );
            }

        }

        #endregion

        #region Constructors

        private StringValueAst(string value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public string Value
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
