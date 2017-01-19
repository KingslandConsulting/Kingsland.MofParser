using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public abstract class LiteralValueAst : PrimitiveTypeValueAst
    {

        #region Constructors

        internal LiteralValueAst()
        {
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.1 Value definitions
        ///
        ///     literalValue       = integerValue / realValue /
        ///                          stringValue / octetStringValue
        ///                          booleanValue /
        ///                          nullValue /
        ///                          dateTimeValue
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref LiteralValueAst node, bool throwIfError = false)
        {

            // integerValue
            parser.Descend();
            var integerValue = default(IntegerValueAst);
            if (IntegerValueAst.TryParse(parser, ref integerValue, false))
            {
                parser.Commit();
                node = integerValue;
                return true;
            }
            parser.Backtrack();

            // stringValue
            parser.Descend();
            var stringValue = default(StringValueAst);
            if (StringValueAst.TryParse(parser, ref stringValue, false))
            {
                parser.Commit();
                node = stringValue;
                return true;
            }
            parser.Backtrack();

            // booleanValue
            parser.Descend();
            var booleanValue = default(BooleanValueAst);
            if (BooleanValueAst.TryParse(parser, ref booleanValue, false))
            {
                parser.Commit();
                node = booleanValue;
                return true;
            }
            parser.Backtrack();

            // nullValue
            parser.Descend();
            var nullValue = default(NullValueAst);
            if (NullValueAst.TryParse(parser, ref nullValue, false))
            {
                parser.Commit();
                node = nullValue;
                return true;
            }
            parser.Backtrack();

            // unexpected token
            return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);

        }

        internal new static LiteralValueAst Parse(Parser parser)
        {
            var node = default(LiteralValueAst);
            LiteralValueAst.TryParse(parser, ref node, true);
            return node;
        }

        internal static bool IsLiteralValueToken(Token token)
        {
            return (token is IntegerLiteralToken) ||
                   //(token is RealLiteralToken) ||
                   //(token is DateTimeLiteralToken) ||
                   (token is StringLiteralToken) ||
                   (token is BooleanLiteralToken) ||
                   //(token is OctetStringLiteralToken) ||
                   (token is NullLiteralToken);
        }

        #endregion

    }

}
