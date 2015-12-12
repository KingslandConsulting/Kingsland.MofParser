using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingsland.MofParser.Lexing
{

    public sealed class Lexer
    {

        #region Constructors

        public Lexer(ILexerStream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            this.Stream = stream;
        }

        #endregion

        #region Properties

        private ILexerStream Stream
        {
            get;
            set;
        }

        public bool Eof
        {
            get
            {
                return this.Stream.Eof;
            }
        }

        #endregion

        #region Lexing Methods

        public static List<Token> Lex(ILexerStream stream)
        {
            var lexer = new Lexer(stream);
            return lexer.AllTokens().ToList();
        }

        public IEnumerable<Token> AllTokens()
        {
            var lexTokens = new List<Token>();
            while (!this.Stream.Eof)
            {
                yield return this.NextToken();
            }
        }

        public Token NextToken()
        {
            var stream = this.Stream;
            var peek = stream.Peek();
            switch (peek.Value)
            {
                case '$':
                    return Lexer.ReadAliasIdentifierToken(stream);
                case ']':
                    return Lexer.ReadAttributeCloseToken(stream);
                case '[':
                    return Lexer.ReadAttributeOpenToken(stream);
                case '}':
                    return Lexer.ReadBlockCloseToken(stream);
                case '{':
                    return Lexer.ReadBlockOpenToken(stream);
                case ':':
                    return Lexer.ReadColonToken(stream);
                case ',':
                    return Lexer.ReadCommaToken(stream);
                case '/':
                    return Lexer.ReadCommentToken(stream);
                case '=':
                    return Lexer.ReadEqualsOperatorToken(stream);
                case ')':
                    return Lexer.ReadParenthesesCloseToken(stream);
                case '(':
                    return Lexer.ReadParenthesesOpenToken(stream);
                case '#':
                    return Lexer.ReadPragmaToken(stream);
                case ';':
                    return Lexer.ReadStatementEndToken(stream);
                case '"':
                    return Lexer.ReadStringLiteralToken(stream);
                default:
                    if (StringValidator.IsWhitespace(peek.Value))
                    {
                        return Lexer.ReadWhitespaceToken(stream);
                    }
                    else if (StringValidator.IsFirstIdentifierChar(peek.Value))
                    {
                        var identifier = Lexer.ReadIdentifierToken(stream);
                        if (StringValidator.IsFalse(identifier.Name))
                        {
                            /// A.17.6 Boolean value
                            ///
                            ///     booleanValue = TRUE / FALSE
                            ///
                            ///     FALSE        = "false" ; keyword: case insensitive
                            ///     TRUE         = "true"  ; keyword: case insensitive
                            return new BooleanLiteralToken(identifier.Extent, false);
                        }
                        else if (StringValidator.IsTrue(identifier.Name))
                        {
                            /// A.17.6 Boolean value
                            ///
                            ///     booleanValue = TRUE / FALSE
                            ///
                            ///     FALSE        = "false" ; keyword: case insensitive
                            ///     TRUE         = "true"  ; keyword: case insensitive
                            return new BooleanLiteralToken(identifier.Extent, true);
                        }
                        else if (StringValidator.IsNull(identifier.Name))
                        {
                            /// A.17.7 Null value
                            ///
                            ///     nullValue = NULL
                            ///
                            ///     NULL = "null" ; keyword: case insensitive
                            ///                   ; second
                            return new NullLiteralToken(identifier.Extent);
                        }
                        else
                        {
                            return identifier;
                        }
                    }
                    else if ((peek.Value == '+') || (peek.Value == '-') ||
                             (StringValidator.IsDecimalDigit(peek.Value)))
                    {
                        return Lexer.ReadIntegerLiteralToken(stream);
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

        private static AttributeCloseToken ReadAttributeCloseToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.Add(stream.ReadChar(']'));
            var extent = new SourceExtent(sourceChars);
            return new AttributeCloseToken(extent);
        }

        private static AttributeOpenToken ReadAttributeOpenToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.Add(stream.ReadChar('['));
            var extent = new SourceExtent(sourceChars);
            return new AttributeOpenToken(extent);
        }

        private static BlockCloseToken ReadBlockCloseToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.Add(stream.ReadChar('}'));
            var extent = new SourceExtent(sourceChars);
            return new BlockCloseToken(extent);
        }

        private static BlockOpenToken ReadBlockOpenToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.Add(stream.ReadChar('{'));
            var extent = new SourceExtent(sourceChars);
            return new BlockOpenToken(extent);
        }

        private static ColonToken ReadColonToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.Add(stream.ReadChar(':'));
            var extent = new SourceExtent(sourceChars);
            return new ColonToken(extent);
        }

        private static CommaToken ReadCommaToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.Add(stream.ReadChar(','));
            var extent = new SourceExtent(sourceChars);
            return new CommaToken(extent);
        }

        private static EqualsOperatorToken ReadEqualsOperatorToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.Add(stream.ReadChar('='));
            var extent = new SourceExtent(sourceChars);
            return new EqualsOperatorToken(extent);
        }

        private static ParenthesesCloseToken ReadParenthesesCloseToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.Add(stream.ReadChar(')'));
            var extent = new SourceExtent(sourceChars);
            return new ParenthesesCloseToken(extent);
        }

        private static ParenthesesOpenToken ReadParenthesesOpenToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.Add(stream.ReadChar('('));
            var extent = new SourceExtent(sourceChars);
            return new ParenthesesOpenToken(extent);
        }

        private static StatementEndToken ReadStatementEndToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.Add(stream.ReadChar(';'));
            var extent = new SourceExtent(sourceChars);
            return new StatementEndToken(extent);
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
        private static WhitespaceToken ReadWhitespaceToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            // read the first whitespace character
            sourceChars.Add(stream.ReadWhitespace());
            // read the remaining whitespace
            while (!stream.Eof && StringValidator.IsWhitespace(stream.Peek().Value))
            {
                sourceChars.Add(stream.Read());
            }
            // return the result
            var extent = new SourceExtent(sourceChars);
            return new WhitespaceToken(extent);
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
        private static CommentToken ReadCommentToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.Add(stream.ReadChar('/'));
            switch (stream.Peek().Value)
            {
                case '/': // single-line
                    sourceChars.Add(stream.ReadChar('/'));
                    // read the comment text
                    while (!stream.Eof && !StringValidator.IsLineTerminator(stream.Peek().Value))
                    {
                        sourceChars.Add(stream.Read());
                    };
                    break;
                case '*': // multi-line
                    sourceChars.Add(stream.ReadChar('*'));
                    // read the comment text
                    while (!stream.Eof)
                    {
                        var @char = stream.Read();
                        sourceChars.Add(@char);
                        if ((@char.Value == '*') && stream.PeekChar('/'))
                        {
                            // read the closing sequence
                            sourceChars.Add(stream.ReadChar('/'));
                            break;
                        }
                    }
                    break;
                default:
                    throw new InvalidOperationException(
                        string.Format("Unexpected character '{0}'.", stream.Peek()));
            }
            // return the result
            var extent = new SourceExtent(sourceChars);
            return new CommentToken(extent);
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
        private static PragmaToken ReadPragmaToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.AddRange(stream.ReadString(Keywords.PRAGMA));
            var extent = new SourceExtent(sourceChars);
            return new PragmaToken(extent);
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
        private static IdentifierToken ReadIdentifierToken(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            var nameChars = new List<char>();
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
            return new IdentifierToken(extent, name);
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
        private static AliasIdentifierToken ReadAliasIdentifierToken(ILexerStream stream)
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
        private static IntegerLiteralToken ReadIntegerLiteralToken(ILexerStream stream)
        {
            /// BUGBUG - this method is woefully underimplemented!
            var sourceChars = new List<SourceChar>();
            // read the sign (if there is one)
            var sign = 0;
            var peek = stream.Peek();
            switch (peek.Value)
            {
                case '+':
                    sign = 1;
                    sourceChars.Add(stream.Read());
                    break;
                case '-':
                    sign = -1;
                    sourceChars.Add(stream.Read());
                    break;
            }
            // read the remaining characters
            // BUGBUG - only handles decimalValue
            sourceChars.Add(stream.ReadDigit());
            while (!stream.Eof && stream.PeekDigit())
            {
                sourceChars.Add(stream.ReadDigit());
            }
            // return the result
            var extent = new SourceExtent(sourceChars);
            return new IntegerLiteralToken(extent, long.Parse(extent.Text));
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
        private static StringLiteralToken ReadStringLiteralToken(ILexerStream stream)
        {
            // BUGBUG - no support for *( *WS DOUBLEQUOTE *stringChar DOUBLEQUOTE )
            // BUGBUG - incomplete escape sequences
            // BUGBUG - no support for UCS characters
            var sourceChars = new List<SourceChar>();
            // read the first character
            sourceChars.Add(stream.ReadChar('"'));
            // read the remaining characters
            var parser = new StringLiteralParser();
            while (!stream.Eof)
            {
                var peek = stream.Peek();
                if (StringValidator.IsDoubleQuote(peek.Value) && !parser.IsEscaped)
                {
                    parser.ConsumeEos();
                    break;
                }
                else
                {
                    sourceChars.Add(peek);
                    parser.ConsumeChar(stream.Read());
                }
            }
            // read the last character
            sourceChars.Add(stream.ReadChar('"'));
            // process any escape sequences in the string
            var unescaped = parser.OutputString.ToString();
            // return the result
            var extent = new SourceExtent(sourceChars);
            return new StringLiteralToken(extent, unescaped);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        ///     stringEscapeSequence = BACKSLASH ( BACKSLASH / DOUBLEQUOTE / SINGLEQUOTE /
        ///                                        BACKSPACE_ESC / TAB_ESC / LINEFEED_ESC /
        ///                                        FORMFEED_ESC / CARRIAGERETURN_ESC /
        ///                                        escapedUCSchar )
        ///
        ///     BACKSPACE_ESC      = "b" ; escape for back space (U+0008)
        ///     TAB_ESC            = "t" ; escape for horizontal tab (U+0009)
        ///     LINEFEED_ESC       = "n" ; escape for line feed (U+000A)
        ///     FORMFEED_ESC       = "f" ; escape for form feed (U+000C)
        ///     CARRIAGERETURN_ESC = "r" ; escape for carriage return (U+000D)
        ///     escapedUCSchar     = ( "x" / "X" ) 1*6( hexDigit ) ; escaped UCS
        ///                          ; character with a UCS code position that is
        ///                          ; the numeric value of the hex number
        ///
        /// </remarks>
        private class StringLiteralParser
        {

            private readonly Dictionary<char, char> _escapeMap = new Dictionary<char, char>()
            {
                { '\\' , '\\' }, { '\"' , '\"' },  { '\'' , '\'' }, {'b', '\b'}, {'t', '\t'},  {'n', '\n'}, {'f', '\f'}, {'r', '\r'}
            };

            private System.Text.StringBuilder _outputString;

            public bool IsEscaped
            {
                get;
                set;
            }

            public StringBuilder OutputString
            {
                get
                {
                    if (_outputString == null)
                    {
                        _outputString = new StringBuilder();
                    }
                    return _outputString;
                }

            }

            /// <summary>
            /// BUGBUG - doesn't handle escapedUCSchar
            /// </summary>
            public void ConsumeChar(SourceChar @char)
            {
                if (this.IsEscaped)
                {
                    if (_escapeMap.ContainsKey(@char.Value))
                    {
                        this.OutputString.Append(_escapeMap[@char.Value]);
                        this.IsEscaped = false;
                    }
                    else
                    {
                        throw new UnexpectedCharacterException(@char);
                    }
                }
                else if (@char.Value == StringValidator.Backslash)
                {
                    this.IsEscaped = true;
                }
                else
                {
                    this.OutputString.Append(@char.Value);
                }
            }

            public void ConsumeEos()
            {
                if (this.IsEscaped)
                {
                    throw new InvalidOperationException("Incomplete escape sequence found.");
                }
            }

        }

        #endregion

        #endregion

    }

}
