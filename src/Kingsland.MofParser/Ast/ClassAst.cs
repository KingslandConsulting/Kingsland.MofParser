using System;
using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{
    public sealed class ClassAst : MofProductionAst
    {

        private ClassAst()
        {
            Members = new List<MemberAst>();
        }

        public string Name
        {
            get;
            private set;
        }

        public string BaseClass
        {
            get;
            private set;
        }

        public List<MemberAst> Members
        {
            get;
            private set;
        }

        internal new static ClassAst Parse(ParserStream stream)
        {
            var ast = new ClassAst();

            stream.ReadKeyword(Keywords.CLASS);

            ast.Name = stream.Read<IdentifierToken>().Name;

            if (!StringValidator.IsStructureName(ast.Name) &&
                !StringValidator.IsClassName(ast.Name) &&
                !StringValidator.IsAssociationName(ast.Name) &&
                !StringValidator.IsSpecialName(ast.Name))
            {
                throw new InvalidOperationException("Identifer is not a structureName, className or associationName");
            }

            if (stream.Peek<ColonToken>() != null)
            {
                stream.Read<ColonToken>();
                ast.BaseClass = stream.Read<IdentifierToken>().Name;
            }

            stream.Read<BlockOpenToken>();

            while (!stream.Eof)
            {
                QualifierListAst memberQualifiers = null;

                if (stream.Peek<BlockCloseToken>() != null)
                {
                    stream.Read<BlockCloseToken>();
                    stream.Read<StatementEndToken>();
                    break;
                }

                if (stream.Peek<AttributeOpenToken>() != null)
                {
                    memberQualifiers = QualifierListAst.Parse(stream);
                }

                ast.Members.Add(MemberAst.Parse(stream, memberQualifiers));
            }

            return ast;
        }

    }
}
