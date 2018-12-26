using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Ast
{

    public sealed class BooleanValueAst : LiteralValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public BooleanLiteralToken Token
            {
                get;
                set;
            }

            public bool Value
            {
                get;
                set;
            }

            public BooleanValueAst Build()
            {
                return new BooleanValueAst(
                    this.Token,
                    this.Value
                );
            }

        }

        #endregion

        #region Constructors

        internal BooleanValueAst(BooleanLiteralToken token, bool value)
        {
            this.Token = token ?? throw new ArgumentNullException(nameof(token));
            this.Value = value;
        }

        #endregion

        #region Properties

        public BooleanLiteralToken Token
        {
            get;
            private set;
        }

        public bool Value
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
