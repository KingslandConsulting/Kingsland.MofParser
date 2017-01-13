using Kingsland.MofParser.Interfaces;
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
        /// <param name="stream"></param>
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
        internal new static LiteralValueAst Parse(ParserStream stream)
        {
            var peek = stream.Peek();

            var literalValueToken = peek as ILiteralValueToken;

            if (literalValueToken == null)
            {
                throw new UnexpectedTokenException(peek);
            }
            else
            {
                return literalValueToken.ToLiteralValueAst(stream);
            }
        }

        #endregion

    }

}
