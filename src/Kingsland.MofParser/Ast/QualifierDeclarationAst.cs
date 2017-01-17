using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;

namespace Kingsland.MofParser.Ast
{

    public sealed class QualifierDeclarationAst : MofProductionAst
    {

        #region Constructors

        private QualifierDeclarationAst()
        {
            this.Flavors = new List<string>();
        }

        #endregion

        #region Properties

        public IdentifierToken Name
        {
            get;
            private set;
        }

        public PrimitiveTypeValueAst Initializer
        {
            get;
            private set;
        }

        public List<string> Flavors
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

        internal new static QualifierDeclarationAst Parse(ParserState state)
        {
            var ast = new QualifierDeclarationAst();

            ast.Name = state.Read<IdentifierToken>();

            if (state.Peek<ParenthesesOpenToken>() != null)
            {
                state.Read<ParenthesesOpenToken>();
                ast.Initializer = LiteralValueAst.Parse(state);
                state.Read<ParenthesesCloseToken>();
            }
            else if (state.Peek<BlockOpenToken>() != null)
            {
                ast.Initializer = LiteralValueArrayAst.Parse(state);
            }

            if (state.Peek<ColonToken>() != null)
            {
                state.Read<ColonToken>();
                while (state.Peek<IdentifierToken>() != null)
                {
                    ast.Flavors.Add(state.Read<IdentifierToken>().Name);
                }
            }

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
