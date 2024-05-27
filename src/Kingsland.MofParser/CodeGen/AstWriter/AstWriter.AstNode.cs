using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(AstNode node)
    {
        switch (node)
        {
            case AssociationDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case BooleanValueAst ast:
                this.WriteAstNode(ast);
                break;
            case ClassDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case CompilerDirectiveAst ast:
                this.WriteAstNode(ast);
                break;
            case ComplexValueArrayAst ast:
                this.WriteAstNode(ast);
                break;
            case ComplexValueAst ast:
                this.WriteAstNode(ast);
                break;
            case EnumValueArrayAst ast:
                this.WriteAstNode(ast);
                break;
            case EnumValueAst ast:
                this.WriteAstNode(ast);
                break;
            case InstanceValueDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case IntegerValueAst ast:
                this.WriteAstNode(ast);
                break;
            case LiteralValueArrayAst ast:
                this.WriteAstNode(ast);
                break;
            case MethodDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case MofSpecificationAst ast:
                this.WriteAstNode(ast);
                break;
            case NullValueAst ast:
                this.WriteAstNode(ast);
                break;
            case ParameterDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case PropertyDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case PropertySlotAst ast:
                this.WriteAstNode(ast);
                break;
            case PropertyValueListAst ast:
                this.WriteAstNode(ast);
                break;
            case QualifierListAst ast:
                this.WriteAstNode(ast);
                break;
            case QualifierTypeDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case QualifierValueAst ast:
                this.WriteAstNode(ast);
                break;
            case RealValueAst ast:
                this.WriteAstNode(ast);
                break;
            case ReferenceTypeValueAst ast:
                this.WriteAstNode(ast);
                break;
            case StringValueAst ast:
                this.WriteAstNode(ast);
                break;
            case StructureDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case StructureValueDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            default:
                throw new NotImplementedException(
                    $"Node type '{node.GetType().Name}' is not implemented."
                );
        }
    }

}
