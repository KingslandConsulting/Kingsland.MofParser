using System;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public abstract class LiteralValueAst : PrimitiveTypeValueAst
    {

        protected LiteralValueAst()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        ///     primitiveTypeValue = literalValue / literalValueArray
        /// 
        ///     literalValueArray  = "{" [ literalValue *( "," literalValue ) ] "}"
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
            // propertyValue = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
            var peek = stream.Peek();
			if (peek is StringLiteralToken)
			{
				// primitiveTypeValue
				return StringValueAst.Parse(stream);
			}
			else if (peek is BooleanLiteralToken)
			{
				// primitiveTypeValue
				return BooleanValueAst.Parse(stream);
			}
			else if(peek is IntegerLiteralToken)
			{
				// primitiveTypeValue
				return IntegerValueAst.Parse(stream);
			}
			else if (peek is NullLiteralToken)
			{
				return NullValueAst.Parse(stream);
			}

			else
			{
				throw new InvalidOperationException();
			}
        }

        internal static bool IsLiteralValueToken(Token token)
        {
            return (token is StringLiteralToken) ||
                   (token is BooleanLiteralToken) ||
                   (token is IntegerLiteralToken) || 
				   (token is NullLiteralToken);
        }

    }

}
