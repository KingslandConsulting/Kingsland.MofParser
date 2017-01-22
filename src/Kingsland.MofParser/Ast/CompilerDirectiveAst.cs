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

        internal new static CompilerDirectiveAst Parse(Parser parser)
        {

            var ast = new CompilerDirectiveAst();

            parser.Read<PragmaToken>();
            ast.Pragma = parser.Read<IdentifierToken>().Name;
            parser.Read<ParenthesesOpenToken>();
            ast.Argument = parser.Read<StringLiteralToken>().Value;
            parser.Read<ParenthesesCloseToken>();

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
