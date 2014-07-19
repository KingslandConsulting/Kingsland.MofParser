using System;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public abstract class PrimitiveTypeValueAst : AstNode
    {

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
        internal static PrimitiveTypeValueAst Parse(ParserStream stream)
        {
            var peek = stream.Peek();
            // primitiveTypeValue = literalValue / literalValueArray
            if (LiteralValueAst.IsLiteralValueToken(peek))
            {
                // primitiveTypeValue
                return LiteralValueAst.Parse(stream);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        
    }

}
