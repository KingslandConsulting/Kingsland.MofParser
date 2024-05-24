using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1.2 Real value

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(RealValueAst node)
    {

        this.WriteString(
            node.RealLiteralToken?.Text
                ?? throw new InvalidOperationException()
        );

    }

    #endregion

}
