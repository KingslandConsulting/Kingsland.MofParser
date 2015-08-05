using System;
using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{
    public sealed class ClassAst : MofProductionAst
    {
        public string Name { get; private set; }
        public string BaseClass { get; private set; }
        public List<MemberAst> Members { get; private set; }

        internal new static ClassAst Parse(ParserStream stream)
        {
            var ast = new ClassAst();

            stream.ReadKeyword("class");

            ast.Name = stream.Read<IdentifierToken>().Name;

            if (!NameValidator.IsStructureName(ast.Name) &&
                !NameValidator.IsClassName(ast.Name) &&
                !NameValidator.IsAssociationName(ast.Name) &&
                !NameValidator.IsSpecialName(ast.Name))
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

        private ClassAst()
        {
            Members = new List<MemberAst>();
        }
    }
}
