﻿using Kingsland.MofParser.Lexing;
using System;
using System.Collections.Generic;

namespace Kingsland.MofParser.Tokens
{

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    ///
    /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
    ///
    /// A.17.3 String values
    ///
    /// Unless explicitly specified via ABNF rule WS, no whitespace is allowed between the elements of the rules
    /// in this ABNF section.
    ///
    ///     stringValue   = DOUBLEQUOTE *stringChar DOUBLEQUOTE
    ///                     *( *WS DOUBLEQUOTE *stringChar DOUBLEQUOTE )
    ///     stringChar    = stringUCSchar / stringEscapeSequence
    ///     stringUCSchar = U+0020...U+0021 / U+0023...U+D7FF / 1092 U+E000...U+FFFD / U+10000...U+10FFFF
    ///                     ; Note that these UCS characters can be
    ///                     ; represented in XML without any escaping
    ///                     ; (see W3C XML).
    ///
    ///     stringEscapeSequence = BACKSLASH ( BACKSLASH / DOUBLEQUOTE / SINGLEQUOTE /
    ///                                        BACKSPACE_ESC / TAB_ESC / LINEFEED_ESC /
    ///                                        FORMFEED_ESC / CARRIAGERETURN_ESC /
    ///                                        escapedUCSchar )
    ///
    ///     BACKSPACE_ESC      = "b" ; escape for back space(U+0008)
    ///     TAB_ESC            = "t" ; escape for horizontal tab(U+0009)
    ///     LINEFEED_ESC       = "n" ; escape for line feed(U+000A)
    ///     FORMFEED_ESC       = "f" ; escape for form feed(U+000C)
    ///     CARRIAGERETURN_ESC = "r" ; escape for carriage return (U+000D)
    ///     escapedUCSchar     = ( "x" / "X" ) 1*6( hexDigit ) ; escaped UCS
    ///                          ; character with a UCS code position that is
    ///                          ; the numeric value of the hex number
    ///
    /// </remarks>
    public sealed class StringLiteralToken : Token
    {

        internal StringLiteralToken(SourceExtent extent, string value)
            : base(extent)
        {
            this.Value = value;
        }

        public string Value
        {
            get;
            private set;
        }

        internal static StringLiteralToken Read(ILexerStream stream)
        {
            // BUGBUG - no support for *( *WS DOUBLEQUOTE *stringChar DOUBLEQUOTE )
            // BUGBUG - incomplete escape sequences
            // BUGBUG - no support for UCS characters
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the first character
            sourceChars.Add(stream.ReadChar('"'));
            // read the remaining characters
            var parser = new StringParser();
            while (!stream.Eof)
            {
                var peek = stream.Peek();
                sourceChars.Add(peek);
                if ((peek == '"') && !parser.IsEscaped)
                {
                    parser.ConsumeEof();
                    break;
                }
                else
                {
                    parser.ConsumeChar(stream.Read());
                }
            }
            // read the last character
            sourceChars.Add(stream.ReadChar('"'));
            // process any escape sequences in the string
            var unescaped = parser.OutputString.ToString();
            // return the result
            extent = extent.WithText(sourceChars).WithEndExtent(stream);
            return new StringLiteralToken(extent, unescaped);
        }

        private class StringParser
        {

            private readonly Dictionary<char, char> _escapeMap = new Dictionary<char, char>()
            {
                { '\\' , '\\' }, { '\"' , '\"' }, {'r', '\r'}, {'n', '\n'}
            } ;

            private System.Text.StringBuilder _outputString;

            public bool IsEscaped
            {
                get; 
                set;
            }

            public System.Text.StringBuilder OutputString
            {
                get
                {
                    if (_outputString == null)
                    {
                        _outputString = new System.Text.StringBuilder();
                    }
                    return _outputString;
                }

            }

            public void ConsumeChar(char value)
            {
                if (this.IsEscaped)
                {
                    if (_escapeMap.ContainsKey(value))
                    {
                        this.OutputString.Append(_escapeMap[value]);
                        this.IsEscaped = false;
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
                else if (value == '\\')
                {
                    this.IsEscaped = true;
                }
                else
                {
                    this.OutputString.Append(value);
                }
            }

            public void ConsumeEof()
            {
                if (this.IsEscaped)
                {
                    throw new InvalidOperationException("Incomplete escape sequence found.");
                }
            }

        }

    }

}
