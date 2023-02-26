using Kingsland.MofParser.Ast;

namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.2 Complex type value

    public void WriteAstNode(StructureValueDeclarationAst node)
    {

        // value of GOLF_PhoneNumber as $JohnDoesPhoneNo
        // {
        //     AreaCode = { "9", "0", "7" };
        //     Number = { "7", "4", "7", "4", "8", "8", "4" };
        // };

        // value
        this.WriteString(node.Value.Extent.Text);

        // of
        this.WriteString(' ');
        this.WriteString(node.Of.Extent.Text);

        // GOLF_PhoneNumber
        this.WriteString(' ');
        this.WriteString(node.TypeName.Name);
        if (node.Alias is not null)
        {
            // as
            this.WriteString(' ');
            var @as = node.As ?? throw new NullReferenceException();
            this.WriteString(@as.Extent.Text);
            // $JohnDoesPhoneNo
            this.WriteString(" $");
            this.WriteString(node.Alias.Name);
        }

        // {
        //     AreaCode = { "9", "0", "7" };
        //     Number = { "7", "4", "7", "4", "8", "8", "4" };
        // }
        this.WriteLine();
        this.WriteAstNode(
            node.PropertyValues
        );

        // ;
        this.WriteString(node.StatementEnd.Extent.Text);

    }

    #endregion

}
