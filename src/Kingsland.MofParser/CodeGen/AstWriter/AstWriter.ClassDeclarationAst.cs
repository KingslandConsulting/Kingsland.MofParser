using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.2 Class declaration

    public void WriteAstNode(ClassDeclarationAst node)
    {

        // class GOLF_Base : GOLF_Superclass
        // {
        //     string InstanceID;
        //     string Caption = Null;
        // };

        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteAstNode(
                node.QualifierList
            );
            this.WriteLine();
            this.WriteIndent();
        }

        // class GOLF_Base : GOLF_Superclass
        this.WriteString($"{Constants.CLASS} {node.ClassName.Name}");
        if (node.SuperClass is not null)
        {
            this.WriteString($" : {node.SuperClass.Name}");
        }

        // {
        this.WriteLine();
        this.WriteIndent();
        this.WriteString("{");

        //     string InstanceID;
        //     string Caption = Null;
        this.Indent();
        foreach (var classFeature in node.ClassFeatures)
        {
            this.WriteLine();
            this.WriteIndent();
            this.WriteAstNode(
                classFeature
            );
        }
        this.Unindent();

        // };
        this.WriteLine();
        this.WriteIndent();
        this.WriteString("};");
    }

    #endregion

}
