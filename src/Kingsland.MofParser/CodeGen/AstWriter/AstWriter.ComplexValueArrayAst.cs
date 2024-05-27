using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.9 Complex type value

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(ComplexValueArrayAst node)
    {

        // instance of GOLF_ClubMember
        // {
        //     LastPaymentDate = {$MyAliasIdentifier, $MyOtherAliasIdentifier};
        //                       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        // };

        // {
        this.WriteString('{');

        // $MyAliasIdentifier, $MyOtherAliasIdentifier
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
