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
    ///     instanceValueDeclaration = INSTANCE OF ( className / associationName )
    ///                                [ alias ]
    ///                                propertyValueList ";"
    ///
    ///     alias                    = AS aliasIdentifier
    ///
    /// </remarks>
    public sealed record InstanceValueDeclarationAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.PropertyValues = new PropertyValueListAst();
            }

            public IdentifierToken? Instance
            {
                get;
                set;
            }

            public IdentifierToken? Of
            {
                get;
                set;
            }

            public IdentifierToken? TypeName
            {
                get;
                set;
            }

            public IdentifierToken? As
            {
                get;
                set;
            }

            public AliasIdentifierToken? Alias
            {
                get;
                set;
            }

            public PropertyValueListAst PropertyValues
            {
                get;
                set;
            }

            public StatementEndToken? StatementEnd
            {
                get;
                set;
            }

            public InstanceValueDeclarationAst Build()
            {
                return new InstanceValueDeclarationAst(
                    instance: this.Instance ?? throw new InvalidOperationException(
                        $"{nameof(this.Instance)} property must be set before calling {nameof(Build)}."
                    ),
                    of: this.Of ?? throw new InvalidOperationException(
                        $"{nameof(this.Of)} property must be set before calling {nameof(Build)}."
                    ),
                    typeName: this.TypeName ?? throw new InvalidOperationException(
                        $"{nameof(this.TypeName)} property must be set before calling {nameof(Build)}."
                    ),
                    @as: this.As,
                    alias: this.Alias,
                    propertyValues: this.PropertyValues,
                    statementEnd: this.StatementEnd ?? throw new InvalidOperationException(
                        $"{nameof(this.StatementEnd)} property must be set before calling {nameof(Build)}."
                    )
                );
            }

        }

        #endregion

        #region Constructors

        internal InstanceValueDeclarationAst(
            IdentifierToken instance,
            IdentifierToken of,
            IdentifierToken typeName,
            IdentifierToken? @as,
            AliasIdentifierToken? alias,
            PropertyValueListAst propertyValues,
            StatementEndToken statementEnd
        )
        {
            this.Instance = instance;
            this.Of = of;
            this.TypeName = typeName;
            if ((@as != null) || (alias != null))
            {
                this.As = @as ?? throw new ArgumentNullException(nameof(@as));
                this.Alias = alias ?? throw new ArgumentNullException(nameof(@alias));
            }
            this.PropertyValues = propertyValues;
            this.StatementEnd = statementEnd;
        }

        #endregion

        #region Properties

        public IdentifierToken Instance
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

        public IdentifierToken? As
        {
            get;
            private init;
        }

        public AliasIdentifierToken? Alias
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
            return AstMofGenerator.ConvertInstanceValueDeclarationAst(this);
        }

        #endregion

    }

}
