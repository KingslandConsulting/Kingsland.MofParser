using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    public sealed class InstanceValueDeclarationAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
            }

            public IdentifierToken TypeName
            {
                get;
                set;
            }

            public AliasIdentifierToken Alias
            {
                get;
                set;
            }

            public PropertyValueListAst PropertyValues
            {
                get;
                set;
            }

            public InstanceValueDeclarationAst Build()
            {
                return new InstanceValueDeclarationAst(
                    this.TypeName,
                    this.Alias,
                    this.PropertyValues ?? new PropertyValueListAst(
                        new ReadOnlyDictionary<string, PropertyValueAst>(
                            new Dictionary<string, PropertyValueAst>()
                        )
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public InstanceValueDeclarationAst(
            IdentifierToken typeName,
            AliasIdentifierToken alias,
            PropertyValueListAst propertyValues
        )
        {
            this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
            this.Alias = alias;
            this.PropertyValues = propertyValues ?? throw new ArgumentNullException(nameof(propertyValues));
        }

        #endregion

        #region Properties

        public IdentifierToken TypeName
        {
            get;
            private set;
        }

        public AliasIdentifierToken Alias
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
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}
