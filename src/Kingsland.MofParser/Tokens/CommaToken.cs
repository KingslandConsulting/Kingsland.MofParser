using Kingsland.MofParser.Lexing;
using System.Collections.Generic;

namespace Kingsland.MofParser.Tokens
{

    public sealed class CommaToken : Token
    {

        internal CommaToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static CommaToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the character
            sourceChars.Add(stream.ReadChar(',').Value);
            // return the result
            extent = extent.WithText(sourceChars).WithEndExtent(stream);
            return new CommaToken(extent);
        }

    }

}
