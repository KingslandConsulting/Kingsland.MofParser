using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;

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
    ///     complexValue      = aliasIdentifier /
    ///                         ( VALUE OF
    ///                           ( structureName / className / associationName )
    ///                           propertyValueList )
    ///
    /// </remarks>
    public sealed class ComplexValueAst : ComplexTypeValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public bool IsAlias
            {
                get;
                set;
            }

            public bool IsValue
            {
                get;
                set;
            }

            public AliasIdentifierToken Alias
            {
                get;
                set;
            }

            public IdentifierToken TypeName
            {
                get;
                set;
            }

            public PropertyValueListAst Properties
            {
                get;
                set;
            }

            public ComplexValueAst Build()
            {
                return new ComplexValueAst(
                    this.IsAlias,
                    this.IsValue,
                    this.Alias,
                    this.TypeName,
                    this.Properties
                );
            }

        }

        #endregion

        #region Constructors

        public ComplexValueAst(
            bool isAlias,
            bool isValue,
            AliasIdentifierToken alias,
            IdentifierToken typeName,
            PropertyValueListAst properties
        )
        {
            this.IsAlias = isAlias;
            this.IsValue = isValue;
            this.Alias = alias;
            this.TypeName = TypeName;
            this.Properties = properties ?? new PropertyValueListAst.Builder().Build();
        }

        #endregion

        #region Properties

        public bool IsAlias
        {
            get;
            private set;
        }

        public bool IsValue
        {
            get;
            private set;
        }

        public AliasIdentifierToken Alias
        {
            get;
            private set;
        }

        public IdentifierToken TypeName
        {
            get;
            private set;
        }

        public PropertyValueListAst Properties
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
