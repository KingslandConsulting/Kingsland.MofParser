using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
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
            var alias = node.Alias?.Name ?? throw new NullReferenceException();
            this.WriteString("$", alias);

        }
        else
        {

            // value of GOLF_PhoneNumber
            // {
            //     AreaCode = { "9", "0", "7" };
            //     Number = { "7", "4", "7", "4", "8", "8", "4" };
            // }

            // value
            var value = node.Value?.Name ?? throw new NullReferenceException();
            this.WriteString(value);
            this.WriteString(' ');

            // of
            var of = node.Of?.Name ?? throw new NullReferenceException();
            this.WriteString(of);
            this.WriteString(' ');

            // GOLF_PhoneNumber
            var typeName = node.TypeName?.Name ?? throw new NullReferenceException();
            this.WriteString(typeName);

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
