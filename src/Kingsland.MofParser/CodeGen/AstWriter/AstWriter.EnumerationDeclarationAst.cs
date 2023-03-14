using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.4 Enumeration declaration

    public void WriteAstNode(EnumerationDeclarationAst node)
    {

        // enumeration MonthsEnum : integer
        // {
        //    January = 1,
        //    February = 2
        // };

        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteAstNode(
                node.QualifierList
            );
            this.WriteLine();
            this.WriteIndent();
        }

        // enumeration MonthsEnum : integer
        this.WriteString(Constants.ENUMERATION, " ", node.EnumName.Name, " : ", node.EnumType.Name);

        // {
        this.WriteLine();
        this.WriteIndent();
        this.WriteString("{");

        //    January = 1,
        //    February = 2
        this.Indent();
        for (var i = 0; i < node.EnumElements.Count; i++)
        {
            this.WriteLine();
            this.WriteIndent();
            this.WriteAstNode(
                node.EnumElements[i]
            );
            if (i < (node.EnumElements.Count - 1))
            {
                this.WriteString(',');
            }
        }
        this.Unindent();

        // };
        this.WriteLine();
        this.WriteIndent();
        this.WriteString("};");

    }

    #endregion

}
