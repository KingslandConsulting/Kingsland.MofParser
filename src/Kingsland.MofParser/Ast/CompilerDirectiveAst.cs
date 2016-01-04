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

        internal new static CompilerDirectiveAst Parse(ParserStream stream)
        {
            var ast = new CompilerDirectiveAst();

            stream.Read<PragmaToken>();
            ast.Pragma = stream.Read<IdentifierToken>().Name;
            stream.Read<ParenthesesOpenToken>();
            ast.Argument = stream.Read<StringLiteralToken>().Value;
            stream.Read<ParenthesesCloseToken>();

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
