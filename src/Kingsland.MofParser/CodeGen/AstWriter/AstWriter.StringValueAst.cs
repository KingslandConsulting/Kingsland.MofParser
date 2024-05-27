using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1.3 String values

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
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
