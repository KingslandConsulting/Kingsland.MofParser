using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{

    public sealed class PropertyValueAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public object Value
            {
                get;
                set;
            }

            public PropertyValueAst Build()
            {
                return new PropertyValueAst(
                    this.Value
                );
            }

        }

        #endregion

        #region Constructors

        public PropertyValueAst(object value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public object Value
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
