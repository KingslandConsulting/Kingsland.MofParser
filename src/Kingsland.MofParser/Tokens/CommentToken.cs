using Kingsland.MofParser.Lexing;
using System;
using System.Collections.Generic;

namespace Kingsland.MofParser.Tokens
{

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    ///
    /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
    ///
    /// 5.4 Comments
    /// Comments in a MOF file do not create, modify, or annotate language elements. They shall be treated as if
    /// they were whitespace.
    ///
    /// Comments may appear anywhere in MOF syntax where whitespace is allowed and are indicated by either
    /// a leading double slash( // ) or a pair of matching /* and */ character sequences. Occurrences of these
    /// character sequences in string literals shall not be treated as comments.
    ///
    /// A // comment is terminated by the end of line (see 5.3), as shown in the example below.
    ///
    ///     uint16 MyProperty; // This is an example of a single-line comment
    ///
    /// A comment that begins with /* is terminated by the next */ sequence, or by the end of the MOF file,
    /// whichever comes first.
    ///
    ///     /* example of a comment between property definition tokens and a multi-line comment */
    ///     uint16 /* 16-bit integer property */ MyProperty; /* and a multi-line
    ///                             comment */
    ///
    /// </remarks>
    public sealed class CommentToken : Token
    {

        internal CommentToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static CommentToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            sourceChars.Add(stream.ReadChar('/'));
            switch(stream.Peek())
            {
                case '/': // single-line
                    sourceChars.Add(stream.ReadChar('/'));
                    // read the comment text
                    while (!stream.Eof && !WhitespaceToken.IsLineTerminator(stream.Peek()))
                    {
                        sourceChars.Add(stream.Read());
                    };
                    break;
                case '*': // multi-line
                    sourceChars.Add(stream.ReadChar('*'));
                    // read the comment text
                    while (!stream.Eof)
                    {
                        var @char = stream.Read();
                        sourceChars.Add(@char);
                        if ((@char == '*') && stream.PeekChar('/'))
                        {
                            // read the closing sequence
                            sourceChars.Add(stream.ReadChar('/'));
                            break;
                        }
                    }
                    break;
                default:
                    throw new InvalidOperationException(
                        string.Format("Unexpected character '{0}'.", stream.Peek()));
            }
            // return the result
            extent = extent.WithText(sourceChars).WithEndExtent(stream);
            return new CommentToken(extent);
        }

    }

}
