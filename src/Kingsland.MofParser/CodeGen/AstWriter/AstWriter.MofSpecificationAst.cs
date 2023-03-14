using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.2 MOF specification

    public void WriteAstNode(MofSpecificationAst node)
    {
        this.WriteDelimitedList(
            node.Productions,
            this.WriteAstNode,
            this.Options.NewLine
        );
    }

    #endregion

}
