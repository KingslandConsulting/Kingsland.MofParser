using Kingsland.MofParser.Lexing;
using System.Collections.Generic;

namespace Kingsland.MofParser.Tokens
{

    public sealed class EqualsOperatorToken : Token
    {

        internal EqualsOperatorToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static EqualsOperatorToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the character
            sourceChars.Add(stream.ReadChar('=').Value);
            // return the result
            extent = extent.WithText(sourceChars).WithEndExtent(stream);
            return new EqualsOperatorToken(extent);
        }

    }

}
