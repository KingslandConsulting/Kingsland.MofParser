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
    /// 7.6.2 Complex type value
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    ///     structureValueDeclaration = VALUE OF
    ///                                 ( className / associationName / structureName )
    ///                                 alias
    ///                                 propertyValueList ";"
    ///
    ///     alias                     = AS aliasIdentifier
    ///
    /// </remarks>
    public sealed class StructureValueDeclarationAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.PropertyValues = new List<PropertyValueAst>();
            }

            public IdentifierToken TypeName
            {
                get;
                set;
            }

            public IdentifierToken Alias
            {
                get;
                set;
            }

            public List<PropertyValueAst> PropertyValues
            {
                get;
                set;
            }

            public StructureValueDeclarationAst Build()
            {
                return new StructureValueDeclarationAst(
                    this.TypeName,
                    this.Alias,
                    new ReadOnlyCollection<PropertyValueAst>(
                        this.PropertyValues ?? new List<PropertyValueAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public StructureValueDeclarationAst(
            IdentifierToken typeName,
            IdentifierToken alias,
            ReadOnlyCollection<PropertyValueAst> propertyValues
        )
        {
            this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
            this.Alias = alias ?? throw new ArgumentNullException(nameof(alias));
            this.PropertyValues = propertyValues ?? new ReadOnlyCollection<PropertyValueAst>(
                new List<PropertyValueAst>()
            );
        }

        #endregion

        #region Properties

        public IdentifierToken TypeName
        {
            get;
            private set;
        }

        public IdentifierToken Alias
        {
            get;
            private set;
        }

        public ReadOnlyCollection<PropertyValueAst> PropertyValues
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
