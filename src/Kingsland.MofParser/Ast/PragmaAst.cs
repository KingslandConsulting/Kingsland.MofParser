using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class PragmaAst : MofProductionAst
    {

        #region Constructors

        private PragmaAst()
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

        internal new static PragmaAst Parse(ParserStream stream)
        {
            var ast = new PragmaAst();

            stream.Read<PragmaToken>();
            ast.Pragma = stream.Read<IdentifierToken>().Name;
            stream.Read<ParenthesesOpenToken>();
            ast.Argument = stream.Read<StringLiteralToken>().Value;
            stream.Read<ParenthesesCloseToken>();

            return ast;
        }

        #endregion

        #region AstNode Members

        public override string GetMofSource()
        {
            return string.Format("!!!!!{0}!!!!!", this.GetType().Name);
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
