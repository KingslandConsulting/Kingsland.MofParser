using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{
    public sealed class MethodDeclarationAst : ClassFeatureAst
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

            public IdentifierToken ReturnType
            {
                get;
                set;
            }

            public bool ReturnTypeIsArray
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
                    this.Name,
                    this.Qualifiers,
                    this.ReturnType,
                    this.ReturnTypeIsArray,
                    new ReadOnlyCollection<ParameterDeclarationAst>(
                        this.Parameters ?? new List<ParameterDeclarationAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        private MethodDeclarationAst(
            IdentifierToken name,
            QualifierListAst qualifiers,
            IdentifierToken returnType,
            bool returnTypeIsArray,
            ReadOnlyCollection<ParameterDeclarationAst> parameters
        )
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Qualifiers = qualifiers ?? throw new ArgumentNullException(nameof(qualifiers));
            this.ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
            this.ReturnTypeIsArray = returnTypeIsArray;
            this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
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

        public IdentifierToken ReturnType
        {
            get;
            private set;
        }

        public bool ReturnTypeIsArray
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
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}
