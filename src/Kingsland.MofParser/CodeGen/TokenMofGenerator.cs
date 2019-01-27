using Kingsland.MofParser.Tokens;
using System.Collections.Generic;
using System.Text;

namespace Kingsland.MofParser.CodeGen
{

    public sealed class TokenMofGenerator
    {

        #region Dispatcher

        public static string ConvertToMof(IEnumerable<Token> tokens, MofQuirks quirks = MofQuirks.None)
        {
            if (tokens == null)
            {
                return null;
            }
            var source = new StringBuilder();
            foreach (var token in tokens)
            {
                source.Append(token.Extent.Text);
            }
            return source.ToString();
        }

        #endregion

    }

}