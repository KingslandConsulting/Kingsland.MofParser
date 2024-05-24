using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1.1 Integer value

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(IntegerValueAst node)
    {

        // instance of GOLF_ClubMember
        // {
        //     Caption = 100;
        //               ^^^
        // };

        // 100
        this.WriteString(
            node.IntegerLiteralToken?.Text
                ?? throw new InvalidOperationException()
        );

    }

    #endregion

}
