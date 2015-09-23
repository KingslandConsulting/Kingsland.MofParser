using System;
using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public class QualifierAst : AstNode
    {

        #region Constructors

        public QualifierAst()
        {
            this.Flavors = new List<string>();
        }

        #endregion

        #region Properties

        public String Qualifier
        {
            get;
            private set;
        }

        public AstNode Initializer
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

        internal static QualifierAst Parse(ParserStream stream)
        {
            var ast = new QualifierAst();

            ast.Qualifier = stream.Read<IdentifierToken>().Name;

            if (stream.Peek<OpenParenthesesToken>() != null)
            {
                stream.Read<OpenParenthesesToken>();
                ast.Initializer = LiteralValueAst.Parse(stream);
                stream.Read<CloseParenthesesToken>();
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
    }
}
