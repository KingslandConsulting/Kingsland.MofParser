using Kingsland.MofParser.Tokens;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.6 Method declaration
///
///     methodDeclaration = [ qualifierList ]
///                         ( ( returnDataType [ array ] ) / VOID ) methodName
///                         "(" [ parameterList ] ")" ";"
///
///     returnDataType    = primitiveType /
///                         structureOrClassName /
///                         enumName /
///                         classReference
///
///     methodName        = IDENTIFIER
///     classReference    = DT_REFERENCE
///     DT_REFERENCE      = className REF
///     VOID              = "void" ; keyword: case insensitive
///     parameterList     = parameterDeclaration *( "," parameterDeclaration )
///
/// 7.5.5 Property declaration
///
///    array             = "[" "]"
///
/// </remarks>
public sealed record MethodDeclarationAst : AstNode, IClassFeatureAst
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.QualifierList = new();
            this.Parameters = [];
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

        public bool ReturnTypeIsArray
        {
            get;
            set;
        }

        public IdentifierToken? MethodName
        {
            get;
            set;
        }

        public List<ParameterDeclarationAst> Parameters
        {
            get;
            set;
        }

        public MethodDeclarationAst Build()
        {
            return new(
                this.QualifierList,
                this.ReturnType ?? throw new InvalidOperationException(
                    $"{nameof(this.ReturnType)} property must be set before calling {nameof(Build)}."
                ),
                this.ReturnTypeRef,
                this.ReturnTypeIsArray,
                this.MethodName ?? throw new InvalidOperationException(
                    $"{nameof(this.MethodName)} property must be set before calling {nameof(Build)}."
                ),
                this.Parameters
            );
        }

    }

    #endregion

    #region Constructors

    internal MethodDeclarationAst(
        QualifierListAst qualifierList,
        IdentifierToken returnType,
        IdentifierToken? returnTypeRef,
        bool returnTypeIsArray,
        IdentifierToken methodName,
        IEnumerable<ParameterDeclarationAst> parameters
    )
    {
        this.QualifierList = qualifierList ?? throw new ArgumentNullException(nameof(qualifierList));
        this.ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
        this.ReturnTypeRef = returnTypeRef;
        this.ReturnTypeIsArray = returnTypeIsArray;
        this.Name = methodName ?? throw new ArgumentNullException(nameof(methodName));
        this.Parameters = (parameters ?? throw new ArgumentNullException(nameof(parameters)))
            .ToList().AsReadOnly();
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

    public bool ReturnTypeIsArray
    {
        get;
    }

    public IdentifierToken Name
    {
        get;
    }

    public ReadOnlyCollection<ParameterDeclarationAst> Parameters
    {
        get;
    }

    #endregion

}
