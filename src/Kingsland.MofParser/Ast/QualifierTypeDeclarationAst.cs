using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

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
            this.QualifierList = new QualifierListAst();
            this.Flavors = new List<string>();
        }

        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        public IdentifierToken? QualifierKeyword
        {
            get;
            set;
        }

        public IdentifierToken? QualifierName
        {
            get;
            set;
        }

        public IdentifierToken? QualifierType
        {
            get;
            set;
        }

        public IdentifierToken? QualifierScope
        {
            get;
            set;
        }

        public IdentifierToken? QualifierPolicy
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
                this.QualifierKeyword ?? throw new InvalidOperationException(
                    $"{nameof(this.QualifierKeyword)} property must be set before calling {nameof(Build)}."
                ),
                this.QualifierName ?? throw new InvalidOperationException(
                    $"{nameof(this.QualifierName)} property must be set before calling {nameof(Build)}."
                ),
                this.QualifierType ?? throw new InvalidOperationException(
                    $"{nameof(this.QualifierType)} property must be set before calling {nameof(Build)}."
                ),
                this.QualifierScope ?? throw new InvalidOperationException(
                    $"{nameof(this.QualifierScope)} property must be set before calling {nameof(Build)}."
                ),
                this.QualifierPolicy ?? throw new InvalidOperationException(
                    $"{nameof(this.QualifierPolicy)} property must be set before calling {nameof(Build)}."
                ),
                this.Flavors
            );
        }

    }

    #endregion

    #region Constructors

    internal QualifierTypeDeclarationAst(
        QualifierListAst? qualifierList,
        IdentifierToken qualifierKeyword,
        IdentifierToken qualifierName,
        IdentifierToken qualifierType,
        IdentifierToken qualifierScope,
        IdentifierToken qualifierPolicy,
        IEnumerable<string> flavors
    )
    {
        this.QualifierList = qualifierList ?? new QualifierListAst();
        this.QualifierKeyword = qualifierKeyword;
        this.QualifierName = qualifierName;
        this.QualifierType = qualifierType;
        this.QualifierScope = qualifierScope;
        this.QualifierPolicy = qualifierPolicy;
        this.Flavors = new ReadOnlyCollection<string>(
            flavors.ToList()
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
