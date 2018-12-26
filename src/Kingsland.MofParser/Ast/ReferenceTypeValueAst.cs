using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{

    public sealed class ReferenceTypeValueAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public string Name
            {
                get;
                set;
            }

            public ReferenceTypeValueAst Build()
            {
                return new ReferenceTypeValueAst(
                    this.Name
                );
            }

        }

        #endregion

        #region Constructors

        private ReferenceTypeValueAst(string name)
        {
            this.Name = name;
        }

        #endregion

        #region Properties

        public string Name
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
