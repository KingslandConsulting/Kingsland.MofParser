using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.5 Property declaration
///
///     propertyDeclaration          = [ qualifierList ] ( primitivePropertyDeclaration /
///                                    complexPropertyDeclaration /
///                                    enumPropertyDeclaration /
///                                    referencePropertyDeclaration) ";"
///
///     primitivePropertyDeclaration = primitiveType propertyName [ array ]
///                                    [ "=" primitiveTypeValue]
///
///     complexPropertyDeclaration   = structureOrClassName propertyName [ array ]
///                                    [ "=" ( complexTypeValue / aliasIdentifier ) ]
///
///     enumPropertyDeclaration      = enumName propertyName [ array ]
///                                    [ "=" enumTypeValue]
///
///     referencePropertyDeclaration = classReference propertyName [ array ]
///                                    [ "=" referenceTypeValue ]
///
///     array                        = "[" "]"
///     propertyName                 = IDENTIFIER
///     structureOrClassName         = IDENTIFIER
///
///     classReference               = DT_REFERENCE
///     DT_REFERENCE                 = className REF
///     REF                          = "ref" ; keyword: case insensitive
///
/// </remarks>
public sealed record PropertyDeclarationAst : AstNode, IStructureFeatureAst
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.QualifierList = new QualifierListAst();
        }

        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        public IdentifierToken? ReturnType
        {
            get;
            set;
        }

        public IdentifierToken? ReturnTypeRef
        {
            get;
            set;
        }

        public IdentifierToken?PropertyName
        {
            get;
            set;
        }

        public bool ReturnTypeIsArray
        {
            get;
            set;
        }

        public PropertyValueAst? Initializer
        {
            get;
            set;
        }

        public PropertyDeclarationAst Build()
        {
            return new PropertyDeclarationAst(
                this.QualifierList,
                this.ReturnType ?? throw new InvalidOperationException(
                    $"{nameof(this.ReturnType)} property must be set before calling {nameof(Build)}."
                ),
                this.ReturnTypeRef,
                this.PropertyName ?? throw new InvalidOperationException(
                    $"{nameof(this.PropertyName)} property must be set before calling {nameof(Build)}."
                ),
                this.ReturnTypeIsArray,
                this.Initializer
            );
        }

    }

    #endregion

    #region Constructors

    internal PropertyDeclarationAst(
        QualifierListAst qualifierList,
        IdentifierToken returnType,
        IdentifierToken? returnTypeRef,
        IdentifierToken propertyName,
        bool returnTypeIsArray,
        PropertyValueAst? initializer
    )
    {
        this.QualifierList = qualifierList ?? throw new ArgumentNullException(nameof(qualifierList));
        this.ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
        this.ReturnTypeRef = returnTypeRef;
        this.PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        this.ReturnTypeIsArray = returnTypeIsArray;
        this.Initializer = initializer;
    }

    #endregion

    #region Properties

    public QualifierListAst QualifierList
    {
        get;
    }

    public IdentifierToken ReturnType
    {
        get;
    }

    public bool ReturnTypeIsRef =>
        this.ReturnTypeRef is not null;

    public IdentifierToken? ReturnTypeRef
    {
        get;
    }

    public IdentifierToken PropertyName
    {
        get;
    }

    public bool ReturnTypeIsArray
    {
        get;
    }

    public PropertyValueAst? Initializer
    {
        get;
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return AstMofGenerator.ConvertPropertyDeclarationAst(this);
    }

    #endregion

}
