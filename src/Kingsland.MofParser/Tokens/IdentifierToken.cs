using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IdentifierToken : Token
    {

        internal IdentifierToken(SourceExtent extent, string name)
            : base(extent)
        {
            this.Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        internal static IdentifierToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the name
            var nameChars = new List<char>();
            while (!stream.Eof)
            {
                extent = extent.WithEndExtent(stream);
                var peek = stream.Peek();
                if (char.IsLetterOrDigit(peek) || ("_".IndexOf(peek) != -1))
                {
                    var @char = stream.Read();
                    sourceChars.Add(@char);
                    nameChars.Add(@char);
                }
                else
                {
                    break;
                }
            }
            // return the result
            extent = extent.WithText(sourceChars).WithEndExtent(stream);
            var name = new string(nameChars.ToArray());
            return new IdentifierToken(extent, name);
        }

    }

}
