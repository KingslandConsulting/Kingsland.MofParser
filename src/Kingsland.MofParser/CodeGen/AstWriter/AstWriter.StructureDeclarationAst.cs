using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.1 Structure declaration

    public void WriteAstNode(StructureDeclarationAst node)
    {

        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteAstNode(
                node.QualifierList
            );
            this.WriteLine();
            this.WriteIndent();
        }

        this.WriteString($"{Constants.STRUCTURE} {node.StructureName.Name}");

        if (node.SuperStructure is not null)
        {
            this.WriteString($" : {node.SuperStructure.Name}");
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
