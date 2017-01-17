using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class CompilerDirectiveAst : MofProductionAst
    {

        #region Constructors

        private CompilerDirectiveAst()
        {
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

        #region Parsing Methods

        internal new static CompilerDirectiveAst Parse(ParserState state)
        {
            var ast = new CompilerDirectiveAst();

            state.Read<PragmaToken>();
            ast.Pragma = state.Read<IdentifierToken>().Name;
            state.Read<ParenthesesOpenToken>();
            ast.Argument = state.Read<StringLiteralToken>().Value;
            state.Read<ParenthesesCloseToken>();

            return ast;
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
