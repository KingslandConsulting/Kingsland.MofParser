using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Ast
{

    public sealed class NullValueAst : LiteralValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public NullLiteralToken Token
            {
                get;
                set;
            }

            public NullValueAst Build()
            {
                return new NullValueAst(
                    this.Token
                );
            }

        }

        #endregion

        #region Constructors

        private NullValueAst(NullLiteralToken token)
        {
            this.Token = token ?? throw new ArgumentNullException(nameof(token));
        }

        #endregion

        #region Properties

        public NullLiteralToken Token
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
