using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{

    public sealed class RealValueAst : LiteralValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public float Value
            {
                get;
                set;
            }

            public RealValueAst Build()
            {
                return new RealValueAst(
                    this.Value
                );
            }

        }

        #endregion

        #region Constructors

        private RealValueAst(float value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public float Value
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
