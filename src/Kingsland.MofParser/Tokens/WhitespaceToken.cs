using System.Collections.Generic;
using Kingsland.MofParser.Lexing;
using System;

namespace Kingsland.MofParser.Tokens
{

    public sealed class WhitespaceToken : Token
    {

        #region Constructors

        internal WhitespaceToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

        internal static readonly char[] WhitespaceChars = new[] { ' ', '\t', '\r', '\n' };

        internal static bool IsWhitespace(char @char)
        {
            return (Array.IndexOf(WhitespaceToken.WhitespaceChars, @char) >= 0);
        }

        internal static readonly char[] LineTerminatorChars = new[] { '\r', '\n' };

        internal static bool IsLineTerminator(char @char)
        {
            return (Array.IndexOf(WhitespaceToken.LineTerminatorChars, @char) >= 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// 
        /// 5.2 - Whitespace
        ///
        /// Whitespace in a MOF file is any combination of the following characters:
        ///
        ///     Space (U+0020),
        ///     Horizontal Tab (U+0009),
        ///     Carriage Return (U+000D) and
        ///     Line Feed (U+000A).
        ///
        /// The WS ABNF rule represents any one of these whitespace characters:
        ///
        ///     WS = U+0020 / U+0009 / U+000D / U+000A
        ///
        /// </remarks>
        internal static WhitespaceToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the first whitespace character
            sourceChars.Add(stream.ReadWhitespace());
            // read the remaining whitespace
            while (!stream.Eof && WhitespaceToken.IsWhitespace(stream.Peek()))
            {
                sourceChars.Add(stream.Read());
            }
            // return the result
            extent = extent.WithText(sourceChars).WithEndExtent(stream);
            return new WhitespaceToken(extent);
        }

    }

}
