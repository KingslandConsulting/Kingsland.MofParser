using System;
using System.Collections.Generic;
using Kingsland.MofParser.Tokens;

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
                switch (peek)
                {
                    case '/':
                        lexTokens.Add(MultilineCommentToken.Read(stream));
                        break;
                    case '$':
                        lexTokens.Add(AliasIdentifierToken.Read(stream));
                        break;
                    case '{':
                        lexTokens.Add(BlockOpenToken.Read(stream));
                        break;
                    case '}':
                        lexTokens.Add(BlockCloseToken.Read(stream));
                        break;
                    case '(':
                        lexTokens.Add(OpenParenthesesToken.Read(stream));
                        break;
                    case ')':
                        lexTokens.Add(CloseParenthesesToken.Read(stream));
                        break;
                    case '[':
                        lexTokens.Add(AttributeOpenToken.Read(stream));
                        break;
                    case ']':
                        lexTokens.Add(AttributeCloseToken.Read(stream));
                        break;
                    case '=':
                        lexTokens.Add(EqualsOperatorToken.Read(stream));
                        break;
                    case '"':
                        lexTokens.Add(StringLiteralToken.Read(stream));
                        break;
                    case ',':
                        lexTokens.Add(CommaToken.Read(stream));
                        break;
                    case ';':
                        lexTokens.Add(StatementEndToken.Read(stream));
                        break;
                    case ':':
                        lexTokens.Add(ColonToken.Read(stream));
                        break;
                    case '#':
                        lexTokens.Add(PragmaToken.Read(stream));
                        break;
                    default:
                        if (char.IsWhiteSpace(peek))
                        {
                            lexTokens.Add(WhitespaceToken.Read(stream));
                            break;
                        }
                        else if (char.IsLetter(peek) || peek == '_')
                        {
                            var identifier = IdentifierToken.Read(stream);
                            switch (identifier.Name)
                            {
                                case "True":
                                    lexTokens.Add(new BooleanLiteralToken(identifier.Extent, true));
                                    break;
                                case "False":
                                    lexTokens.Add(new BooleanLiteralToken(identifier.Extent, false));
                                    break;
                                default:
                                    lexTokens.Add(identifier);
                                    break;
                            }
                            break;
                        }
                        else if (char.IsDigit(peek))
                        {
                            lexTokens.Add(IntegerLiteralToken.Read(stream));
                            break;
                        }
                        else
                        {
                            throw new InvalidOperationException(
                                string.Format("Unexpected character '{0}'", peek));
                        }
                }
            }

            return lexTokens;

        }

    }

}
