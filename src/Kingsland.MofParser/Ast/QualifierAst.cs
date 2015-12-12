using System;
using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System.Text;

namespace Kingsland.MofParser.Ast
{

    public sealed class QualifierAst : AstNode
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

        internal static QualifierAst Parse(ParserStream stream)
        {
            var ast = new QualifierAst();

            ast.Qualifier = stream.Read<IdentifierToken>().Name;

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

        public override string ToString()
        {
            var result = new StringBuilder();
            if (!string.IsNullOrEmpty(this.Qualifier))
            {
                result.Append(this.Qualifier);
            }
            if(this.Initializer == null)
            {
                // no nothing
            }
            else if (this.Initializer is LiteralValueAst)
            {
                result.AppendFormat("({0})", this.Initializer.ToString());
            }
            else if(this.Initializer is LiteralValueArrayAst)
            {
                result.Append(this.Initializer.ToString());
            }
            else
            {
                throw new InvalidOperationException();
            }
            if(this.Flavors.Count > 0)
            {
                result.AppendFormat(": {0}", string.Join(" ", this.Flavors.ToArray()));
            }
            return result.ToString();
        }

    }

}
