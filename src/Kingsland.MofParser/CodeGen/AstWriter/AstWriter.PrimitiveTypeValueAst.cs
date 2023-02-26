using Kingsland.MofParser.Ast;

namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1 Primitive type value

    public void WriteAstNode(PrimitiveTypeValueAst node)
    {
        switch (node)
        {
            case LiteralValueAst ast:
                this.WriteAstNode(ast);
                break;
            case LiteralValueArrayAst ast:
                this.WriteAstNode(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

}
