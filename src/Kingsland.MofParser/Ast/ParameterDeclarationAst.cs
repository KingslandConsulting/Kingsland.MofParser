using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.7 Parameter declaration
    ///
    ///     parameterDeclaration      = [ qualifierList ] ( primitiveParamDeclaration /
    ///                                 complexParamDeclaration /
    ///                                 enumParamDeclaration /
    ///                                 referenceParamDeclaration )
    ///
    ///     primitiveParamDeclaration = primitiveType parameterName [ array ]
    ///                                 [ "=" primitiveTypeValue ]
    ///
    ///     complexParamDeclaration   = structureOrClassName parameterName [ array ]
    ///                                 [ "=" ( complexTypeValue / aliasIdentifier ) ]
    ///
    ///     enumParamDeclaration      = enumName parameterName [ array ]
    ///                                 [ "=" enumValue ]
    ///
    ///     referenceParamDeclaration = classReference parameterName [ array ]
    ///                                 [ "=" referenceTypeValue ]
    ///
    ///     parameterName             = IDENTIFIER
    ///
    /// </remarks>
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
            this.Qualifiers = qualifiers ?? new QualifierListAst.Builder().Build();
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
            this.IsRef = isRef;
            this.IsArray = isArray;
            this.DefaultValue = defaultValue;
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
            return MofGenerator.ConvertParameterDeclarationAst(this);
        }

        #endregion

    }

}

