using Kingsland.MofParser.CodeGen;
using Kingsland.ParseFx.Parsing;
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
    /// 7.5.9 Complex type value
    ///
    ///     propertyValueList = "{" *propertySlot "}"
    ///
    ///     propertySlot      = propertyName "=" propertyValue ";"
    ///
    ///     propertyValue     = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
    ///
    ///     propertyName      = IDENTIFIER
    ///
    /// </remarks>
    public sealed record PropertyValueListAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.PropertyValues = new Dictionary<string, PropertyValueAst>();
            }

            public Dictionary<string, PropertyValueAst> PropertyValues
            {
                get;
                set;
            }

            public PropertyValueListAst Build()
            {
                return new PropertyValueListAst(
                    new ReadOnlyDictionary<string, PropertyValueAst>(
                        this.PropertyValues ?? new Dictionary<string, PropertyValueAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public PropertyValueListAst(ReadOnlyDictionary<string, PropertyValueAst> propertyValues)
        {
            this.PropertyValues = propertyValues ?? new ReadOnlyDictionary<string, PropertyValueAst>(
                new Dictionary<string, PropertyValueAst>()
            );
        }

        #endregion

        #region Properties

        public ReadOnlyDictionary<string, PropertyValueAst> PropertyValues
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertPropertyValueListAst(this);
        }

        #endregion

    }

}
