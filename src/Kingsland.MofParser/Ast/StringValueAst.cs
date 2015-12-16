using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kingsland.MofParser.Ast
{

    public sealed class StringValueAst : LiteralValueAst
    {

        #region Constructors

        private StringValueAst()
        {
        }

        #endregion

        #region Properties

        public string Value
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

        internal new static StringValueAst Parse(ParserStream stream)
        {
            return new StringValueAst
            {
                Value = stream.Read<StringLiteralToken>().Value
            };
        }

        internal static string EscapeString(string value)
        {
            var escapeMap = new Dictionary<char, char>()
            {
                { '\\' , '\\' }, { '\"' , '\"' },  {'\b', 'b'}, {'\t', 't'},  {'\n', 'n'}, {'\f', 'f'}, {'\r', 'r'}
            };
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var escaped = new StringBuilder();
            foreach(var @char in value.ToCharArray())
            {
                if (escapeMap.ContainsKey(@char))
                {
                    escaped.AppendFormat("\\{0}", escapeMap[@char]);
                }
                else if ((@char >= 32) && (@char <= 126))
                {
                    escaped.Append(@char);
                }
                else
                {
                    throw new InvalidOperationException(new string(new char[] { @char }));
                }
            }
            return escaped.ToString();
        }

        #endregion

        #region AstNode Members

        public override string GetMofSource()
        {
            return string.Format("\"{0}\"", StringValueAst.EscapeString(this.Value));
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return this.GetMofSource();
        }

        #endregion

    }

}
