using Kingsland.ParseFx.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kingsland.MofParser.CodeGen
{

    public sealed class TokenSerializer
    {

        #region Dispatcher

        public static string ConvertToMofText(SyntaxToken token)
        {
            return token.Extent.Text;
        }

        public static string ConvertToMofText(IEnumerable<SyntaxToken> tokens)
        {
            if (tokens == null)
            {
                throw new ArgumentNullException(nameof(tokens));
            }
            var source = new StringBuilder();
            foreach (var token in tokens)
            {
                source.Append(TokenSerializer.ConvertToMofText(token));
            }
            return source.ToString();
        }

        #endregion

    }

}