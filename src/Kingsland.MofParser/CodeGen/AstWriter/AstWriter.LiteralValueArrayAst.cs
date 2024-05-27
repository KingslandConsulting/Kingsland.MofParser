using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1 Primitive type value

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
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
