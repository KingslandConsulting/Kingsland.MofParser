using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1.3 String values
    ///
    ///     singleStringValue = DOUBLEQUOTE *stringChar DOUBLEQUOTE
    ///
    ///     stringValue       = singleStringValue *( *WS singleStringValue )
    ///
    /// </remarks>
    public sealed record StringValueAst : LiteralValueAst, IEnumElementValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.StringLiteralValues = new List<StringLiteralToken>();
            }

            public List<StringLiteralToken> StringLiteralValues
            {
                get;
                set;
            }

            public string Value
            {
                get;
                set;
            }

            public StringValueAst Build()
            {
                return new StringValueAst(
                    new ReadOnlyCollection<StringLiteralToken>(this.StringLiteralValues),
                    this.Value
                );
            }

        }

        #endregion

        #region Constructors

        public StringValueAst(ReadOnlyCollection<StringLiteralToken> stringLiteralValues, string value)
        {
            if (stringLiteralValues == null)
            {
                throw new ArgumentNullException(nameof(stringLiteralValues));
            }
            if (stringLiteralValues.Count == 0)
            {
                throw new ArgumentException(nameof(stringLiteralValues));
            }
            this.StringLiteralValues = stringLiteralValues;
            this.Value = value;
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<StringLiteralToken> StringLiteralValues
        {
            get;
            private init;
        }

        public string Value
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertStringValueAst(this);
        }

        #endregion

    }

}
