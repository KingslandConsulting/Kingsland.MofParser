using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.2 Class declaration

    public void WriteAstNode(IClassFeatureAst node)
    {
        switch (node)
        {
            case IStructureFeatureAst ast:
                this.WriteAstNode(ast);
                break;
            case MethodDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

}
