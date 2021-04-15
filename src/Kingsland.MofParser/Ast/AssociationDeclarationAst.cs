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
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.3 Association declaration
    ///
    ///     associationDeclaration = [ qualifierList ] ASSOCIATION associationName
    ///                              [ superAssociation ]
    ///                              "{" *classFeature "}" ";"
    ///
    ///     associationName        = elementName
    ///
    ///     superAssociation       = ":" elementName
    ///
    ///     ASSOCIATION            = "association" ; keyword: case insensitive
    ///
    /// </remarks>
    public sealed record AssociationDeclarationAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.ClassFeatures = new List<IClassFeatureAst>();
            }

            public QualifierListAst QualifierList
            {
                get;
                set;
            }

            public IdentifierToken AssociationName
            {
                get;
                set;
            }

            public IdentifierToken SuperAssociation
            {
                get;
                set;
            }

            public List<IClassFeatureAst> ClassFeatures
            {
                get;
                set;
            }

            public AssociationDeclarationAst Build()
            {
                return new AssociationDeclarationAst(
                    this.QualifierList,
                    this.AssociationName,
                    this.SuperAssociation,
                    this.ClassFeatures
                );
            }

        }

        #endregion

        #region Constructors

        internal AssociationDeclarationAst(QualifierListAst qualifierList, IdentifierToken associationName, IdentifierToken superAssociation, IEnumerable<IClassFeatureAst> classFeatures)
        {
            this.QualifierList = qualifierList ?? new QualifierListAst();
            this.AssociationName = associationName ?? throw new ArgumentNullException(nameof(associationName));
            this.SuperAssociation = superAssociation;
            this.ClassFeatures = new ReadOnlyCollection<IClassFeatureAst>(
                classFeatures?.ToList() ?? new List<IClassFeatureAst>()
            );
        }

        #endregion

        #region Properties

        public QualifierListAst QualifierList
        {
            get;
            private init;
        }

        public IdentifierToken AssociationName
        {
            get;
            private init;
        }

        public IdentifierToken SuperAssociation
        {
            get;
            private init;
        }

        public ReadOnlyCollection<IClassFeatureAst> ClassFeatures
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertAssociationDeclarationAst(this);
        }

        #endregion

    }

}
