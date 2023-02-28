using Kingsland.MofParser.Ast;

namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.2 Complex type value

    public void WriteAstNode(InstanceValueDeclarationAst node)
    {

        // instance of myType as $Alias00000070
        // {
        //     Reference = TRUE;
        // };

        // instance
        this.WriteString(node.Instance.Name);
        this.WriteString(' ');

        // of
        this.WriteString(node.Of.Name);
        this.WriteString(' ');

        // myType
        this.WriteString(node.TypeName.Name);

        if (node.Alias is not null)
        {
            this.WriteString(' ');
            // as
            var @as = node.As?.Name ?? throw new NullReferenceException();
            this.WriteString(@as);
            this.WriteString(' ');
            // $Alias00000070
            this.WriteString("$", node.Alias.Name);
        }
        this.WriteLine();

        // {
        //     Reference = TRUE;
        // }
        this.WriteAstNode(
            node.PropertyValues
        );

        // ;
        this.WriteString(node.StatementEnd.Text ?? ";");

    }

    #endregion

}
