using Kingsland.MofParser.CodeGen;
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

            public Builder()
            {
                this.Properties = new Dictionary<string, PropertyValueAst>();
            }

            public QualifierListAst Qualifiers
            {
                get;
                set;
            }

            public bool IsInstance
            {
                get;
                set;
            }

            public bool IsValue
            {
                get;
                set;
            }

            public string TypeName
            {
                get;
                set;
            }

            public string Alias
            {
                get;
                set;
            }

            public Dictionary<string, PropertyValueAst> Properties
            {
                get;
                set;
            }

            public ComplexValueAst Build()
            {
                return new ComplexValueAst(
                    this.Qualifiers ?? new QualifierListAst.Builder().Build(),
                    this.IsInstance,
                    this.IsValue,
                    this.TypeName,
                    this.Alias,
                    new ReadOnlyDictionary<string, PropertyValueAst>(
                        this.Properties ?? new Dictionary<string, PropertyValueAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        private ComplexValueAst(
            QualifierListAst qualifiers,
            bool isInstance,
            bool isValue,
            string typeName,
            string alias,
            ReadOnlyDictionary<string, PropertyValueAst> properties
        ) : base(qualifiers)
        {
            this.IsInstance = isInstance;
            this.IsValue = isValue;
            this.TypeName = typeName;
            this.Alias = alias;
            this.Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        #endregion

        #region Properties

        public bool IsInstance
        {
            get;
            private set;
        }

        public bool IsValue
        {
            get;
            private set;
        }

        public string TypeName
        {
            get;
            private set;
        }

        public string Alias
        {
            get;
            private set;
        }

        public ReadOnlyDictionary<string, PropertyValueAst> Properties
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
