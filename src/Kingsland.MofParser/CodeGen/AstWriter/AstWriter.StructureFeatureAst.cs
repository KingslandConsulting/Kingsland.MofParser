using Kingsland.MofParser.Ast;

namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.1 Structure declaration

    public void WriteAstNode(IStructureFeatureAst node)
    {
        switch (node)
        {
            case StructureDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case EnumerationDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            case PropertyDeclarationAst ast:
                this.WriteAstNode(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

}
