using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{
    public sealed class OpenParenthesesToken : Token
    {

        internal OpenParenthesesToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static OpenParenthesesToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the character
            sourceChars.Add(stream.ReadChar('('));
            // return the result
            extent = extent.WithText(sourceChars);
            return new OpenParenthesesToken(extent);
        }

    }
}
