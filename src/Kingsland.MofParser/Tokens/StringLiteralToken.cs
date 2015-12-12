using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

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

    }

}
