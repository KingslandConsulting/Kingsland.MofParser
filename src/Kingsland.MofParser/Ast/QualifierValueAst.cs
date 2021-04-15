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
    public sealed record QualifierValueAst : AstNode
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

            public IQualifierInitializerAst Initializer
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
                    this.Initializer,
                    this.Flavors
                );
            }

        }

        #endregion

        #region Constructors

        internal QualifierValueAst(
            IdentifierToken qualifierName,
            IQualifierInitializerAst initializer,
            IEnumerable<IdentifierToken> flavors)
        {
            this.QualifierName = qualifierName ?? throw new ArgumentNullException(nameof(qualifierName));
            this.Initializer = initializer;
            this.Flavors = new ReadOnlyCollection<IdentifierToken>(
                flavors?.ToList() ?? new List<IdentifierToken>()
            );
        }

        #endregion

        #region Properties

        public IdentifierToken QualifierName
        {
            get;
            private init;
        }

        public IQualifierInitializerAst Initializer
        {
            get;
            private init;
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
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertQualifierValueAst(this);
        }

        #endregion

    }

}
