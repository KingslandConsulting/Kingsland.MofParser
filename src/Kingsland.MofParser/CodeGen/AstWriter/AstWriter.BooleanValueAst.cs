using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1.5 Boolean value

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
