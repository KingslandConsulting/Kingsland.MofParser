using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1.5 Boolean value

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(BooleanValueAst node)
    {

        // TRUE
        // FALSE

        this.WriteString(
            node.Token?.Text
                ?? (node.Value ? Constants.TRUE : Constants.FALSE)
        );

    }

    #endregion

}
