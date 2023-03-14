using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.4 Enumeration declaration

    public void WriteAstNode(EnumElementAst node)
    {

        // enumeration MonthsEnum : integer
        // {
        //    January = 1,
        //    ^^^^^^^^^^^
        //    February = 2
        // };

        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteAstNode(
                node.QualifierList
            );
            this.WriteString(' ');
        }

        //    January,
        this.WriteString(node.EnumElementName.Name);

        if (node.EnumElementValue is not null)
        {
            // =
            this.WriteString(" = ");
            // 1
            this.WriteAstNode(
                node.EnumElementValue
            );
        }

    }

    #endregion

}
