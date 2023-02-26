using Kingsland.MofParser.Ast;

namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.3 Enum type value

    public void WriteAstNode(EnumValueAst node)
    {

        // instance of GOLF_Date
        // {
        //     Month = MonthEnums.July;
        //             ^^^^^^^^^^^^^^^
        // };

        if (node.EnumName is not null)
        {
            // MonthEnums
            this.WriteString(node.EnumName.Name);
            // .
            this.WriteString('.');
        }

        // July
        this.WriteString(node.EnumLiteral.Name);

    }

    #endregion

}
