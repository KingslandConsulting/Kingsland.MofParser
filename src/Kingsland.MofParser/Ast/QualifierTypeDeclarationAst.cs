using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <returns>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.4 Qualifiers
    ///
    ///     qualifierTypeDeclaration = [ qualifierList ] QUALIFIER qualifierName ":"
    ///                                qualifierType qualifierScope
    ///                                [ qualifierPolicy ] ";"
    ///
    ///     qualifierName            = elementName
    ///
    ///     qualifierType            = primitiveQualifierType / enumQualiferType
    ///
    ///     primitiveQualifierType   = primitiveType [ array ]
    ///                                [ "=" primitiveTypeValue ] ";"
    ///
    ///     enumQualiferType         = enumName [ array ] "=" enumTypeValue ";"
    ///
    ///     qualifierScope           = SCOPE "(" ANY / scopeKindList ")"
    ///
    /// </returns>
    public sealed record QualifierTypeDeclarationAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Flavors = new List<string>();
            }

            public QualifierListAst QualifierList
            {
                get;
                set;
            }

            public IdentifierToken QualifierKeyword
            {
                get;
                set;
            }

            public IdentifierToken QualifierName
            {
                get;
                set;
            }

            public IdentifierToken QualifierType
            {
                get;
                set;
            }

            public IdentifierToken QualifierScope
            {
                get;
                set;
            }

            public IdentifierToken QualifierPolicy
            {
                get;
                set;
            }

            public List<string> Flavors
            {
                get;
                set;
            }

            public QualifierTypeDeclarationAst Build()
            {
                return new QualifierTypeDeclarationAst(
                    this.QualifierList,
                    this.QualifierKeyword,
                    this.QualifierName,
                    this.QualifierType,
                    this.QualifierScope,
                    this.QualifierPolicy,
                    this.Flavors
                );
            }

        }

        #endregion

        #region Constructors

        internal QualifierTypeDeclarationAst(
            QualifierListAst qualifierList,
            IdentifierToken qualifierKeyword,
            IdentifierToken qualifierName,
            IdentifierToken qualifierType,
            IdentifierToken qualifierScope,
            IdentifierToken qualifierPolicy,
            IEnumerable<string> flavors
        )
        {
            this.QualifierList = qualifierList ?? new QualifierListAst.Builder().Build();
            this.QualifierKeyword = qualifierKeyword ?? throw new ArgumentNullException(nameof(qualifierKeyword));
            this.QualifierName = qualifierName ?? throw new ArgumentNullException(nameof(qualifierName));
            this.QualifierType = qualifierType ?? throw new ArgumentNullException(nameof(qualifierType));
            this.QualifierScope = qualifierScope ?? throw new ArgumentNullException(nameof(qualifierScope));
            this.QualifierPolicy = qualifierPolicy ?? throw new ArgumentNullException(nameof(qualifierPolicy));
            this.Flavors = new ReadOnlyCollection<string>(
                flavors?.ToList() ?? new List<string>()
            );
        }

        #endregion

        #region Properties

        public QualifierListAst QualifierList
        {
            get;
            private init;
        }

        public IdentifierToken QualifierKeyword
        {
            get;
            private init;
        }

        public IdentifierToken QualifierName
        {
            get;
            private init;
        }

        public IdentifierToken QualifierType
        {
            get;
            private init;
        }

        public IdentifierToken QualifierScope
        {
            get;
            private init;
        }

        public IdentifierToken QualifierPolicy
        {
            get;
            private init;
        }

        public ReadOnlyCollection<string> Flavors
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertQualifierTypeDeclarationAst(this);
        }

        #endregion

    }

}
