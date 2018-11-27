using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Ast
{

    public sealed class PropertyDeclarationAst : StructureFeatureAst
    {

        #region Builder

        public sealed class Builder
        {

            public IdentifierToken Name
            {
                get;
                set;
            }

            public QualifierListAst Qualifiers
            {
                get;
                set;
            }

            public IdentifierToken Type
            {
                get;
                set;
            }

            public bool IsArray
            {
                get;
                set;
            }

            public bool IsRef
            {
                get;
                set;
            }

            public PrimitiveTypeValueAst Initializer
            {
                get;
                set;
            }

            public PropertyDeclarationAst Build()
            {
                return new PropertyDeclarationAst(
                    this.Name,
                    this.Qualifiers,
                    this.Type,
                    this.IsArray,
                    this.IsRef,
                    this.Initializer
                );
            }

        }

        #endregion

        #region Constructors

        private PropertyDeclarationAst(
            IdentifierToken name,
            QualifierListAst qualifiers,
            IdentifierToken type,
            bool isArray,
            bool isRef,
            PrimitiveTypeValueAst initializer
        )
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Qualifiers = qualifiers ?? throw new ArgumentNullException(nameof(qualifiers));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
            this.IsArray = isArray;
            this.IsRef = isRef;
            this.Initializer = initializer ?? throw new ArgumentNullException(nameof(initializer));
        }

        #endregion

        #region Properties

        public IdentifierToken Name
        {
            get;
            private set;
        }

        public QualifierListAst Qualifiers
        {
            get;
            private set;
        }

        public IdentifierToken Type
        {
            get;
            private set;
        }

        public bool IsArray
        {
            get;
            private set;
        }

        public bool IsRef
        {
            get;
            private set;
        }

        public PrimitiveTypeValueAst Initializer
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
