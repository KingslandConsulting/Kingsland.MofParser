using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class StringLiteralToken : SyntaxToken
    {

        public StringLiteralToken(SourceExtent extent, string value)
            : base(extent)
        {
            this.Value = value;
        }

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
            return this?.Extent.Text ?? StringLiteralToken.EscapeString(this.Value);
        }

        #endregion

        #region Helpers

        private readonly static Dictionary<char, string> escapeMap = new Dictionary<char, string>()
            {
                { '\\' , "\\\\" }, { '\"' , "\\\"" },  { '\'' , "\\\'" },
                { '\b', "\\b" }, { '\t', "\\t" },  { '\n', "\\n" }, { '\f', "\\f" }, { '\r', "\\r" }
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
                if ((@char >= 32) && (@char <= 126))
                {
                    // printable characters ' ' - '~'
                    escapedString.Append(@char);
                }
                else if (StringLiteralToken.escapeMap.TryGetValue(@char, out var escapedChar))
                {
                    // escape sequence
                    escapedString.Append(escapedChar);
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
