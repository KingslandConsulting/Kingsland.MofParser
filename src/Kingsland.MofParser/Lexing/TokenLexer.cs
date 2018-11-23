using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Source;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingsland.MofParser.Lexing
{

    public sealed class TokenLexer
    {

        #region Constructors

        public TokenLexer(SourceReader reader)
        {
            this.Reader = reader ??
                throw new ArgumentNullException(nameof(reader));
        }

        #endregion

        #region Properties

        private SourceReader Reader
        {
            get;
            set;
        }

        public bool Eof
        {
            get
            {
                return this.Reader.Eof();
            }
        }

        #endregion

        #region Lexing Methods

        public static List<Token> Lex(SourceReader reader)
        {
            var lexer = new TokenLexer(reader);
            return lexer.AllTokens().ToList();
        }

        public IEnumerable<Token> AllTokens()
        {
            var lexTokens = new List<Token>();
            while (!this.Reader.Eof())
            {
                yield return this.NextToken();
            }
        }

        public Token NextToken()
        {
            var reader = this.Reader;
            var peek = reader.Peek();
            switch (peek.Value)
            {
                case '$':
                    {
                        var (aliasIdentifierToken, nextReader) = TokenLexer.ReadAliasIdentifierToken(reader);
                        this.Reader = nextReader;
                        return aliasIdentifierToken;
                    }
                case ']':
                    {
                        var (attributeClosetoken, nextReader) = TokenLexer.ReadAttributeCloseToken(reader);
                        this.Reader = nextReader;
                        return attributeClosetoken;
                    }
                case '[':
                    {
                        var (attributeOpenToken, nextReader) = TokenLexer.ReadAttributeOpenToken(reader);
                        this.Reader = nextReader;
                        return attributeOpenToken;
                    }
                case '}':
                    {
                        var (blockCloseToken, nextReader) = TokenLexer.ReadBlockCloseToken(reader);
                        this.Reader = nextReader;
                        return blockCloseToken;
                    }
                case '{':
                    {
                        var (blockOpenToken, nextReader) = TokenLexer.ReadBlockOpenToken(reader);
                        this.Reader = nextReader;
                        return blockOpenToken;
                    }
                case ':':
                    {
                        var (colonToken, nextReader) = TokenLexer.ReadColonToken(reader);
                        this.Reader = nextReader;
                        return colonToken;
                    }
                case ',':
                    {
                        var (commaToken, nextReader) = TokenLexer.ReadCommaToken(reader);
                        this.Reader = nextReader;
                        return commaToken;
                    }
                case '/':
                    {
                        var (forwardSlashToken, nextReader) = TokenLexer.ReadCommentToken(reader);
                        this.Reader = nextReader;
                        return forwardSlashToken;
                    }
                case '=':
                    {
                        var (equalsToken, nextReader) = TokenLexer.ReadEqualsOperatorToken(reader);
                        this.Reader = nextReader;
                        return equalsToken;
                    }
                case ')':
                    {
                        var (parenthesesCloseToken, nextReader) = TokenLexer.ReadParenthesesCloseToken(reader);
                        this.Reader = nextReader;
                        return parenthesesCloseToken;
                    }
                case '(':
                    {
                        var (parenthesesOpenToken, nextReader) = TokenLexer.ReadParenthesesOpenToken(reader);
                        this.Reader = nextReader;
                        return parenthesesOpenToken;
                    }
                case '#':
                    {
                        var (pragmaToken, nextReader) = TokenLexer.ReadPragmaToken(reader);
                        this.Reader = nextReader;
                        return pragmaToken;
                    }
                case ';':
                    {
                        var (statementEndToken, nextReader) = TokenLexer.ReadStatementEndToken(reader);
                        this.Reader = nextReader;
                        return statementEndToken;
                    }
                case '"':
                    {
                        var (stringLiteralToken, nextReader) = TokenLexer.ReadStringLiteralToken(reader);
                        this.Reader = nextReader;
                        return stringLiteralToken;
                    }
                default:
                    if (StringValidator.IsWhitespace(peek.Value))
                    {
                        var (whitespaceToken, nextReader) = TokenLexer.ReadWhitespaceToken(reader);
                        this.Reader = nextReader;
                        return whitespaceToken;
                    }
                    else if (StringValidator.IsFirstIdentifierChar(peek.Value))
                    {
                        var (identifierToken, nextReader) = TokenLexer.ReadIdentifierToken(reader);
                        this.Reader = nextReader;
                        if (StringValidator.IsFalse(identifierToken.Name))
                        {
                            /// A.17.6 Boolean value
                            ///
                            ///     booleanValue = TRUE / FALSE
                            ///
                            ///     FALSE        = "false" ; keyword: case insensitive
                            ///     TRUE         = "true"  ; keyword: case insensitive
                            return new BooleanLiteralToken(identifierToken.Extent, false);
                        }
                        else if (StringValidator.IsTrue(identifierToken.Name))
                        {
                            /// A.17.6 Boolean value
                            ///
                            ///     booleanValue = TRUE / FALSE
                            ///
                            ///     FALSE        = "false" ; keyword: case insensitive
                            ///     TRUE         = "true"  ; keyword: case insensitive
                            return new BooleanLiteralToken(identifierToken.Extent, true);
                        }
                        else if (StringValidator.IsNull(identifierToken.Name))
                        {
                            /// A.17.7 Null value
                            ///
                            ///     nullValue = NULL
                            ///
                            ///     NULL = "null" ; keyword: case insensitive
                            ///                   ; second
                            return new NullLiteralToken(identifierToken.Extent);
                        }
                        else
                        {
                            return identifierToken;
                        }
                    }
                    else if ((peek.Value == '+') || (peek.Value == '-') ||
                             (StringValidator.IsDecimalDigit(peek.Value)))
                    {
                        var (integerToken, nextReader) = TokenLexer.ReadIntegerLiteralToken(reader);
                        this.Reader = nextReader;
                        return integerToken;
                    }
                    else
                    {
                        throw new UnexpectedCharacterException(peek);
                    }
            }
        }

        #endregion

        #region Token Methods

        #region Symbols

        private static (AttributeCloseToken, SourceReader) ReadAttributeCloseToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(']');
            var extent = SourceExtent.From(sourceChar);
            return (new AttributeCloseToken(extent), nextReader);
        }

        private static (AttributeOpenToken, SourceReader) ReadAttributeOpenToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('[');
            var extent = SourceExtent.From(sourceChar);
            return (new AttributeOpenToken(extent), nextReader);
        }

        private static (BlockCloseToken, SourceReader) ReadBlockCloseToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('}');
            var extent = SourceExtent.From(sourceChar);
            return (new BlockCloseToken(extent), nextReader);
        }

        private static (BlockOpenToken, SourceReader) ReadBlockOpenToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('{');
            var extent = SourceExtent.From(sourceChar);
            return (new BlockOpenToken(extent), nextReader);
        }

        private static (ColonToken, SourceReader) ReadColonToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(':');
            var extent = SourceExtent.From(sourceChar);
            return (new ColonToken(extent), nextReader);
        }

        private static (CommaToken, SourceReader) ReadCommaToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(',');
            var extent = SourceExtent.From(sourceChar);
            return (new CommaToken(extent), nextReader);
        }

        private static (EqualsOperatorToken, SourceReader) ReadEqualsOperatorToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('=');
            var extent = SourceExtent.From(sourceChar);
            return (new EqualsOperatorToken(extent), nextReader);
        }

        private static (ParenthesesCloseToken, SourceReader) ReadParenthesesCloseToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(')');
            var extent = SourceExtent.From(sourceChar);
            return (new ParenthesesCloseToken(extent), nextReader);
        }

        private static (ParenthesesOpenToken, SourceReader) ReadParenthesesOpenToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('(');
            var extent = SourceExtent.From(sourceChar);
            return (new ParenthesesOpenToken(extent), nextReader);
        }

        private static (StatementEndToken, SourceReader) ReadStatementEndToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(';');
            var extent = SourceExtent.From(sourceChar);
            return (new StatementEndToken(extent), nextReader);
        }

        #endregion

        #region 5.2 Whitespace

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        ///
        /// 5.2 - Whitespace
        ///
        /// Whitespace in a MOF file is any combination of the following characters:
        ///
        ///     Space (U+0020),
        ///     Horizontal Tab (U+0009),
        ///     Carriage Return (U+000D) and
        ///     Line Feed (U+000A).
        ///
        /// The WS ABNF rule represents any one of these whitespace characters:
        ///
        ///     WS = U+0020 / U+0009 / U+000D / U+000A
        ///
        /// </remarks>
        private static (WhitespaceToken, SourceReader) ReadWhitespaceToken(SourceReader reader)
        {
            var thisReader = reader;
            var sourceChar = default(SourceChar);
            var sourceChars = new List<SourceChar>();
            // read the first whitespace character
            (sourceChar, thisReader) = thisReader.Read(StringValidator.IsWhitespace);
            sourceChars.Add(sourceChar);
            // read the remaining whitespace
            while (!thisReader.Eof() && thisReader.Peek(StringValidator.IsWhitespace))
            {
                (sourceChar, thisReader) = thisReader.Read();
                sourceChars.Add(sourceChar);
            }
            // return the result
            var extent = SourceExtent.From(sourceChars);
            return (new WhitespaceToken(extent), thisReader);
        }

        #endregion

        #region 5.4 Comments

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        ///
        /// 5.4 Comments
        /// Comments in a MOF file do not create, modify, or annotate language elements. They shall be treated as if
        /// they were whitespace.
        ///
        /// Comments may appear anywhere in MOF syntax where whitespace is allowed and are indicated by either
        /// a leading double slash( // ) or a pair of matching /* and */ character sequences. Occurrences of these
        /// character sequences in string literals shall not be treated as comments.
        ///
        /// A // comment is terminated by the end of line (see 5.3), as shown in the example below.
        ///
        ///     uint16 MyProperty; // This is an example of a single-line comment
        ///
        /// A comment that begins with /* is terminated by the next */ sequence, or by the end of the MOF file,
        /// whichever comes first.
        ///
        ///     /* example of a comment between property definition tokens and a multi-line comment */
        ///     uint16 /* 16-bit integer property */ MyProperty; /* and a multi-line
        ///                             comment */
        ///
        /// </remarks>
        private static (CommentToken, SourceReader) ReadCommentToken(SourceReader reader)
        {
            var thisReader = reader;
            var sourceChar = default(SourceChar);
            var sourceChars = new List<SourceChar>();
            // read the starting '/'
            (sourceChar, thisReader) = thisReader.Read('/');
            sourceChars.Add(sourceChar);
            switch (thisReader.Peek().Value)
            {
                case '/': // single-line
                    (sourceChar, thisReader) = thisReader.Read('/');
                    sourceChars.Add(sourceChar);
                    // read the comment text
                    while (!thisReader.Eof() && !thisReader.Peek(StringValidator.IsLineTerminator))
                    {
                        (sourceChar, thisReader) = thisReader.Read();
                        sourceChars.Add(sourceChar);
                    };
                    break;
                case '*': // multi-line
                    (sourceChar, thisReader) = thisReader.Read('*');
                    sourceChars.Add(sourceChar);
                    // read the comment text
                    while (!thisReader.Eof())
                    {
                        (sourceChar, thisReader) = thisReader.Read();
                        sourceChars.Add(sourceChar);
                        if ((sourceChar.Value == '*') && thisReader.Peek('/'))
                        {
                            // read the closing sequence
                            (sourceChar, thisReader) = thisReader.Read('/');
                            sourceChars.Add(sourceChar);
                            break;
                        }
                    }
                    break;
                default:
                    throw new UnexpectedCharacterException(reader.Peek());
            }
            // return the result
            var extent = SourceExtent.From(sourceChars);
            return (new CommentToken(extent), thisReader);
        }

        #endregion

        #region A.3 Compiler directive

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        ///
        /// A.3 Compiler directive
        ///
        /// compilerDirective = PRAGMA ( pragmaName / standardPragmaName )
        ///                     "(" pragmaParameter ")"
        ///
        /// pragmaName         = IDENTIFIER
        /// standardPragmaName = INCLUDE
        /// pragmaParameter    = stringValue ; if the pragma is INCLUDE,
        ///                                  ; the parameter value
        ///                                  ; shall represent a relative
        ///                                  ; or full file path
        /// PRAGMA             = "#pragma"  ; keyword: case insensitive
        /// INCLUDE            = "include"  ; keyword: case insensitive
        ///
        /// </remarks>
        private static (PragmaToken, SourceReader) ReadPragmaToken(SourceReader reader)
        {
            (var sourceChars, var thisReader) = reader.ReadString(Keywords.PRAGMA);
            var extent = SourceExtent.From(sourceChars.ToList());
            return (new PragmaToken(extent), thisReader);
        }

        #endregion

        #region A.13 Names

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
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
        private static (IdentifierToken, SourceReader) ReadIdentifierToken(SourceReader reader)
        {
            var thisReader = reader;
            var sourceChar = default(SourceChar);
            var sourceChars = new List<SourceChar>();
            var nameChars = new StringBuilder();
            // firstIdentifierChar
            (sourceChar, thisReader) = thisReader.Read(StringValidator.IsFirstIdentifierChar);
            sourceChars.Add(sourceChar);
            nameChars.Append(sourceChar.Value);
            // *( nextIdentifierChar )
            while (!thisReader.Eof() && thisReader.Peek(StringValidator.IsNextIdentifierChar))
            {
                (sourceChar, thisReader) = thisReader.Read();
                sourceChars.Add(sourceChar);
                nameChars.Append(sourceChar.Value);
            }
            // return the result
            var extent = SourceExtent.From(sourceChars);
            var name = nameChars.ToString();
            return (new IdentifierToken(extent, name), thisReader);
        }

        #endregion

        #region A.13.2 Alias identifier

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
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
        private static (AliasIdentifierToken, SourceReader) ReadAliasIdentifierToken(SourceReader reader)
        {
            var thisReader = reader;
            var sourceChar = default(SourceChar);
            var sourceChars = new List<SourceChar>();
            var nameChars = new StringBuilder();
            // read the first character
            (sourceChar, thisReader) = thisReader.Read('$');
            sourceChars.Add(sourceChar);
            // firstIdentifierChar
            (sourceChar, thisReader) = thisReader.Read(StringValidator.IsFirstIdentifierChar);
            sourceChars.Add(sourceChar);
            nameChars.Append(sourceChar.Value);
            // *( nextIdentifierChar )
            while (!thisReader.Eof() && thisReader.Peek(StringValidator.IsNextIdentifierChar))
            {
                (sourceChar, thisReader) = thisReader.Read();
                sourceChars.Add(sourceChar);
                nameChars.Append(sourceChar.Value);
            }
            // return the result
            var extent = SourceExtent.From(sourceChars);
            var name = nameChars.ToString();
            return (new AliasIdentifierToken(extent, name), thisReader);
        }

        #endregion

        #region A.17.1 Integer value

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        ///
        /// A.17.1 Integer value
        ///
        /// No whitespace is allowed between the elements of the rules in this ABNF section.
        ///
        ///     integerValue         = binaryValue / octalValue / hexValue / decimalValue
        ///
        ///     binaryValue          = [ "+" / "-" ] 1*binaryDigit ( "b" / "B" )
        ///     binaryDigit          = "0" / "1"
        ///
        ///     octalValue           = [ "+" / "-" ] unsignedOctalValue
        ///     unsignedOctalValue   = "0" 1*octalDigit
        ///     octalDigit           = "0" / "1" / "2" / "3" / "4" / "5" / "6" / "7"
        ///
        ///     hexValue             = [ "+" / "-" ] ( "0x" / "0X" ) 1*hexDigit
        ///     hexDigit             = decimalDigit / "a" / "A" / "b" / "B" / "c" / "C" /
        ///                                           "d" / "D" / "e" / "E" / "f" / "F"
        ///
        ///     decimalValue         = [ "+" / "-" ] unsignedDecimalValue
        ///     unsignedDecimalValue = positiveDecimalDigit *decimalDigit
        ///     decimalDigit         = "0" / positiveDecimalDigit
        ///     positiveDecimalDigit = "1"..."9"
        ///
        /// </remarks>
        private static (IntegerLiteralToken, SourceReader) ReadIntegerLiteralToken(SourceReader reader)
        {
            /// BUGBUG - this method is woefully underimplemented!
            var thisReader = reader;
            var sourceChar = default(SourceChar);
            var sourceChars = new List<SourceChar>();
            // read the sign (if there is one)
            var sign = default(SourceChar);
            var peek = thisReader.Peek();
            switch (peek.Value)
            {
                case '+':
                case '-':
                    (sign, thisReader) = thisReader.Read();
                    sourceChars.Add(sign);
                    break;
            }
            // read the remaining characters
            // BUGBUG - only handles decimalValue
            (sourceChar, thisReader) = thisReader.Read(StringValidator.IsDecimalDigit);
            sourceChars.Add(sourceChar);
            while (!thisReader.Eof() && thisReader.Peek(StringValidator.IsDecimalDigit))
            {
                (sourceChar, thisReader) = thisReader.Read();
                sourceChars.Add(sourceChar);
            }
            // return the result
            var extent = SourceExtent.From(sourceChars);
            return (new IntegerLiteralToken(extent, long.Parse(extent.Text)), thisReader);
        }

        #endregion

        #region A.17.3 String values

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
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
        ///
        /// </remarks>
        private static (StringLiteralToken, SourceReader) ReadStringLiteralToken(SourceReader reader)
        {
            // BUGBUG - no support for *( *WS DOUBLEQUOTE *stringChar DOUBLEQUOTE )
            // BUGBUG - incomplete escape sequences
            // BUGBUG - no support for UCS characters
            var thisReader = reader;
            var sourceChar = default(SourceChar);
            var sourceChars = new List<SourceChar>();
            var stringChars = new StringBuilder();
            // read the first double-quote character
            (sourceChar, thisReader) = thisReader.Read('"');
            sourceChars.Add(sourceChar);
            // read the remaining characters
            bool isEscaped = false;
            bool isTerminated = false;
            while (!thisReader.Eof())
            {
                var peek = thisReader.Peek();
                if (isEscaped)
                {
                    // read the second character in an escape sequence
                    var escapedChar = default(char);
                    switch (peek.Value)
                    {
                        case '\\':
                            escapedChar = '\\';
                            break;
                        case '\"':
                            escapedChar = '\"';
                            break;
                        case '\'':
                            escapedChar = '\'';
                            break;
                        case 'b':
                            escapedChar = '\b';
                            break;
                        case 't':
                            escapedChar = '\t';
                            break;
                        case 'n':
                            escapedChar = '\n';
                            break;
                        case 'f':
                            escapedChar = '\f';
                            break;
                        case 'r':
                            escapedChar = '\r';
                            break;
                        default:
                            throw new UnexpectedCharacterException(peek);
                    }
                    thisReader = thisReader.Next();
                    sourceChars.Add(peek);
                    stringChars.Append(escapedChar);
                    isEscaped = false;
                }
                else if (peek.Value == '\\')
                {
                    // read the first character in an escape sequence
                    thisReader = thisReader.Next();
                    sourceChars.Add(peek);
                    isEscaped = true;
                }
                else if (peek.Value == '\"')
                {
                    // read the last double-quote character
                    (_, thisReader) = thisReader.Read('"');
                    sourceChars.Add(peek);
                    isTerminated = true;
                    break;
                }
                else
                {
                    // read a literal string character
                    thisReader = thisReader.Next();
                    sourceChars.Add(peek);
                    stringChars.Append(peek.Value);
                }
            }
            // make sure we found the end of the string
            if (!isTerminated)
            {
                throw new InvalidOperationException("Unterminated string found.");
            }
            // return the result
            var extent = SourceExtent.From(sourceChars);
            var stringValue = stringChars.ToString();
            return (new StringLiteralToken(extent, stringValue), thisReader);
        }

        #endregion

        #endregion

    }

}