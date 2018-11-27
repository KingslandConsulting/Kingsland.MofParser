using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{

    public sealed class CompilerDirectiveAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public string Pragma
            {
                get;
                set;
            }

            public string Argument
            {
                get;
                set;
            }

            public CompilerDirectiveAst Build()
            {
                return new CompilerDirectiveAst(
                    this.Pragma,
                    this.Argument
                );
            }

        }

        #endregion

        #region Constructors

        private CompilerDirectiveAst(string pragma, string argument)
        {
            this.Pragma = pragma;
            this.Argument = argument;
        }

        #endregion

        #region Properties

        public string Pragma
        {
            get;
            private set;
        }

        public string Argument
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
