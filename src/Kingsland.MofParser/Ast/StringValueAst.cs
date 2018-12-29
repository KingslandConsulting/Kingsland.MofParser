using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{

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
    public sealed class StringValueAst : LiteralValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public string Value
            {
                get;
                set;
            }

            public StringValueAst Build()
            {
                return new StringValueAst(
                    this.Value
                );
            }

        }

        #endregion

        #region Constructors

        public StringValueAst(string value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public string Value
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}
