using Kingsland.MofParser.Ast;

namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.9 Complex type value

    public void WriteAstNode(ComplexValueAst node)
    {

        if (node.IsAlias)
        {

            // instance of GOLF_ClubMember
            // {
            //     LastPaymentDate = $MyAliasIdentifier;
            //                       ^^^^^^^^^^^^^^^^^^^
            // };

            // $MyAliasIdentifier
            var alias = node.Alias ?? throw new NullReferenceException();
            this.WriteString($"${alias.Name}");

        }
        else
        {

            // value of GOLF_PhoneNumber
            // {
            //     AreaCode = { "9", "0", "7" };
            //     Number = { "7", "4", "7", "4", "8", "8", "4" };
            // }

            // value
            var value = node.Value ?? throw new NullReferenceException();
            this.WriteString(value.Extent.Text);
            this.WriteString(' ');

            // of
            var of = node.Of ?? throw new NullReferenceException();
            this.WriteString(of.Extent.Text);
            this.WriteString(' ');

            // GOLF_PhoneNumber
            var typeName = node.TypeName ?? throw new NullReferenceException();
            this.WriteString(typeName.Name);

            // {
            //     AreaCode = { "9", "0", "7" };
            //     Number = { "7", "4", "7", "4", "8", "8", "4" };
            // }
            this.WriteLine();
            this.WriteIndent();
            this.WriteAstNode(
                node.PropertyValues
            );

        }

    }

    #endregion

}
