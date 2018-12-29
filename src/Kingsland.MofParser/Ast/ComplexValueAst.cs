using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    public sealed class ComplexValueAst : ComplexTypeValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public QualifierListAst Qualifiers
            {
                get;
                set;
            }

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
                    this.Qualifiers ?? new QualifierListAst.Builder().Build(),
                    this.IsAlias,
                    this.IsValue,
                    this.Alias,
                    this.TypeName,
                    this.Properties ?? new PropertyValueListAst.Builder().Build()
                );
            }

        }

        #endregion

        #region Constructors

        public ComplexValueAst(
            QualifierListAst qualifiers,
            bool isAlias,
            bool isValue,
            AliasIdentifierToken alias,
            IdentifierToken typeName,
            PropertyValueListAst properties
        ) : base(qualifiers)
        {
            this.IsAlias = isAlias;
            this.IsValue = isValue;
            this.Alias = alias;
            this.TypeName = TypeName;
            this.Properties = properties;
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
