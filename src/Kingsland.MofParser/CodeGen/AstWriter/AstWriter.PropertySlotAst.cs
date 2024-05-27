using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.9 Complex type value

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(PropertySlotAst node)
    {

        // instance of GOLF_ClubMember
        // {
        //     FirstName = ""John"";
        //     ^^^^^^^^^^^^^^^^^^^^^
        //     LastName = ""Doe"";
        // };

        // FirstName
        this.WriteString(node.PropertyName.Name);

        // =
        this.WriteString(" = ");

        // ""John""
        this.WriteAstNode(
            node.PropertyValue
        );

        // ;
        this.WriteString(";");

    }

    #endregion

}
