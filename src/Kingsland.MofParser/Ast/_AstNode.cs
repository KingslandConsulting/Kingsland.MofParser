using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Ast
{

    public abstract class AstNode
    {

        #region Constructors

        internal AstNode()
        {
        }

        #endregion

        #region Methods

        internal static bool HandleUnexpectedToken(Token foundToken, bool throwIfError)
        {
            if (throwIfError)
            {
                throw new UnexpectedTokenException(foundToken);
            }
            else
            {
                return false;
            }
        }

        #endregion

    }

}
