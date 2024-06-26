﻿using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.2 MOF specification

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(MofProductionAst node)
    {
        switch (node)
        {
            case CompilerDirectiveAst ast:
                this.WriteAstNode(ast);
                break;
            case StructureDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case ClassDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case AssociationDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case EnumerationDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case InstanceValueDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case StructureValueDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case QualifierTypeDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

}
