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
    /// 7.5.9 Complex type value
    ///
    ///     complexValue = aliasIdentifier /
    ///                    ( VALUE OF
    ///                      ( structureName / className / associationName )
    ///                      propertyValueList )
    ///
    /// </remarks>
    public sealed class ComplexValueAst : ComplexTypeValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public AliasIdentifierToken Alias
            {
                get;
                set;
            }

            public IdentifierToken Value
            {
                get;
                set;
            }

            public IdentifierToken Of
            {
                get;
                set;
            }

            public IdentifierToken TypeName
            {
                get;
                set;
            }

            public PropertyValueListAst PropertyValues
            {
                get;
                set;
            }

            public ComplexValueAst Build()
            {
                if (!(this.Alias == null))
                {
                    return new ComplexValueAst(
                        this.Alias
                    );
                }
                else
                {
                    return new ComplexValueAst(
                        this.Value,
                        this.Of,
                        this.TypeName,
                        this.PropertyValues
                    );
                }
            }

        }

        #endregion

        #region Constructors

        public ComplexValueAst(
            AliasIdentifierToken alias
        )
        {
            this.Alias = alias ?? throw new ArgumentException(nameof(alias));
            this.Value = null;
            this.Of = null;
            this.TypeName = null;
            this.PropertyValues = new PropertyValueListAst.Builder().Build();
        }

        public ComplexValueAst(
            IdentifierToken value,
            IdentifierToken of,
            IdentifierToken typeName,
            PropertyValueListAst propertyValues
        )
        {
            this.Alias = null;
            this.Value = value ?? throw new ArgumentException(nameof(value));
            this.Of = of;
            this.TypeName = typeName;
            this.PropertyValues = propertyValues ?? new PropertyValueListAst.Builder().Build();
        }

        #endregion

        #region Properties

        public bool IsAlias
        {
            get
            {
                return !(this.Alias == null);
            }
        }

        public bool IsValue
        {
            get
            {
                return !(this.Value == null);
            }
        }

        public AliasIdentifierToken Alias
        {
            get;
            private set;
        }

        public IdentifierToken Value
        {
            get;
            private set;
        }

        public IdentifierToken Of
        {
            get;
            private set;
        }

        public IdentifierToken TypeName
        {
            get;
            private set;
        }

        public PropertyValueListAst PropertyValues
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertComplexValueAst(this);
        }

        #endregion

    }

}
