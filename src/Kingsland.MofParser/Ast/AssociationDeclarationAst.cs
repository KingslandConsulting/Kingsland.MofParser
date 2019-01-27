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
    public sealed class AssociationDeclarationAst : MofProductionAst
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
                    new ReadOnlyCollection<IClassFeatureAst>(
                        this.ClassFeatures ?? new List<IClassFeatureAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public AssociationDeclarationAst(QualifierListAst qualifierList, IdentifierToken associationName, IdentifierToken superAssociation, ReadOnlyCollection<IClassFeatureAst> classFeatures)
        {
            this.QualifierList = qualifierList ?? new QualifierListAst.Builder().Build();
            this.AssociationName = associationName ?? throw new ArgumentNullException(nameof(associationName));
            this.SuperAssociation = superAssociation;
            this.ClassFeatures = classFeatures ?? new ReadOnlyCollection<IClassFeatureAst>(new List<IClassFeatureAst>());
        }

        #endregion

        #region Properties

        public QualifierListAst QualifierList
        {
            get;
            private set;
        }

        public IdentifierToken AssociationName
        {
            get;
            private set;
        }

        public IdentifierToken SuperAssociation
        {
            get;
            private set;
        }

        public ReadOnlyCollection<IClassFeatureAst> ClassFeatures
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertAssociationDeclarationAst(this);
        }

        #endregion

    }

}
