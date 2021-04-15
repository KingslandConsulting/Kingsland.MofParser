using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
    public sealed record MethodDeclarationAst : AstNode, IClassFeatureAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Parameters = new List<ParameterDeclarationAst>();
            }

            public QualifierListAst QualifierList
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
                    this.QualifierList,
                    this.ReturnType,
                    this.ReturnTypeRef,
                    this.ReturnTypeIsArray,
                    this.MethodName,
                    this.Parameters
                );
            }

        }

        #endregion

        #region Constructors

        internal MethodDeclarationAst(
            QualifierListAst qualifierList,
            IdentifierToken returnType,
            IdentifierToken returnTypeRef,
            bool returnTypeIsArray,
            IdentifierToken methodName,
            IEnumerable<ParameterDeclarationAst> parameters
        )
        {
            this.QualifierList = qualifierList ?? new QualifierListAst.Builder().Build();
            this.ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
            this.ReturnTypeRef = returnTypeRef;
            this.ReturnTypeIsArray = returnTypeIsArray;
            this.Name = methodName ?? throw new ArgumentNullException(nameof(methodName));
            this.Parameters = new ReadOnlyCollection<ParameterDeclarationAst>(
                parameters?.ToList() ?? new List<ParameterDeclarationAst>()
            );
        }

        #endregion

        #region Properties

        public QualifierListAst QualifierList
        {
            get;
            private init;
        }

        public IdentifierToken ReturnType
        {
            get;
            private init;
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
            private init;
        }

        public bool ReturnTypeIsArray
        {
            get;
            private init;
        }

        public IdentifierToken Name
        {
            get;
            private init;
        }

        public ReadOnlyCollection<ParameterDeclarationAst> Parameters
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertMethodDeclarationAst(this);
        }

        #endregion

    }

}
