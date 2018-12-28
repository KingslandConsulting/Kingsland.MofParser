using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Source;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingsland.MofParser.Lexing
{

    public static class LexerEngine
    {

        public static (Token Token, Lexer NextLexer) ReadToken(Lexer lexer)
        {
            var reader = lexer.Reader;
            var peek = reader.Peek();
            switch (peek.Value)
            {
                case '$':
                    {
                        var (aliasIdentifierToken, nextReader) = LexerEngine.ReadAliasIdentifierToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (aliasIdentifierToken, nextLexer);
                    }
                case ']':
                    {
                        var (attributeClosetoken, nextReader) = LexerEngine.ReadAttributeCloseToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (attributeClosetoken, nextLexer);
                    }
                case '[':
                    {
                        var (attributeOpenToken, nextReader) = LexerEngine.ReadAttributeOpenToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (attributeOpenToken, nextLexer);
                    }
                case '}':
                    {
                        var (blockCloseToken, nextReader) = LexerEngine.ReadBlockCloseToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (blockCloseToken, nextLexer);
                    }
                case '{':
                    {
                        var (blockOpenToken, nextReader) = LexerEngine.ReadBlockOpenToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (blockOpenToken, nextLexer);
                    }
                case ':':
                    {
                        var (colonToken, nextReader) = LexerEngine.ReadColonToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (colonToken, nextLexer);
                    }
                case ',':
                    {
                        var (commaToken, nextReader) = LexerEngine.ReadCommaToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (commaToken, nextLexer);
                    }
                case '/':
                    {
                        var (forwardSlashToken, nextReader) = LexerEngine.ReadCommentToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (forwardSlashToken, nextLexer);
                    }
                case '=':
                    {
                        var (equalsToken, nextReader) = LexerEngine.ReadEqualsOperatorToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (equalsToken, nextLexer);
                    }
                case ')':
                    {
                        var (parenthesesCloseToken, nextReader) = LexerEngine.ReadParenthesesCloseToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (parenthesesCloseToken, nextLexer);
                    }
                case '(':
                    {
                        var (parenthesesOpenToken, nextReader) = LexerEngine.ReadParenthesesOpenToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (parenthesesOpenToken, nextLexer);
                    }
                case '#':
                    {
                        var (pragmaToken, nextReader) = LexerEngine.ReadPragmaToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (pragmaToken, nextLexer);
                    }
                case ';':
                    {
                        var (statementEndToken, nextReader) = LexerEngine.ReadStatementEndToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (statementEndToken, nextLexer);
                    }
                case '"':
                    {
                        var (stringLiteralToken, nextReader) = LexerEngine.ReadStringLiteralToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (stringLiteralToken, nextLexer);
                    }
                default:
                    if (StringValidator.IsWhitespace(peek.Value))
                    {
                        var (whitespaceToken, nextReader) = LexerEngine.ReadWhitespaceToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (whitespaceToken, nextLexer);
                    }
                    else if (StringValidator.IsFirstIdentifierChar(peek.Value))
                    {
                        var (identifierToken, nextReader) = LexerEngine.ReadIdentifierToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        if (StringValidator.IsFalse(identifierToken.Name))
                        {
                            /// 7.6.1.5 Boolean value
                            ///
                            ///     booleanValue = TRUE / FALSE
                            ///
                            ///     FALSE        = "false" ; keyword: case insensitive
                            ///     TRUE         = "true"  ; keyword: case insensitive
                            ///
                            var booleanLiteralToken = new BooleanLiteralToken(identifierToken.Extent, false);
                            return (booleanLiteralToken, nextLexer);
                        }
                        else if (StringValidator.IsTrue(identifierToken.Name))
                        {
                            /// 7.6.1.5 Boolean value
                            ///
                            ///     booleanValue = TRUE / FALSE
                            ///
                            ///     FALSE        = "false" ; keyword: case insensitive
                            ///     TRUE         = "true"  ; keyword: case insensitive
                            var booleanLiteralToken = new BooleanLiteralToken(identifierToken.Extent, true);
                            return (booleanLiteralToken, nextLexer);
                        }
                        else if (StringValidator.IsNull(identifierToken.Name))
                        {
                            /// 7.6.1.6 Null value
                            ///
                            ///     nullValue = NULL
                            ///
                            ///     NULL = "null" ; keyword: case insensitive
                            ///                   ; second
                            var nullLiteralToken = new NullLiteralToken(identifierToken.Extent);
                            return (nullLiteralToken, nextLexer);
                        }
                        else
                        {
                            return (identifierToken, nextLexer);
                        }
                    }
                    else if ((peek.Value == '+') || (peek.Value == '-') ||
                             (StringValidator.IsDecimalDigit(peek.Value)))
                    {
                        var (integerToken, nextReader) = LexerEngine.ReadIntegerLiteralToken(reader);
                        var nextLexer = new Lexer(nextReader);
                        return (integerToken, nextLexer);
                    }
                    else
                    {
                        throw new UnexpectedCharacterException(peek);
                    }
            }
        }

        #region Symbols

        public static (AttributeCloseToken, SourceReader) ReadAttributeCloseToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(']');
            var extent = SourceExtent.From(sourceChar);
            return (new AttributeCloseToken(extent), nextReader);
        }

        public static (AttributeOpenToken, SourceReader) ReadAttributeOpenToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('[');
            var extent = SourceExtent.From(sourceChar);
            return (new AttributeOpenToken(extent), nextReader);
        }

        public static (BlockCloseToken, SourceReader) ReadBlockCloseToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('}');
            var extent = SourceExtent.From(sourceChar);
            return (new BlockCloseToken(extent), nextReader);
        }

        public static (BlockOpenToken, SourceReader) ReadBlockOpenToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('{');
            var extent = SourceExtent.From(sourceChar);
            return (new BlockOpenToken(extent), nextReader);
        }

        public static (ColonToken, SourceReader) ReadColonToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(':');
            var extent = SourceExtent.From(sourceChar);
            return (new ColonToken(extent), nextReader);
        }

        public static (CommaToken, SourceReader) ReadCommaToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(',');
            var extent = SourceExtent.From(sourceChar);
            return (new CommaToken(extent), nextReader);
        }

        public static (EqualsOperatorToken, SourceReader) ReadEqualsOperatorToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('=');
            var extent = SourceExtent.From(sourceChar);
            return (new EqualsOperatorToken(extent), nextReader);
        }

        public static (ParenthesesCloseToken, SourceReader) ReadParenthesesCloseToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(')');
            var extent = SourceExtent.From(sourceChar);
            return (new ParenthesesCloseToken(extent), nextReader);
        }

        public static (ParenthesesOpenToken, SourceReader) ReadParenthesesOpenToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('(');
            var extent = SourceExtent.From(sourceChar);
            return (new ParenthesesOpenToken(extent), nextReader);
        }

        public static (StatementEndToken, SourceReader) ReadStatementEndToken(SourceReader reader)
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
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 5.2 Whitespace
        ///
        /// Whitespace in a MOF file is any combination of the following characters:
        ///
        ///     * Space (U+0020),
        ///     * Horizontal Tab (U+0009),
        ///     * Carriage Return (U+000D) and
        ///     * Line Feed (U+000A).
        ///
        /// The WS ABNF rule represents any one of these whitespace characters:
        ///
        ///     WS = U+0020 / U+0009 / U+000D / U+000A
        ///
        /// </remarks>
        public static (WhitespaceToken, SourceReader) ReadWhitespaceToken(SourceReader reader)
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
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 5.4 Comments
        ///
        /// Comments in a MOF file do not create, modify, or annotate language elements. They shall be treated as if
        /// they were whitespace.
        ///
        /// Comments may appear anywhere in MOF syntax where whitespace is allowed and are indicated by either
        /// a leading double slash (//) or a pair of matching /* and */ character sequences. Occurrences of these
        /// character sequences in string literals shall not be treated as comments.
        ///
        /// A // comment is terminated by the end of line (see 5.3), as shown in the example below.
        ///
        ///     Integer MyProperty; // This is an example of a single-line comment
        ///
        /// A comment that begins with /* is terminated by the next */ sequence, or by the end of the MOF file,
        /// whichever comes first.
        ///
        ///     /* example of a comment between property definition tokens and a multi-line comment */
        ///     Integer /* 16-bit integer property */ MyProperty; /* and a multi-line
        ///                             comment */
        ///
        /// </remarks>
        public static (CommentToken, SourceReader) ReadCommentToken(SourceReader reader)
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

        #region 7.3 Compiler directives

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.3 Compiler directives
        ///
        /// Compiler directives direct the processing of MOF files. Compiler directives do not create, modify, or
        /// annotate the language elements.
        ///
        /// Compiler directives shall conform to the format defined by ABNF rule compilerDirective (whitespace
        /// as defined in 5.2 is allowed between the elements of the rules in this ABNF section):
        ///
        ///     compilerDirective = PRAGMA ( pragmaName / standardPragmaName )
        ///                         "(" pragmaParameter ")"
        ///
        ///     pragmaName         = directiveName
        ///     standardPragmaName = INCLUDE
        ///     pragmaParameter    = stringValue ; if the pragma is INCLUDE,
        ///                                      ; the parameter value
        ///                                      ; shall represent a relative
        ///                                      ; or full file path
        ///     PRAGMA             = "#pragma"  ; keyword: case insensitive
        ///     INCLUDE            = "include"  ; keyword: case insensitive
        ///
        ///     directiveName      = org-id "_" IDENTIFIER
        ///
        /// </remarks>
        public static (PragmaToken, SourceReader) ReadPragmaToken(SourceReader reader)
        {
            (var sourceChars, var thisReader) = reader.ReadString(Constants.PRAGMA, true);
            var extent = SourceExtent.From(sourceChars.ToList());
            return (new PragmaToken(extent), thisReader);
        }

        #endregion

        #region 7.7.1 Names

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.7.1 Names
        ///
        /// MOF names are identifiers with the format defined by the IDENTIFIER rule.
        ///
        /// No whitespace is allowed between the elements of the rules in this ABNF section.
        ///
        ///     IDENTIFIER          = firstIdentifierChar *( nextIdentifierChar )
        ///     firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE
        ///     nextIdentifierChar  = firstIdentifierChar / decimalDigit
        ///     elementName         = localName / schemaQualifiedName
        ///     localName           = IDENTIFIER
        ///
        /// </remarks>
        public static (IdentifierToken, SourceReader) ReadIdentifierToken(SourceReader reader)
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

        #region 7.7.3 Alias identifier

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.7.3 Alias identifier
        ///
        /// An aliasIdentifier identifies an Instance or Value within the context of a MOF compilation unit.
        ///
        /// No whitespace is allowed between the elements of this rule.
        ///
        ///     aliasIdentifier     = "$" IDENTIFIER
        ///
        ///     IDENTIFIER          = firstIdentifierChar *( nextIdentifierChar )
        ///     firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE
        ///     nextIdentifierChar  = firstIdentifierChar / decimalDigit
        ///     elementName         = localName / schemaQualifiedName
        ///     localName           = IDENTIFIER
        ///
        /// </remarks>
        public static (AliasIdentifierToken, SourceReader) ReadAliasIdentifierToken(SourceReader reader)
        {
            var thisReader = reader;
            var sourceChar = default(SourceChar);
            var sourceChars = new List<SourceChar>();
            var nameChars = new StringBuilder();
            // "$"
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

        #region 7.6.1.1 Integer value

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1.1 Integer value
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
        ///
        ///     decimalDigit         = "0" / positiveDecimalDigit
        ///     positiveDecimalDigit = "1"..."9"
        ///
        /// </remarks>
        public static (IntegerLiteralToken, SourceReader) ReadIntegerLiteralToken(SourceReader reader)
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

        #region 7.6.1.3 String values

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1.3 String values
        ///
        /// Unless explicitly specified via ABNF rule WS, no whitespace is allowed between the elements of the rules
        /// in this ABNF section.
        ///
        ///     singleStringValue = DOUBLEQUOTE *stringChar DOUBLEQUOTE
        ///     stringValue       = singleStringValue *( *WS singleStringValue )
        ///
        ///     stringChar        = stringUCSchar / stringEscapeSequence
        ///     stringUCSchar     = U+0020...U+0021 / U+0023...U+D7FF /
        ///                         U+E000...U+FFFD / U+10000...U+10FFFF
        ///                         ; Note that these UCS characters can be
        ///                         ; represented in XML without any escaping
        ///                         ; (see W3C XML).
        ///
        ///     stringEscapeSequence = BACKSLASH ( BACKSLASH / DOUBLEQUOTE / SINGLEQUOTE /
        ///                            BACKSPACE_ESC / TAB_ESC / LINEFEED_ESC /
        ///                            FORMFEED_ESC / CARRIAGERETURN_ESC /
        ///                            escapedUCSchar )
        ///
        ///     BACKSPACE_ESC      = "b" ; escape for back space (U+0008)
        ///     TAB_ESC            = "t" ; escape for horizontal tab(U+0009)
        ///     LINEFEED_ESC       = "n" ; escape for line feed(U+000A)
        ///     FORMFEED_ESC       = "f" ; escape for form feed(U+000C)
        ///     CARRIAGERETURN_ESC = "r" ; escape for carriage return (U+000D)
        ///
        ///     escapedUCSchar     = ( "x" / "X" ) 1*6(hexDigit ) ; escaped UCS
        ///                          ; character with a UCS code position that is
        ///                          ; the numeric value of the hex number
        ///
        /// The following special characters are also used in other ABNF rules in this specification:
        ///
        ///     BACKSLASH   = U+005C ; \
        ///     DOUBLEQUOTE = U+0022 ; "
        ///     SINGLEQUOTE = U+0027 ; '
        ///     UPPERALPHA  = U+0041...U+005A ; A ... Z
        ///     LOWERALPHA  = U+0061...U+007A ; a ... z
        ///     UNDERSCORE  = U+005F ; _
        ///
        /// </remarks>
        public static (StringLiteralToken, SourceReader) ReadStringLiteralToken(SourceReader reader)
        {
            // BUGBUG - no support for *( *WS DOUBLEQUOTE *stringChar DOUBLEQUOTE )
            // BUGBUG - incomplete escape sequences
            // BUGBUG - no support for UCS characters
            var thisReader = reader;
            var sourceChar = default(SourceChar);
            var sourceChars = new List<SourceChar>();
            var stringChars = new StringBuilder();
            // read the first double-quote character
            (sourceChar, thisReader) = thisReader.Read(Constants.DOUBLEQUOTE);
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
                        case Constants.BACKSLASH:
                            escapedChar = Constants.BACKSLASH;
                            break;
                        case Constants.DOUBLEQUOTE:
                            escapedChar = Constants.DOUBLEQUOTE;
                            break;
                        case Constants.SINGLEQUOTE:
                            escapedChar = Constants.SINGLEQUOTE;
                            break;
                        case Constants.BACKSPACE_ESC:
                            escapedChar = '\b';
                            break;
                        case Constants.TAB_ESC:
                            escapedChar = '\t';
                            break;
                        case Constants.LINEFEED_ESC:
                            escapedChar = '\n';
                            break;
                        case Constants.FORMFEED_ESC:
                            escapedChar = '\f';
                            break;
                        case Constants.CARRIAGERETURN_ESC:
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
                else if (peek.Value == Constants.BACKSLASH)
                {
                    // read the first character in an escape sequence
                    thisReader = thisReader.Next();
                    sourceChars.Add(peek);
                    isEscaped = true;
                }
                else if (peek.Value == Constants.DOUBLEQUOTE)
                {
                    // read the last double-quote character
                    (_, thisReader) = thisReader.Read(Constants.DOUBLEQUOTE);
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

    }

}
