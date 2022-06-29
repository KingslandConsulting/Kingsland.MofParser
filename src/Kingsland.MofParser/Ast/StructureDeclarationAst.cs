using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.1 Structure declaration
///
///     structureDeclaration = [ qualifierList ] STRUCTURE structureName
///                            [ superStructure ]
///                            "{" *structureFeature "}" ";"
///
///     structureName        = elementName
///
///     superStructure       = ":" structureName
///
///     structureFeature     = structureDeclaration /   ; local structure
///                            enumerationDeclaration / ; local enumeration
///                            propertyDeclaration
///
///     STRUCTURE            = "structure" ; keyword: case insensitive
///
/// </remarks>
public sealed record StructureDeclarationAst : MofProductionAst, IStructureFeatureAst
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.QualifierList = new QualifierListAst();
            this.StructureFeatures = new List<IStructureFeatureAst>();
        }

        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        public IdentifierToken? StructureName
        {
            get;
            set;
        }

        public IdentifierToken? SuperStructure
        {
            get;
            set;
        }

        public List<IStructureFeatureAst> StructureFeatures
        {
            get;
            set;
        }

        public StructureDeclarationAst Build()
        {
            return new StructureDeclarationAst(
                this.QualifierList,
                this.StructureName ?? throw new InvalidOperationException(
                    $"{nameof(this.StructureName)} property must be set before calling {nameof(Build)}."
                ),
                this.SuperStructure,
                this.StructureFeatures
            );
        }

    }

    #endregion

    #region Constructors

    internal StructureDeclarationAst(
        QualifierListAst qualifierList,
        IdentifierToken structureName,
        IdentifierToken? superStructure,
        IEnumerable<IStructureFeatureAst> structureFeatures
    )
    {
        this.QualifierList = qualifierList;
        this.StructureName = structureName;
        this.SuperStructure = superStructure;
        this.StructureFeatures = new ReadOnlyCollection<IStructureFeatureAst>(
            structureFeatures.ToList()
        );
    }

    #endregion

    #region Properties

    public QualifierListAst QualifierList
    {
        get;
        private init;
    }

    public IdentifierToken StructureName
    {
        get;
        private init;
    }

    public IdentifierToken? SuperStructure
    {
        get;
        private init;
    }

    public ReadOnlyCollection<IStructureFeatureAst> StructureFeatures
    {
        get;
        private init;
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return AstMofGenerator.ConvertStructureDeclarationAst(this);
    }

    #endregion

}
