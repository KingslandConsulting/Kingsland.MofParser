using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1.5 Boolean value
    ///
    ///     booleanValue = TRUE / FALSE
    ///
    ///     FALSE        = "false" ; keyword: case insensitive
    ///     TRUE         = "true"  ; keyword: case insensitive
    ///
    /// </remarks>
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

            public BooleanValueAst Build()
            {
                return new BooleanValueAst(
                    this.Token
                );
            }

        }

        #endregion

        #region Constructors

        public BooleanValueAst(BooleanLiteralToken token)
        {
            this.Token = token ?? throw new ArgumentNullException(nameof(token));
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
            get
            {
                return this.Token.Value;
            }
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertBooleanValueAst(this);
        }

        #endregion

    }

}
