using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1.6 Null value

    public void WriteAstNode(NullValueAst node)
    {

        // instance of GOLF_ClubMember
        // {
        //     LastPaymentDate = null;
        // };

        // null
        this.WriteString(
            node.Token?.Text
                ?? Constants.NULL
        );

    }

    #endregion

}
