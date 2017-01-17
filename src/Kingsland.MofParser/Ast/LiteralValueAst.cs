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
        internal new static LiteralValueAst Parse(Parser parser)
        {
            var state = parser.CurrentState;
            var peek = state.Peek();
			if (peek is IntegerLiteralToken)
            {
                // integerValue
                return IntegerValueAst.Parse(parser);
            }
            else if (peek is StringLiteralToken)
			{
                // stringValue
                return StringValueAst.Parse(parser);
			}
			else if (peek is BooleanLiteralToken)
			{
                // booleanValue
                return BooleanValueAst.Parse(parser);
			}
			else if (peek is NullLiteralToken)
			{
                // nullValue
                return NullValueAst.Parse(parser);
			}
            else
            {
				throw new UnexpectedTokenException(peek);
			}
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
