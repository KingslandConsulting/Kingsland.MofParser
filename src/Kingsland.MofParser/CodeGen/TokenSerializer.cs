using Kingsland.ParseFx.Syntax;
using System.Text;

namespace Kingsland.MofParser.CodeGen;

public static class TokenSerializer
{

    #region Dispatcher

    public static string ToSourceText(IEnumerable<SyntaxToken> tokens)
    {
        ArgumentNullException.ThrowIfNull(tokens);
        var source = new StringBuilder();
        foreach (var token in tokens)
        {
            source.Append(token.GetSourceString());
        }
        return source.ToString();
    }

    #endregion

}