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
                        lexTokens.Add(CommentToken.Read(stream));
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
                    default:
                        if (char.IsWhiteSpace(peek))
                        {
                            lexTokens.Add(WhitespaceToken.Read(stream));
                            break;
                        }
                        else if (char.IsLetter(peek))
                        {
                            var identifier = IdentifierToken.Read(stream);
                            var lower = identifier.Name.ToLowerInvariant();
                            if (lower == BooleanLiteralToken.TrueText.ToLowerInvariant())
                            {
                                lexTokens.Add(new BooleanLiteralToken(identifier.Extent, true));
                            }
                            else if (lower == BooleanLiteralToken.FalseText.ToLowerInvariant())
                            {
                                lexTokens.Add(new BooleanLiteralToken(identifier.Extent, false));
                            }
                            else if (lower == NullLiteralToken.NullText.ToLowerInvariant())
                            {
                                    lexTokens.Add(new BooleanLiteralToken(identifier.Extent, false));
                            }
                            else
                            {
                                lexTokens.Add(identifier);
                            }
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
                        break;
                }
            }

            return lexTokens;

        }

    }

}
