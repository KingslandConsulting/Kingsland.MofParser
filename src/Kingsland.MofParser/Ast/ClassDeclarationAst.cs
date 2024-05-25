using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.2 Class declaration
///
///     classDeclaration = [ qualifierList ] CLASS className [ superClass ]
///                        "{" *classFeature "}" ";"
///
///     className        = elementName
///
///     superClass       = ":" className
///
///     classFeature     = structureFeature /
///                        methodDeclaration
///
///     CLASS            = "class" ; keyword: case insensitive
///
/// </remarks>
public sealed record ClassDeclarationAst : MofProductionAst
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

        public IdentifierToken? ClassName
        {
            get;
            set;
        }

        public IdentifierToken? SuperClass
        {
            get;
            set;
        }

        public List<IClassFeatureAst> ClassFeatures
        {
            get;
            set;
        }

        public ClassDeclarationAst Build()
        {
            return new(
                this.QualifierList,
                this.ClassName ?? throw new InvalidOperationException(
                    $"{nameof(this.ClassName)} property must be set before calling {nameof(Build)}."
                ),
                this.SuperClass,
                this.ClassFeatures
            );
        }

    }

    #endregion

    #region Constructors

    internal ClassDeclarationAst(
        QualifierListAst qualifierList,
        IdentifierToken className,
        IdentifierToken? superClass,
        IEnumerable<IClassFeatureAst> classFeatures
    )
    {
        this.QualifierList = qualifierList ?? throw new ArgumentNullException(nameof(qualifierList));
        this.ClassName = className ?? throw new ArgumentNullException(nameof(className));
        this.SuperClass = superClass;
        this.ClassFeatures = (classFeatures ?? throw new ArgumentNullException(nameof(classFeatures)))
            .ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    public QualifierListAst QualifierList
    {
        get;
    }

    public IdentifierToken ClassName
    {
        get;
    }

    public IdentifierToken? SuperClass
    {
        get;
    }

    public ReadOnlyCollection<IClassFeatureAst> ClassFeatures
    {
        get;
    }

    #endregion

}
