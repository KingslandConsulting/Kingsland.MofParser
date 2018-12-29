using Kingsland.MofParser.CodeGen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    public sealed class PropertyValueListAst : AstNode
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
            this.PropertyValues = propertyValues ?? throw new ArgumentNullException(nameof(propertyValues));
        }

        #endregion

        #region Properties

        public ReadOnlyDictionary<string, PropertyValueAst> PropertyValues
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
