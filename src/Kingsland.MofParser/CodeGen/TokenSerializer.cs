using Kingsland.ParseFx.Syntax;
using System.Text;

namespace Kingsland.MofParser.CodeGen;

public sealed class TokenSerializer
{

    #region Dispatcher

    public static string ToSourceText(IEnumerable<SyntaxToken> tokens)
    {
        if (tokens == null)
        {
            throw new ArgumentNullException(nameof(tokens));
        }
        var source = new StringBuilder();
        foreach (var token in tokens)
        {
            source.Append(token.GetSourceString());
        }
        return source.ToString();
    }

    #endregion

}