using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{
    public sealed class QualifierListAst : AstNode
    {

        #region Constructors

        private QualifierListAst()
        {
            this.Qualifiers = new List<QualifierDeclarationAst>();
        }

        #endregion

        #region Properties

        public List<QualifierDeclarationAst> Qualifiers
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

        internal static QualifierListAst Parse(ParserState state)
        {

            var ast = new QualifierListAst();

            state.Read<AttributeOpenToken>();

            while (!state.Eof)
            {
                ast.Qualifiers.Add(QualifierDeclarationAst.Parse(state));
                if (state.Peek<CommaToken>() == null)
                {
                    break;
                }
                state.Read<CommaToken>();
            }

            state.Read<AttributeCloseToken>();

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
