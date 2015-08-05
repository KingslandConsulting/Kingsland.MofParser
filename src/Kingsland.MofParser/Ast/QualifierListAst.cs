using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{
    public class QualifierListAst : AstNode
    {
        public List<QualifierAst> Qualifiers { get; private set; }

        internal QualifierListAst()
        {
            Qualifiers = new List<QualifierAst>();
        }

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
