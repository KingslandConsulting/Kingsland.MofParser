using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BlockOpenToken : Token
    {

        internal BlockOpenToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static BlockOpenToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the character
            sourceChars.Add(stream.ReadChar('{'));
            // return the result
            extent = extent.WithText(sourceChars).WithEndExtent(stream);
            return new BlockOpenToken(extent);
        }

    }

}
