using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;
using System;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
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
    /// 7.5.6 Method declaration
    ///
    ///     classReference            = DT_REFERENCE
    ///
    /// 7.5.10 Reference type declaration
    ///
    ///     DT_REFERENCE              = className REF
    ///
    /// </remarks>
    public sealed class ParameterDeclarationAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public QualifierListAst QualifierList
            {
                get;
                set;
            }

            public IdentifierToken ParameterType
            {
                get;
                set;
            }

            public IdentifierToken ParameterRef
            {
                get;
                set;
            }

            public IdentifierToken ParameterName
            {
                get;
                set;
            }

            public bool ParameterIsArray
            {
                get;
                set;
            }

            public PropertyValueAst DefaultValue
            {
                get;
                set;
            }

            public ParameterDeclarationAst Build()
            {
                return new ParameterDeclarationAst(
                    this.QualifierList,
                    this.ParameterType,
                    this.ParameterRef,
                    this.ParameterName,
                    this.ParameterIsArray,
                    this.DefaultValue
                );
            }

        }

        #endregion

        #region Constructors

        private ParameterDeclarationAst(
            QualifierListAst qualifierList,
            IdentifierToken parameterType,
            IdentifierToken parameterRef,
            IdentifierToken parameterName,
            bool parameterIsArray,
            PropertyValueAst defaultValue
        )
        {
            this.QualifierList = qualifierList ?? new QualifierListAst.Builder().Build();
            this.ParameterType = parameterType ?? throw new ArgumentNullException(nameof(parameterType));
            this.ParameterRef = parameterRef;
            this.ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
            this.ParameterIsArray = parameterIsArray;
            this.DefaultValue = defaultValue;
        }

        #endregion

        #region Properties

        public QualifierListAst QualifierList
        {
            get;
            private set;
        }

        public IdentifierToken ParameterType
        {
            get;
            private set;
        }

        public IdentifierToken ParameterName
        {
            get;
            private set;
        }

        public bool ParameterIsRef
        {
            get
            {
                return (this.ParameterRef != null);
            }
        }

        public IdentifierToken ParameterRef
        {
            get;
            private set;
        }

        public bool ParameterIsArray
        {
            get;
            private set;
        }

        public PropertyValueAst DefaultValue
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertParameterDeclarationAst(this);
        }

        #endregion

    }

}

