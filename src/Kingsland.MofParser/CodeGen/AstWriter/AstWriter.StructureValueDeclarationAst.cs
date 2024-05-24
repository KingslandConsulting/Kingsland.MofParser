using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.2 Complex type value

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(StructureValueDeclarationAst node)
    {

        // value of GOLF_PhoneNumber as $JohnDoesPhoneNo
        // {
        //     AreaCode = { "9", "0", "7" };
        //     Number = { "7", "4", "7", "4", "8", "8", "4" };
        // };

        // value
        this.WriteString(node.Value.Name);

        // of
        this.WriteString(' ');
        this.WriteString(node.Of.Name);

        // GOLF_PhoneNumber
        this.WriteString(' ');
        this.WriteString(node.TypeName.Name);
        if (node.Alias is not null)
        {
            // as
            this.WriteString(' ');
            var @as = node.As?.Name ?? throw new NullReferenceException();
            this.WriteString(@as);
            // $JohnDoesPhoneNo
            this.WriteString(" $", node.Alias.Name);
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
        this.WriteString(node.StatementEnd?.Text ?? ";");

    }

    #endregion

}
