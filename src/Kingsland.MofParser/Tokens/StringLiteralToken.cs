using System;
using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

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
