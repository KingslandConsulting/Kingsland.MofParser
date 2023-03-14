using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.1 Primitive type value

    public void WriteAstNode(LiteralValueAst node)
    {
        switch (node)
        {
            case IntegerValueAst ast:
                this.WriteAstNode(ast);
                break;
            case RealValueAst ast:
                this.WriteAstNode(ast);
                break;
            case BooleanValueAst ast:
                this.WriteAstNode(ast);
                break;
            case NullValueAst ast:
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
