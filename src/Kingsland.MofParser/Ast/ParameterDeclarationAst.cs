using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Ast
{

    public sealed class ParameterDeclarationAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public QualifierListAst Qualifiers
            {
                get;
                set;
            }

            public IdentifierToken Name
            {
                get;
                set;
            }

            public IdentifierToken Type
            {
                get;
                set;
            }

            public bool IsRef
            {
                get;
                set;
            }

            public bool IsArray
            {
                get;
                set;
            }

            public AstNode DefaultValue
            {
                get;
                set;
            }

            public ParameterDeclarationAst Build()
            {
                return new ParameterDeclarationAst(
                    this.Qualifiers,
                    this.Name,
                    this.Type,
                    this.IsRef,
                    this.IsArray,
                    this.DefaultValue
                );
            }

        }

        #endregion

        #region Constructors

        private ParameterDeclarationAst(
            QualifierListAst qualifiers,
            IdentifierToken name,
            IdentifierToken type,
            bool isRef,
            bool isArray,
            AstNode defaultValue
        )
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Qualifiers = qualifiers ?? throw new ArgumentNullException(nameof(qualifiers));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
            this.IsRef = isRef;
            this.IsArray = isArray;
            this.DefaultValue = defaultValue ?? throw new ArgumentNullException(nameof(defaultValue));
        }

        #endregion

        #region Properties

        public QualifierListAst Qualifiers
        {
            get;
            private set;
        }

        public IdentifierToken Name
        {
            get;
            private set;
        }

        public IdentifierToken Type
        {
            get;
            private set;
        }

        public bool IsRef
        {
            get;
            private set;
        }

        public bool IsArray
        {
            get;
            private set;
        }

        public AstNode DefaultValue
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

