using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1.3 String values

    public void WriteAstNode(StringValueAst node)
    {

        this.WriteDelimitedList(
            node.StringLiteralValues,
            (item) => {
                this.WriteString($"\"{StringLiteralToken.EscapeString(item.Value)}\"");
            },
            " "
        );

    }

    #endregion

}
