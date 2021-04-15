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
    /// 7.6.2 Complex type value
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    ///     structureValueDeclaration = VALUE OF
    ///                                 ( className / associationName / structureName )
    ///                                 alias
    ///                                 propertyValueList ";"
    ///
    ///     alias                     = AS aliasIdentifier
    ///
    /// </remarks>
    public sealed record StructureValueDeclarationAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public IdentifierToken Value
            {
                get;
                set;
            }

            public IdentifierToken Of
            {
                get;
                set;
            }

            public IdentifierToken TypeName
            {
                get;
                set;
            }

            public IdentifierToken As
            {
                get;
                set;
            }

            public AliasIdentifierToken Alias
            {
                get;
                set;
            }

            public PropertyValueListAst PropertyValues
            {
                get;
                set;
            }

            public StatementEndToken StatementEnd
            {
                get;
                set;
            }

            public StructureValueDeclarationAst Build()
            {
                return new StructureValueDeclarationAst(
                    value: this.Value,
                    of: this.Of,
                    typeName: this.TypeName,
                    @as: this.As,
                    alias: this.Alias,
                    propertyValues: this.PropertyValues,
                    statementEnd: this.StatementEnd
                );
            }

        }

        #endregion

        #region Constructors

        public StructureValueDeclarationAst(
            IdentifierToken value,
            IdentifierToken of,
            IdentifierToken typeName,
            IdentifierToken @as,
            AliasIdentifierToken alias,
            PropertyValueListAst propertyValues,
            StatementEndToken statementEnd
        )
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.Of = of ?? throw new ArgumentNullException(nameof(of));
            this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
            this.As = @as;
            this.Alias = alias;
            this.PropertyValues = propertyValues ?? new PropertyValueListAst(
                new ReadOnlyDictionary<string, PropertyValueAst>(
                    new Dictionary<string, PropertyValueAst>()
                )
            );
            this.StatementEnd = statementEnd ?? throw new ArgumentNullException(nameof(statementEnd));
        }

        #endregion

        #region Properties

        public IdentifierToken Value
        {
            get;
            private init;
        }

        public IdentifierToken Of
        {
            get;
            private init;
        }

        public IdentifierToken TypeName
        {
            get;
            private init;
        }

        public IdentifierToken As
        {
            get;
            private init;
        }

        public AliasIdentifierToken Alias
        {
            get;
            private init;
        }

        public PropertyValueListAst PropertyValues
        {
            get;
            private init;
        }

        public StatementEndToken StatementEnd
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertStructureValueDeclarationAst(this);
        }

        #endregion

    }

}
