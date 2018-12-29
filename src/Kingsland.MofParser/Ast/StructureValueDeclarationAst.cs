using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    public sealed class StructureValueDeclarationAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.PropertyValues = new List<PropertyValueAst>();
            }

            public IdentifierToken Name
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
                    this.Name,
                    new ReadOnlyCollection<PropertyValueAst>(
                        this.PropertyValues ?? new List<PropertyValueAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public StructureValueDeclarationAst(
            IdentifierToken name,
            ReadOnlyCollection<PropertyValueAst> propertyValues
        )
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.PropertyValues = propertyValues ?? throw new ArgumentNullException(nameof(propertyValues));
        }

        #endregion

        #region Properties

        public IdentifierToken Name
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
