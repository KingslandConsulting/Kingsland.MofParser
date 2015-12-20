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

        internal static QualifierListAst Parse(ParserStream stream)
        {

            var ast = new QualifierListAst();

            stream.Read<AttributeOpenToken>();

            while (!stream.Eof)
            {
                ast.Qualifiers.Add(QualifierDeclarationAst.Parse(stream));
                if (stream.Peek<CommaToken>() == null)
                {
                    break;
                }
                stream.Read<CommaToken>();
            }

            stream.Read<AttributeCloseToken>();

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
