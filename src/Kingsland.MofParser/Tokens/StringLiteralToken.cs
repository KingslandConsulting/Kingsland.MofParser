using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class StringLiteralToken : SyntaxToken
    {

        #region Constructors

        public StringLiteralToken(string value)
            : this(SourceExtent.Empty, value)
        {
        }

        public StringLiteralToken(SourcePosition start, SourcePosition end, string text, string value)
            : this(new SourceExtent(start, end, text), value)
        {
        }

        public StringLiteralToken(SourceExtent extent, string value)
            : base(extent)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public string Value
        {
            get;
            private set;
        }

        #endregion

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return (this.Extent != SourceExtent.Empty) ?
                this.Extent.Text :
                $"\"{StringLiteralToken.EscapeSourceString(this.Value)}\"";
        }

        #endregion

        #region Helpers

        private readonly static Dictionary<char, string> EscapeMap = new Dictionary<char, string>()
            {
                { '\\', $"{Constants.BACKSLASH}{Constants.BACKSLASH}" },
                { '\"', $"{Constants.BACKSLASH}{Constants.DOUBLEQUOTE}" },
                { '\'', $"{Constants.BACKSLASH}{Constants.SINGLEQUOTE}" },
                { '\b', $"{Constants.BACKSLASH}{Constants.BACKSPACE_ESC}" },
                { '\t', $"{Constants.BACKSLASH}{Constants.TAB_ESC}" },
                { '\n', $"{Constants.BACKSLASH}{Constants.LINEFEED_ESC}" },
                { '\f', $"{Constants.BACKSLASH}{Constants.FORMFEED_ESC}" },
                { '\r', $"{Constants.BACKSLASH}{Constants.CARRIAGERETURN_ESC}" }
            };

        public static string EscapeString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var escapedString = new StringBuilder();
            foreach (var @char in value.ToCharArray())
            {
                if (StringLiteralToken.EscapeMap.TryGetValue(@char, out var escapedChar))
                {
                    // escape sequence
                    escapedString.Append(escapedChar);
                }
                else if ((@char >= 32) && (@char <= 126))
                {
                    // printable characters ' ' - '~'
                    escapedString.Append(@char);
                }
                else
                {
                    throw new InvalidOperationException(new string(new char[] { @char }));
                }
            }
            return escapedString.ToString();
        }

        #endregion

    }

}
