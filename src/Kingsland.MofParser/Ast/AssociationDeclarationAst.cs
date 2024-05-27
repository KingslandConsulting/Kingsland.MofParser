using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

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
            this.QualifierList = new();
            this.ClassFeatures = [];
        }

        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        public IdentifierToken? AssociationName
        {
            get;
            set;
        }

        public IdentifierToken? SuperAssociation
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
            return new(
                this.QualifierList,
                this.AssociationName ?? throw new InvalidOperationException(
                    $"{nameof(this.AssociationName)} property must be set before calling {nameof(Build)}."
                ),
                this.SuperAssociation,
                this.ClassFeatures
            );
        }

    }

    #endregion

    #region Constructors

    internal AssociationDeclarationAst(
        QualifierListAst qualifierList,
        IdentifierToken associationName,
        IdentifierToken? superAssociation,
        IEnumerable<IClassFeatureAst> classFeatures
    )
    {
        this.QualifierList = qualifierList ?? throw new ArgumentNullException(nameof(qualifierList));
        this.AssociationName = associationName ?? throw new ArgumentNullException(nameof(associationName));
        this.SuperAssociation = superAssociation;
        this.ClassFeatures = (classFeatures ?? throw new ArgumentNullException(nameof(classFeatures)))
            .ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    public QualifierListAst QualifierList
    {
        get;
    }

    public IdentifierToken AssociationName
    {
        get;
    }

    public IdentifierToken? SuperAssociation
    {
        get;
    }

    public ReadOnlyCollection<IClassFeatureAst> ClassFeatures
    {
        get;
    }

    #endregion

}
