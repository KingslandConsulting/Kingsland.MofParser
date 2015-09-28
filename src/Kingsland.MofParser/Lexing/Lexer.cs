using System;
using System.Collections.Generic;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Lexing
{
    
    public static class Lexer
    {

        public static List<Token> Lex(ILexerStream stream)
        {

            var lexTokens = new List<Token>();

            while (!stream.Eof)
            {
                var peek = stream.Peek();
                switch (peek.Value)
                {
                    case '$':
                        lexTokens.Add(AliasIdentifierToken.Read(stream));
                        break;
                    case ']':
                        lexTokens.Add(AttributeCloseToken.Read(stream));
                        break;
                    case '[':
                        lexTokens.Add(AttributeOpenToken.Read(stream));
                        break;
                    case '}':
                        lexTokens.Add(BlockCloseToken.Read(stream));
                        break;
                    case '{':
                        lexTokens.Add(BlockOpenToken.Read(stream));
                        break;
                    case ':':
                        lexTokens.Add(ColonToken.Read(stream));
                        break;
                    case ',':
                        lexTokens.Add(CommaToken.Read(stream));
                        break;
                    case '/':
                        lexTokens.Add(CommentToken.Read(stream));
                        break;
                    case '=':
                        lexTokens.Add(EqualsOperatorToken.Read(stream));
                        break;
                    case ')':
                        lexTokens.Add(ParenthesesCloseToken.Read(stream));
                        break;
                    case '(':
                        lexTokens.Add(ParenthesesOpenToken.Read(stream));
                        break;
                    case '#':
                        lexTokens.Add(PragmaToken.Read(stream));
                        break;
                    case ';':
                        lexTokens.Add(StatementEndToken.Read(stream));
                        break;
                    case '"':
                        lexTokens.Add(StringLiteralToken.Read(stream));
                        break;
                    default:
                        if (StringValidator.IsWhitespace(peek.Value))
                        {
                            lexTokens.Add(WhitespaceToken.Read(stream));
                            break;
                        }
                        else if (StringValidator.IsFirstIdentifierChar(peek.Value))
                        {
                            var identifier = IdentifierToken.Read(stream);
                            if (StringValidator.IsFalse(identifier.Name))
                            {
                                lexTokens.Add(new BooleanLiteralToken(identifier.Extent, false));
                            }
                            else if (StringValidator.IsTrue(identifier.Name))
                            {
                                lexTokens.Add(new BooleanLiteralToken(identifier.Extent, true));
                            }
                            else if (StringValidator.IsNull(identifier.Name))
                            {
                                lexTokens.Add(new NullLiteralToken(identifier.Extent));
                            }
                            else
                            {
                                lexTokens.Add(identifier);
                            }
                        }
                        else if ((peek.Value == '+') || (peek.Value == '-') ||
                                 (StringValidator.IsDecimalDigit(peek.Value)))
                        {
                            lexTokens.Add(IntegerLiteralToken.Read(stream));
                            break;
                        }
                        else
                        {
                            throw new UnexpectedCharacterException(peek);
                        }
                        break;
                }
            }

            return lexTokens;

        }

    }

}
