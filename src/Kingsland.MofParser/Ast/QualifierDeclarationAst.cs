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

        internal new static QualifierDeclarationAst Parse(ParserStream stream)
        {
            var ast = new QualifierDeclarationAst();

            ast.Name = stream.Read<IdentifierToken>();

            if (stream.Peek<ParenthesesOpenToken>() != null)
            {
                stream.Read<ParenthesesOpenToken>();
                ast.Initializer = LiteralValueAst.Parse(stream);
                stream.Read<ParenthesesCloseToken>();
            }
            else if (stream.Peek<BlockOpenToken>() != null)
            {
                ast.Initializer = LiteralValueArrayAst.Parse(stream);
            }

            if (stream.Peek<ColonToken>() != null)
            {
                stream.Read<ColonToken>();
                while (stream.Peek<IdentifierToken>() != null)
                {
                    ast.Flavors.Add(stream.Read<IdentifierToken>().Name);
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
