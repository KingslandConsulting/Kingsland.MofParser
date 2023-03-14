using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1 Primitive type value

    public void WriteAstNode(LiteralValueArrayAst node)
    {

        // instance of GOLF_ClubMember
        // {
        //     LastPaymentDate = {1, 2};
        //                       ^^^^^^
        // };
        
        // {
        this.WriteString('{');

        // 1, 2
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
