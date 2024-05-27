using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1.6 Null value

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
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
