using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.9 Complex type value

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
