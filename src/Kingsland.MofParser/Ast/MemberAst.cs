using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public abstract class MemberAst : MofProductionAst
    {
        public QualifierListAst Qualifiers { get; private set; }
        public string Name { get; private set; }

        internal static MemberAst Parse(ParserStream stream, QualifierListAst qualifiers)
        {
            var type = stream.Read<IdentifierToken>();

            bool isRef = stream.PeekKeyword(Keywords.REF);
            if (isRef)
            {
                stream.ReadKeyword(Keywords.REF);
            }

            var name = stream.Read<IdentifierToken>();

            if (stream.Peek<ParenthesesOpenToken>() != null && !isRef)
            {
                var ast = new MethodAst
                {
                    Qualifiers = qualifiers,
                    Name = name.Name,
                    ReturnType = type.Name
                };

                stream.Read<ParenthesesOpenToken>();
                while (!stream.Eof)
                {
                    if (stream.Peek<ParenthesesCloseToken>() != null)
                        break;

                    QualifierListAst argQualifiers = null;
                    if (stream.Peek<AttributeOpenToken>() != null)
                    {
                        argQualifiers = QualifierListAst.Parse(stream);
                    }

                    var argument = new MethodAst.Argument
                    {
                        Qualifiers = argQualifiers
                    };

                    argument.Type = stream.Read<IdentifierToken>().Name;

                    if (stream.PeekKeyword(Keywords.REF))
                    {
                        stream.ReadKeyword(Keywords.REF);
                        argument.IsRef = true;
                    }
                    else
                    {
                        argument.IsRef = false;
                    }

                    argument.Name = stream.Read<IdentifierToken>().Name;

                    if (stream.Peek<AttributeOpenToken>() != null)
                    {
                        stream.Read<AttributeOpenToken>();
                        stream.Read<AttributeCloseToken>();
                    }

                    if (stream.Peek<EqualsOperatorToken>() != null)
                    {
                        stream.Read<EqualsOperatorToken>();
                        argument.DefaultValue = LiteralValueAst.Parse(stream);
                    }

                    ast.Arguments.Add(argument);

                    if (stream.Peek<CommaToken>() == null)
                        break;

                    stream.Read<CommaToken>();
                }
                stream.Read<ParenthesesCloseToken>();
                stream.Read<StatementEndToken>();

                return ast;
            }
            else
            {
                var ast = new FieldAst
                {
                    Qualifiers = qualifiers,
                    Name = name.Name,
                    Type = type.Name,
                    IsRef = isRef
                };

                if (stream.Peek<AttributeOpenToken>() != null)
                {
                    stream.Read<AttributeOpenToken>();
                    stream.Read<AttributeCloseToken>();
                    ast.IsArray = true;
                }

                if (stream.Peek<EqualsOperatorToken>() != null)
                {
                    stream.Read<EqualsOperatorToken>();
                    ast.Initializer = LiteralValueAst.Parse(stream);
                }

                stream.Read<StatementEndToken>();

                return ast;
            }
        }
    }

}
