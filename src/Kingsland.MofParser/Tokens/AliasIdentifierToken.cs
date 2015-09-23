﻿using Kingsland.MofParser.Lexing;
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
    /// A.13.2 Alias identifier
    ///
    /// No whitespace is allowed between the elements of this rule.
    ///
    ///     aliasIdentifier = "$" IDENTIFIER
    ///
    ///     IDENTIFIER          = firstIdentifierChar *( nextIdentifierChar )
    ///     firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE
    ///     nextIdentifierChar  = firstIdentifierChar / decimalDigit
    ///     elementName         = localName / schemaQualifiedName
    ///     localName           = IDENTIFIER
    ///
    /// </remarks>
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
            var sourceChars = new List<SourceChar>();
            var nameChars = new List<char>();
            // read the first character
            sourceChars.Add(stream.ReadChar('$'));
            // firstIdentifierChar
            var peek = stream.Peek();
            if (!StringValidator.IsFirstIdentifierChar(peek.Value))
            {
                throw new InvalidOperationException(
                    string.Format("Unexpected character '{0}' encountered", peek.Value));
            }
            // *( nextIdentifierChar )
            while (!stream.Eof)
            {
                peek = stream.Peek();
                if (StringValidator.IsNextIdentifierChar(peek.Value))
                {
                    var @char = stream.Read();
                    sourceChars.Add(@char);
                    nameChars.Add(@char.Value);
                }
                else
                {
                    break;
                }
            }
            // return the result
            var extent = new SourceExtent(sourceChars);
            var name = new string(nameChars.ToArray());
            return new AliasIdentifierToken(extent, name);
        }

    }

}
