using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{
    public sealed class QualifierListAst : AstNode
    {

        #region Constructors

        internal QualifierListAst()
        {
            this.Qualifiers = new List<QualifierAst>();
        }

        #endregion

        #region Properties

        public List<QualifierAst> Qualifiers
        {
            get;
            private set;
        }

        #endregion

        internal static QualifierListAst Parse(ParserStream stream)
        {
            var ast = new QualifierListAst();

            stream.Read<AttributeOpenToken>();

            while (!stream.Eof)
            {
                ast.Qualifiers.Add(QualifierAst.Parse(stream));

                if (stream.Peek<CommaToken>() == null)
                    break;

                stream.Read<CommaToken>();
            }

            stream.Read<AttributeCloseToken>();

            return ast;
        }
    }
}
