using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
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
    /// A.13 Names
    ///
    /// MOF names are identifiers with the format defined by the IDENTIFIER rule.
    /// No whitespace is allowed between the elements of the rules in this ABNF section.
    ///
    ///     IDENTIFIER          = firstIdentifierChar *( nextIdentifierChar )
    ///     firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE
    ///     nextIdentifierChar  = firstIdentifierChar / decimalDigit
    ///     elementName         = localName / schemaQualifiedName
    ///     localName           = IDENTIFIER
    ///
    /// </remarks>
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
            var nameChars = new List<char>();
            // firstIdentifierChar
            var peek = stream.Peek();
            if(!StringValidator.IsFirstIdentifierChar(peek))
            {
                throw new InvalidOperationException(
                    string.Format("Unexpected character '{0}' encountered", peek));
            }
            // *( nextIdentifierChar )
            while (!stream.Eof)
            {
                extent = extent.WithEndExtent(stream);
                peek = stream.Peek();
                if (StringValidator.IsNextIdentifierChar(peek))
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
