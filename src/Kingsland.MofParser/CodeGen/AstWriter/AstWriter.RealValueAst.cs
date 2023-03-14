using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1.2 Real value

    public void WriteAstNode(RealValueAst node)
    {

        this.WriteString(
            node.RealLiteralToken?.Text
                ?? throw new InvalidOperationException()
        );

    }

    #endregion

}
