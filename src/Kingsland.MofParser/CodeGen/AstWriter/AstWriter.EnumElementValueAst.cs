using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.4 Enumeration declaration

    public void WriteAstNode(IEnumElementValueAst node)
    {
        switch (node)
        {
            case IntegerValueAst ast:
                this.WriteAstNode(ast);
                break;
            case StringValueAst ast:
                this.WriteAstNode(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

}
