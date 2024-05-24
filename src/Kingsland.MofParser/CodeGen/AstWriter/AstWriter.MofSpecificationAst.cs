using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.2 MOF specification

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
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
