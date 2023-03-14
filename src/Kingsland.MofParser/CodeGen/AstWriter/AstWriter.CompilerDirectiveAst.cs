using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.3 Compiler directives

    public void WriteAstNode(CompilerDirectiveAst node)
    {

        // #pragma include ("GlobalStructs/GOLF_Address.mof")

        // #pragma include (
        this.WriteString(
            node.PragmaKeyword.Text ?? Constants.PRAGMA,
            " ",
            node.PragmaName.Name,
            " ("
        );

        // "GlobalStructs/GOLF_Address.mof"
        this.WriteAstNode(
            node.PragmaParameter
        );

        // )
        this.WriteString(")");

    }

    #endregion

}
