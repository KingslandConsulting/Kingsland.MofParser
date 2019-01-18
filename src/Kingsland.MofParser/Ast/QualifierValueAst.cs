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
    /// 7.4.1 QualifierList
    ///
    ///     qualifierValue                = qualifierName [ qualifierValueInitializer /
    ///                                     qualiferValueArrayInitializer ]
    ///
    ///     qualifierValueInitializer     = "(" literalValue ")"
    ///
    ///     qualiferValueArrayInitializer = "{" literalValue *( "," literalValue ) "}"
    ///
    /// </remarks>
    public sealed class QualifierValueAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Flavors = new List<IdentifierToken>();
            }

            public IdentifierToken QualifierName
            {
                get;
                set;
            }

            public LiteralValueAst ValueInitializer
            {
                get;
                set;
            }

            public LiteralValueArrayAst ValueArrayInitializer
            {
                get;
                set;
            }

            public List<IdentifierToken> Flavors
            {
                get;
                set;
            }

            public QualifierValueAst Build()
            {
                return new QualifierValueAst(
                    this.QualifierName,
                    this.ValueInitializer,
                    this.ValueArrayInitializer,
                    new ReadOnlyCollection<IdentifierToken>(
                        this.Flavors ?? new List<IdentifierToken>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public QualifierValueAst(IdentifierToken qualifierName, LiteralValueAst valueInitializer, LiteralValueArrayAst valueArrayInitializer, ReadOnlyCollection<IdentifierToken> flavors)
        {
            this.QualifierName = qualifierName ?? throw new ArgumentNullException(nameof(qualifierName));
            this.ValueInitializer = valueInitializer;
            this.ValueArrayInitializer = valueArrayInitializer;
            this.Flavors = flavors ?? new ReadOnlyCollection<IdentifierToken>(
                new List<IdentifierToken>()
            );
        }

        #endregion

        #region Properties

        public IdentifierToken QualifierName
        {
            get;
            private set;
        }

        public LiteralValueAst ValueInitializer
        {
            get;
            private set;
        }

        public LiteralValueArrayAst ValueArrayInitializer
        {
            get;
            private set;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///
        /// 7.4 Qualifiers
        ///
        /// NOTE A MOF v2 qualifier declaration has to be converted to MOF v3 qualifierTypeDeclaration because the
        /// MOF v2 qualifier flavor has been replaced by the MOF v3 qualifierPolicy.
        ///
        /// These aren't part of the MOF 3.0.1 spec, but we'll include them anyway for backward compatibility.
        ///
        /// </remarks>
        public ReadOnlyCollection<IdentifierToken> Flavors
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertQualifierValueAst(this);
        }

        #endregion

    }

}
