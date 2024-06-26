﻿using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.1 Structure declaration

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(StructureDeclarationAst node)
    {

        if (node.QualifierList.QualifierValues.Count > 0)
        {
            this.WriteAstNode(
                node.QualifierList
            );
            this.WriteLine();
            this.WriteIndent();
        }

        this.WriteString(Constants.STRUCTURE, " ", node.StructureName.Name);

        if (node.SuperStructure is not null)
        {
            this.WriteString(" : ", node.SuperStructure.Name);
        }

        this.WriteLine();
        this.WriteIndent();
        this.WriteString("{");
        
        this.Indent();
        foreach (var structureFeature in node.StructureFeatures)
        {
            this.WriteLine();
            this.WriteIndent();
            this.WriteAstNode(
                structureFeature
            );
        }
        this.Unindent();

        this.WriteLine();
        this.WriteIndent();
        this.WriteString("};");
    }

    #endregion

}
