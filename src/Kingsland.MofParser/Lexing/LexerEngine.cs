using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Source;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingsland.MofParser.Lexing
{

    internal static class LexerEngine
    {

        #region Dispatcher

        public static (Token Token, Lexer NextLexer) ReadToken(Lexer lexer)
        {

            var reader = lexer.Reader;
            var peek = reader.Peek();

            switch (peek.Value)
            {

                case '$':
                    return LexerEngine.ReadAliasIdentifierToken(reader);
                case ']':
                    return LexerEngine.ReadAttributeCloseToken(reader);
                case '[':
                    return LexerEngine.ReadAttributeOpenToken(reader);
                case '}':
                    return LexerEngine.ReadBlockCloseToken(reader);
                case '{':
                    return LexerEngine.ReadBlockOpenToken(reader);
                case ':':
                    return LexerEngine.ReadColonToken(reader);
                case ',':
                    return LexerEngine.ReadCommaToken(reader);
                case '/':
                    return LexerEngine.ReadCommentToken(reader);
                case '=':
                    return LexerEngine.ReadEqualsOperatorToken(reader);
                case ')':
                    return LexerEngine.ReadParenthesisCloseToken(reader);
                case '(':
                    return LexerEngine.ReadParenthesisOpenToken(reader);
                case '#':
                    return LexerEngine.ReadPragmaToken(reader);
                case ';':
                    return LexerEngine.ReadStatementEndToken(reader);
                case '"':
                    return LexerEngine.ReadStringLiteralToken(reader);
                case '+':
                case '-':
                    return LexerEngine.ReadNumericLiteralToken(reader);
                case '.':
                    // if the next character is a decimalDigit then we're reading a RealLiteralToken with
                    // no leading digits before the decimal point (e.g. ".45"), otherwise we're reading
                    // a DotOperatorToken (e.g. the "." in "MyPropertyValue = MyEnum.Value;")
                    var readAhead = reader.Read().NextReader;
                    if (!readAhead.Eof() && StringValidator.IsDecimalDigit(readAhead.Peek().Value))
                    {
                        return LexerEngine.ReadNumericLiteralToken(reader);
                    }
                    else
                    {
                        return LexerEngine.ReadDotOperatorToken(reader);
                    }

                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'g':
                case 'h':
                case 'i':
                case 'j':
                case 'k':
                case 'l':
                case 'm':
                case 'n':
                case 'o':
                case 'p':
                case 'q':
                case 'r':
                case 's':
                case 't':
                case 'u':
                case 'v':
                case 'w':
                case 'x':
                case 'y':
                case 'z':
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                case 'G':
                case 'H':
                case 'I':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'O':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'U':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                case '_':
                    // firstIdentifierChar
                    var (identifierToken, nextLexer) = LexerEngine.ReadIdentifierToken(reader);
                    var normalized = identifierToken.Name.ToLowerInvariant();
                    switch (normalized)
                    {
                        case "false":
                            var falseToken = new BooleanLiteralToken(identifierToken.Extent, false);
                            return (falseToken, nextLexer);
                        case "true":
                            var trueToken = new BooleanLiteralToken(identifierToken.Extent, true);
                            return (trueToken, nextLexer);
                        case "null":
                            var nullLiteralToken = new NullLiteralToken(identifierToken.Extent);
                            return (nullLiteralToken, nextLexer);
                        default:
                            return (identifierToken, nextLexer);
                    }

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    // decimalDigit
                    return LexerEngine.ReadNumericLiteralToken(reader);

                case '\u0020': // space
                case '\u0009': // horizontal tab
                case '\u000D': // carriage return
                case '\u000A': // line feed
                    // WS
                    return LexerEngine.ReadWhitespaceToken(reader);

                default:
                    throw new UnexpectedCharacterException(peek);

            }

        }

        #endregion

        #region Symbols

        public static (AttributeCloseToken, Lexer) ReadAttributeCloseToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(']');
            var extent = SourceExtent.From(sourceChar);
            return (new AttributeCloseToken(extent), new Lexer(nextReader));
        }

        public static (AttributeOpenToken, Lexer) ReadAttributeOpenToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('[');
            var extent = SourceExtent.From(sourceChar);
            return (new AttributeOpenToken(extent), new Lexer(nextReader));
        }

        public static (BlockCloseToken, Lexer) ReadBlockCloseToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('}');
            var extent = SourceExtent.From(sourceChar);
            return (new BlockCloseToken(extent), new Lexer(nextReader));
        }

        public static (BlockOpenToken, Lexer) ReadBlockOpenToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('{');
            var extent = SourceExtent.From(sourceChar);
            return (new BlockOpenToken(extent), new Lexer(nextReader));
        }

        public static (ColonToken, Lexer) ReadColonToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(':');
            var extent = SourceExtent.From(sourceChar);
            return (new ColonToken(extent), new Lexer(nextReader));
        }

        public static (CommaToken, Lexer) ReadCommaToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(',');
            var extent = SourceExtent.From(sourceChar);
            return (new CommaToken(extent), new Lexer(nextReader));
        }

        public static (DotOperatorToken, Lexer) ReadDotOperatorToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('.');
            var extent = SourceExtent.From(sourceChar);
            return (new DotOperatorToken(extent), new Lexer(nextReader));
        }

        public static (EqualsOperatorToken, Lexer) ReadEqualsOperatorToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('=');
            var extent = SourceExtent.From(sourceChar);
            return (new EqualsOperatorToken(extent), new Lexer(nextReader));
        }

        public static (ParenthesisCloseToken, Lexer) ReadParenthesisCloseToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(')');
            var extent = SourceExtent.From(sourceChar);
            return (new ParenthesisCloseToken(extent), new Lexer(nextReader));
        }

        public static (ParenthesisOpenToken, Lexer) ReadParenthesisOpenToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read('(');
            var extent = SourceExtent.From(sourceChar);
            return (new ParenthesisOpenToken(extent), new Lexer(nextReader));
        }

        public static (StatementEndToken, Lexer) ReadStatementEndToken(SourceReader reader)
        {
            (var sourceChar, var nextReader) = reader.Read(';');
            var extent = SourceExtent.From(sourceChar);
            return (new StatementEndToken(extent), new Lexer(nextReader));
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
        public static (WhitespaceToken, Lexer) ReadWhitespaceToken(SourceReader reader)
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
            return (new WhitespaceToken(extent), new Lexer(thisReader));
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
        public static (CommentToken, Lexer) ReadCommentToken(SourceReader reader)
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
            return (new CommentToken(extent), new Lexer(thisReader));
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
        public static (PragmaToken, Lexer) ReadPragmaToken(SourceReader reader)
        {
            (var sourceChars, var thisReader) = reader.ReadString(Constants.PRAGMA, true);
            var extent = SourceExtent.From(sourceChars.ToList());
            return (new PragmaToken(extent), new Lexer(thisReader));
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
        public static (IdentifierToken, Lexer) ReadIdentifierToken(SourceReader reader)
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
            return (new IdentifierToken(extent, name), new Lexer(thisReader));
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
        public static (AliasIdentifierToken, Lexer) ReadAliasIdentifierToken(SourceReader reader)
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
            return (new AliasIdentifierToken(extent, name), new Lexer(thisReader));
        }

        #endregion

        #region 7.6.1.1 Integer value / 7.6.1.2 Real value

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
        ///     unsignedDecimalValue = "0" / positiveDecimalDigit *decimalDigit
        ///
        ///     decimalDigit         = "0" / positiveDecimalDigit
        ///     positiveDecimalDigit = "1"..."9"
        ///
        /// 7.6.1.2 Real value
        ///
        /// No whitespace is allowed between the elements of the rules in this ABNF section.
        ///
        ///     realValue            = [ "+" / "-" ] *decimalDigit "." 1*decimalDigit
        ///                            [ ( "e" / "E" ) [ "+" / "-" ] 1*decimalDigit ]
        ///
        ///     decimalDigit         = "0" / positiveDecimalDigit
        ///     positiveDecimalDigit = "1"..."9"
        ///
        /// </remarks>
        public static (Token, Lexer) ReadNumericLiteralToken(SourceReader reader)
        {

            int ParseBinaryValueDigits(IEnumerable<SourceChar> binaryChars, SourceChar sign)
            {
                return ParseIntegerValueDigits(new Dictionary<char, int>
                {
                    { '0', 0 }, { '1', 1 }
                }, 2, binaryChars, sign);
            }

            int ParseOctalValueDigits(IEnumerable<SourceChar> octalChars, SourceChar sign)
            {
                return ParseIntegerValueDigits(new Dictionary<char, int>
                {
                    { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }
                }, 8, octalChars, sign);
            }

            int ParseHexValueDigits(IEnumerable<SourceChar> hexChars, SourceChar sign)
            {
                return ParseIntegerValueDigits(new Dictionary<char, int>
                {
                    { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
                    { 'a', 10 }, { 'b', 11 }, { 'c', 12 }, { 'd', 13 }, { 'e', 14 }, { 'f', 15 },
                    { 'A', 10 }, { 'B', 11 }, { 'C', 12 }, { 'D', 13 }, { 'E', 14 }, { 'F', 15 }
                }, 16, hexChars, sign);
            }

            int ParseDecimalValueDigits(IEnumerable<SourceChar> decimalChars, SourceChar sign)
            {
                return ParseIntegerValueDigits(new Dictionary<char, int>
                {
                    { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 }
                }, 10, decimalChars, sign);
            }

            int ParseIntegerValueDigits(Dictionary<char, int> alphabet, int radix, IEnumerable<SourceChar> chars, SourceChar sign)
            {
                var literalValue = 0;
                foreach (var digit in chars)
                {
                    var digitValue = alphabet[digit.Value];
                    literalValue = (literalValue * radix) + digitValue;
                }
                if (sign?.Value == '-')
                {
                    literalValue = -literalValue;
                }
                return literalValue;
            }

            const int stateLeadingSign = 1;
            const int stateFirstDigitBlock = 2;
            const int stateOctalOrDecimalValue = 3;
            const int stateBinaryValue = 4;
            const int stateOctalValue = 5;
            const int stateHexValue = 6;
            const int stateDecimalValue = 7;
            const int stateRealValue = 8;
            const int stateRealValueFraction = 9;
            const int stateRealValueExponent = 10;
            const int stateDone = 99;

            var thisReader = reader;
            var sourceChar = default(SourceChar);
            var sourceChars = new List<SourceChar>();

            var token = default(Token);
            var signChar = default(SourceChar);
            var firstDigitBlock = new List<SourceChar>();

            var currentState = stateLeadingSign;
            while (currentState != stateDone)
            {
                switch (currentState)
                {

                    case stateLeadingSign:
                        // we're reading the initial optional leading sign
                        // [ "+" / "-" ]
                        sourceChar = thisReader.Peek();
                        switch (sourceChar.Value)
                        {
                            case '+':
                            case '-':
                                (signChar, thisReader) = thisReader.Read();
                                sourceChars.Add(signChar);
                                break;
                        }
                        currentState = stateFirstDigitBlock;
                        break;

                    case stateFirstDigitBlock:
                        // we're reading the first block of digits in the value,
                        // but we won't necessarily know which type we're reading
                        // until we've consumed more of the input stream
                        //
                        //     binaryValue  => 1*binaryDigit
                        //     octalValue   => "0" 1*octalDigit
                        //     hexValue     => ( "0x" / "0X" )
                        //     decimalValue => positiveDecimalDigit *decimalDigit
                        //     realValue    => *decimalDigit
                        //
                        if (thisReader.Peek('.'))
                        {
                            // we're reading a realValue with no "*decimalDigit" characters before the "."
                            // e.g. ".45", "+.45", "-.45", so consume the decimal point
                            (sourceChar, thisReader) = thisReader.Read();
                            sourceChars.Add(sourceChar);
                            // and go to the next state
                            currentState = stateRealValueFraction;
                            break;
                        }
                        // we don't know which base the value is in yet, but if it's hexadecimal them
                        // we should be reading the "0x" part here, so restrict digits to decimal in
                        // all cases
                        (firstDigitBlock, thisReader) = thisReader.ReadWhile(StringValidator.IsDecimalDigit);
                        sourceChars.AddRange(firstDigitBlock);
                        // now we can do some validation
                        if (firstDigitBlock.Count == 0)
                        {
                            // only realValue allows no digits in the first block, and
                            // we've already handled that at the start of this case
                            throw new UnexpectedCharacterException(sourceChar);
                        }
                        // if we've reached the end of the stream then there's no suffix
                        // (e.g. b, B, x, X, .) so this must be an octalValue or decimalValue
                        if (thisReader.Eof())
                        {
                            currentState = stateOctalOrDecimalValue;
                            break;
                        }
                        // check the next character to see if it tells us anything
                        // about which type of literal we're reading
                        sourceChar = thisReader.Peek();
                        switch (sourceChar.Value)
                        {
                            case 'b':
                            case 'B':
                                // binaryValue
                                currentState = stateBinaryValue;
                                break;
                            case 'x':
                            case 'X':
                                // hexValue
                                currentState = stateHexValue;
                                break;
                            case '.':
                                // realValue
                                currentState = stateRealValue;
                                break;
                            default:
                                // by elmination, this must be an octalValue or decimalValue
                                currentState = stateOctalOrDecimalValue;
                                break;
                        }
                        break;

                    case stateOctalOrDecimalValue:
                        // we're reading an octalValue or decimalValue, but we're not sure which yet...
                        if ((firstDigitBlock.First().Value == '0') && (firstDigitBlock.Count > 1))
                        {
                            currentState = stateOctalValue;
                        }
                        else
                        {
                            currentState = stateDecimalValue;
                        }
                        break;

                    case stateBinaryValue:
                        // we're trying to read a binaryValue, so check all the characters in the digit block are valid,
                        // i.e. 1*binaryDigit
                        if (firstDigitBlock.Any(c => !StringValidator.IsBinaryDigit(c.Value)))
                        {
                            throw new UnexpectedCharacterException(sourceChar);
                        }
                        // all the characters are valid, so consume the suffix
                        (sourceChar, thisReader) = thisReader.Read(c => (c == 'b') || (c == 'B'));
                        sourceChars.Add(sourceChar);
                        // now build the return value
                        var binaryValue = ParseBinaryValueDigits(firstDigitBlock, signChar);
                        token = new IntegerLiteralToken(SourceExtent.From(sourceChars), IntegerKind.BinaryValue, binaryValue);
                        // and we're done
                        currentState = stateDone;
                        break;

                    case stateOctalValue:
                        // we're trying to read an octalValue (since decimalValue can't start with a
                        // leading '0') so check all the characters in the digit block are valid,
                        // i.e. "0" 1*octalDigit
                        if ((firstDigitBlock.Count < 2) || (firstDigitBlock.First().Value != '0'))
                        {
                            throw new UnexpectedCharacterException(sourceChar);
                        }
                        if (firstDigitBlock.Skip(1).Any(c => !StringValidator.IsOctalDigit(c.Value)))
                        {
                            throw new UnexpectedCharacterException(sourceChar);
                        }
                        // now build the return value
                        var octalValue = ParseOctalValueDigits(firstDigitBlock, signChar);
                        token = new IntegerLiteralToken(SourceExtent.From(sourceChars), IntegerKind.OctalValue, octalValue);
                        // and we're done
                        currentState = stateDone;
                        break;

                    case stateHexValue:
                        // we're trying to read a hexValue, so we should have just read a leading zero
                        if ((firstDigitBlock.Count != 1) || (firstDigitBlock.First().Value != '0'))
                        {
                            throw new UnexpectedCharacterException(sourceChar);
                        }
                        // all the characters are valid, so consume the suffix
                        (sourceChar, thisReader) = thisReader.Read(c => (c == 'x') || (c == 'X'));
                        sourceChars.Add(sourceChar);
                        // 1*hexDigit
                        var hexDigits = default(List<SourceChar>);
                        (hexDigits, thisReader) = thisReader.ReadWhile(StringValidator.IsHexDigit);
                        if (hexDigits.Count == 0)
                        {
                            throw new UnexpectedCharacterException(thisReader.Peek());
                        }
                        sourceChars.AddRange(hexDigits);
                        // build the return value
                        var hexValue = ParseHexValueDigits(hexDigits, signChar);
                        token = new IntegerLiteralToken(SourceExtent.From(sourceChars), IntegerKind.HexValue, hexValue);
                        // and we're done
                        currentState = stateDone;
                        break;

                    case stateDecimalValue:
                        // we're trying to read a decimalValue (since that's the only remaining option),
                        // so check all the characters in the digit block are valid,
                        // i.e. "0" / positiveDecimalDigit *decimalDigit
                        if ((firstDigitBlock.Count == 1) && (firstDigitBlock.First().Value == '0'))
                        {
                            // "0"
                        }
                        else if (!StringValidator.IsPositiveDecimalDigit(firstDigitBlock.First().Value))
                        {
                            throw new UnexpectedCharacterException(sourceChar);
                        }
                        else if (firstDigitBlock.Skip(1).Any(c => !StringValidator.IsDecimalDigit(c.Value)))
                        {
                            throw new UnexpectedCharacterException(sourceChar);
                        }
                        // build the return value
                        var decimalValue = ParseDecimalValueDigits(firstDigitBlock, signChar);
                        token = new IntegerLiteralToken(SourceExtent.From(sourceChars), IntegerKind.DecimalValue, decimalValue);
                        // and we're done
                        currentState = stateDone;
                        break;

                    case stateRealValue:
                        // we're trying to read a realValue, so check all the characters in the digit block are valid,
                        // i.e. *decimalDigit
                        if (firstDigitBlock.Any(c => !StringValidator.IsDecimalDigit(c.Value)))
                        {
                            throw new UnexpectedCharacterException(sourceChar);
                        }
                        // all the characters are valid, so consume the decimal point
                        (sourceChar, thisReader) = thisReader.Read('.');
                        sourceChars.Add(sourceChar);
                        // and go to the next state
                        currentState = stateRealValueFraction;
                        break;

                    case stateRealValueFraction:
                        // 1*decimalDigit
                        var realFractionDigits = default(List<SourceChar>);
                        (realFractionDigits, thisReader) = thisReader.ReadWhile(StringValidator.IsHexDigit);
                        if (realFractionDigits.Count == 0)
                        {
                            throw new UnexpectedCharacterException(thisReader.Peek());
                        }
                        sourceChars.AddRange(realFractionDigits);
                        // ( "e" / "E" )
                        if (!thisReader.Eof())
                        {
                            sourceChar = thisReader.Peek();
                            if ((sourceChar.Value == 'e') || (sourceChar.Value == 'E'))
                            {
                                currentState = stateRealValueExponent;
                                break;
                            }
                        }
                        // build the return value
                        var realIntegerValue = ParseDecimalValueDigits(firstDigitBlock, signChar);
                        var realFractionValue = (double)ParseDecimalValueDigits(realFractionDigits, signChar);
                        if (realFractionDigits.Any())
                        {
                            realFractionValue = realFractionValue / Math.Pow(10, realFractionDigits.Count);
                        }
                        token = new RealLiteralToken(
                            SourceExtent.From(sourceChars),
                            realIntegerValue + realFractionValue
                        );
                        // and we're done
                        currentState = stateDone;
                        break;

                    case stateRealValueExponent:
                        throw new InvalidOperationException();

                    case stateDone:
                        // the main while loop should exit before we ever get here
                        throw new InvalidOperationException();

                    default:
                        throw new InvalidOperationException();

                }
            }

            return (token, new Lexer(thisReader));

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
        public static (StringLiteralToken, Lexer) ReadStringLiteralToken(SourceReader reader)
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
            return (new StringLiteralToken(extent, stringValue), new Lexer(thisReader));
        }

        #endregion

    }

}
