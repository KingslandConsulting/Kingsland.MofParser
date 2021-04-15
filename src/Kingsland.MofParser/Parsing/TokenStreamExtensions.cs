using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;
using System;

namespace Kingsland.MofParser.Parsing
{

    internal static class TokenStreamExtensions
    {

        #region PeekIdentifierToken Methods

        public static bool TryPeekIdentifierToken(this TokenStream stream, string name, out IdentifierToken? result)
        {
            return stream.TryPeek<IdentifierToken>(
                t => t.IsKeyword(name),
                out result
            );
        }

        #endregion

        #region ReadIdentifierToken Methods

        public static IdentifierToken ReadIdentifierToken(this TokenStream stream, string name)
        {
            var token = stream.Read<IdentifierToken>();
            if (!token.IsKeyword(name))
            {
                throw new UnexpectedTokenException(token);
            }
            return token;
        }

        public static IdentifierToken ReadIdentifierToken(this TokenStream stream, Func<IdentifierToken, bool> predicate)
        {
            var token = stream.Read<IdentifierToken>();
            return predicate(token) ? token : throw new UnexpectedTokenException(token);
        }

        public static bool TryReadIdentifierToken(this TokenStream stream, string name, out IdentifierToken? result)
        {
            if (stream.TryPeekIdentifierToken(name, out result))
            {
                stream.Read();
                return true;
            }
            return false;
        }

        #endregion

    }

}
