using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.9 Complex type value

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(PropertyValueListAst node)
    {

        // instance of GOLF_ClubMember
        // vvvvvvvvvvvvvvvvvvvvvvvvv
        // {
        //     FirstName = ""John"";
        //     LastName = ""Doe"";
        // };
        // ^^^^^^^^^^^^^^^^^^^^^^^^^

        // {
        this.WriteString('{');

        //     FirstName = ""John"";
        //     LastName = ""Doe"";
        this.Indent();
        foreach (var propertySlot in node.PropertySlots)
        {
            this.WriteLine();
            this.WriteIndent();
            this.WriteAstNode(propertySlot);
        }
        this.Unindent();

        // }
        this.WriteLine();
        this.WriteIndent();
        this.WriteString('}');

    }

    #endregion

}
