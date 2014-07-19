using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class AliasIdentifierToken : Token
    {

        internal AliasIdentifierToken(SourceExtent extent, string name)
            : base(extent)
        {
            this.Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        internal static AliasIdentifierToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the first character
            sourceChars.Add(stream.ReadChar('$'));
            // read the name
            var nameChars = new List<char>();
            while (!stream.Eof)
            {
                extent = extent.WithEndExtent(stream);
                var peek = stream.Peek();
                if (char.IsLetterOrDigit(peek) || ("_".IndexOf(peek) != -1) )
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
            extent = extent.WithText(sourceChars);
            var name = new string(nameChars.ToArray());
            return new AliasIdentifierToken(extent, name);
        }

    }

}
