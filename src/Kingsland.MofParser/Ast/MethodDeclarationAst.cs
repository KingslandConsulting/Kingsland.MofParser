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
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.6 Method declaration
    ///
    ///     methodDeclaration = [ qualifierList ]
    ///                         ( ( returnDataType [ array ] ) / VOID ) methodName
    ///                         "(" [ parameterList ] ")" ";"
    ///
    ///     returnDataType    = primitiveType /
    ///                         structureOrClassName /
    ///                         enumName /
    ///                         classReference
    ///
    ///     methodName        = IDENTIFIER
    ///     classReference    = DT_REFERENCE
    ///     DT_REFERENCE      = className REF
    ///     VOID              = "void" ; keyword: case insensitive
    ///     parameterList     = parameterDeclaration *( "," parameterDeclaration )
    ///
    /// 7.5.5 Property declaration
    ///
    ///    array             = "[" "]"
    ///
    public sealed class MethodDeclarationAst : ClassFeatureAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Parameters = new List<ParameterDeclarationAst>();
            }

            public QualifierListAst Qualifiers
            {
                get;
                set;
            }

            public IdentifierToken ReturnType
            {
                get;
                set;
            }

            public IdentifierToken ReturnTypeRef
            {
                get;
                set;
            }

            public bool ReturnTypeIsArray
            {
                get;
                set;
            }

            public IdentifierToken MethodName
            {
                get;
                set;
            }

            public List<ParameterDeclarationAst> Parameters
            {
                get;
                set;
            }

            public MethodDeclarationAst Build()
            {
                return new MethodDeclarationAst(
                    this.Qualifiers,
                    this.ReturnType,
                    this.ReturnTypeRef,
                    this.ReturnTypeIsArray,
                    this.MethodName,
                    new ReadOnlyCollection<ParameterDeclarationAst>(
                        this.Parameters ?? new List<ParameterDeclarationAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        private MethodDeclarationAst(
            QualifierListAst qualifiers,
            IdentifierToken returnType,
            IdentifierToken returnTypeRef,
            bool returnTypeIsArray,
            IdentifierToken methodName,
            ReadOnlyCollection<ParameterDeclarationAst> parameters
        )
        {
            this.Qualifiers = qualifiers ?? new QualifierListAst.Builder().Build();
            this.ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
            this.ReturnTypeRef = returnTypeRef;
            this.ReturnTypeIsArray = returnTypeIsArray;
            this.Name = methodName ?? throw new ArgumentNullException(nameof(methodName));
            this.Parameters = parameters ?? new ReadOnlyCollection<ParameterDeclarationAst>(
                new List<ParameterDeclarationAst>()
            );
        }

        #endregion

        #region Properties

        public QualifierListAst Qualifiers
        {
            get;
            private set;
        }

        public IdentifierToken ReturnType
        {
            get;
            private set;
        }

        public bool ReturnTypeIsRef
        {
            get
            {
                return (this.ReturnTypeRef != null);
            }
        }

        public IdentifierToken ReturnTypeRef
        {
            get;
            private set;
        }

        public bool ReturnTypeIsArray
        {
            get;
            private set;
        }

        public IdentifierToken Name
        {
            get;
            private set;
        }

        public ReadOnlyCollection<ParameterDeclarationAst> Parameters
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertMethodDeclarationAst(this);
        }

        #endregion

    }

}
