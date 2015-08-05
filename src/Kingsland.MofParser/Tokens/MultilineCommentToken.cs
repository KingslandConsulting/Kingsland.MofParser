using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class MultilineCommentToken : Token
    {

        internal MultilineCommentToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static MultilineCommentToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the opening sequence
            sourceChars.Add(stream.ReadChar('/'));
            sourceChars.Add(stream.ReadChar('*'));
            // read the comment text
            while (!stream.Eof)
            {
                var c = stream.Read();
                sourceChars.Add(c);

                if (c == '*' && stream.PeekChar('/'))
                    break;
            }
            // read the closing  sequence
            sourceChars.Add(stream.ReadChar('/'));
            // return the result
            extent = extent.WithText(sourceChars);
            return new MultilineCommentToken(extent);
        }

    }

}
