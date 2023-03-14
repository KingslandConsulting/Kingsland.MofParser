using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.6.3 Enum type value

    public void WriteAstNode(EnumTypeValueAst node)
    {
        switch (node)
        {
            case EnumValueAst ast:
                this.WriteAstNode(ast);
                break;
            case EnumValueArrayAst ast:
                this.WriteAstNode(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

}
