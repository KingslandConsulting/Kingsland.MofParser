using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.3 Enum type value

    public void WriteAstNode(EnumValueArrayAst node)
    {
        // instance of GOLF_Date
        // {
        //   Month = {June, July};
        //           ^^^^^^^^^^^^
        // };

        // {
        this.WriteString('{');

        // June, July
        this.WriteDelimitedList(
            node.Values,
            this.WriteAstNode,
            ", "
        );

        // }
        this.WriteString('}');

    }

    #endregion

}
