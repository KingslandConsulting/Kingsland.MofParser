using Kingsland.MofParser.Ast;

namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.9 Complex type value

    public void WriteAstNode(ComplexTypeValueAst node)
    {
        switch (node)
        {
            case ComplexValueArrayAst ast:
                this.WriteAstNode(ast);
                break;
            case ComplexValueAst ast:
                this.WriteAstNode(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

}
