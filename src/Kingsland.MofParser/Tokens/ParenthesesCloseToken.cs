using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class ParenthesesCloseToken : Token
    {

        internal ParenthesesCloseToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static ParenthesesCloseToken Read(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            // read the character
            sourceChars.Add(stream.ReadChar(')'));
            // return the result
            var extent = new SourceExtent(sourceChars);
            return new ParenthesesCloseToken(extent);
        }

    }

}
