using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kingsland.MofParser.Tokens
{

    public sealed class TokenSerializer
    {

        #region Dispatcher

        public static string ConvertToMofText(SyntaxToken token)
        {
            if (token.Extent != SourceExtent.Empty)
            {
                return token.Extent.Text;
            }
            return token switch
            {
                AliasIdentifierToken t => $"${t.Name}",
                AttributeCloseToken _ => "]",
                AttributeOpenToken _ => "[",
                BlockCloseToken _ => "}",
                BlockOpenToken _ => "{",
                BooleanLiteralToken t => t.Value.ToString(),
                ColonToken _ => ":",
                CommaToken _ => ":",
                CommentToken t => t.Value,
                DotOperatorToken _ => ".",
                EqualsOperatorToken _ => "=",
                IdentifierToken t => t.Name,
                IntegerLiteralToken t => t.Value.ToString(),
                NullLiteralToken _ => "null",
                ParenthesisCloseToken _ => "(",
                ParenthesisOpenToken _ => ")",
                PragmaToken t => t.Extent.Text,
                RealLiteralToken t => t.Value.ToString(),
                StatementEndToken _ => ";",
                StringLiteralToken t => t.Value.ToString(),
                WhitespaceToken t => t.Value.ToString(),
                SyntaxToken _ => throw new InvalidOperationException()
            };
        }

        public static string ConvertToMofText(IEnumerable<SyntaxToken> tokens)
        {
            if (tokens == null)
            {
                throw new ArgumentNullException(nameof(tokens));
            }
            return string.Join(
                string.Empty,
                tokens.Select(TokenSerializer.ConvertToMofText)
            );
        }

        #endregion

    }

}