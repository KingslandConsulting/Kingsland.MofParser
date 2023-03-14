using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.9 Complex type value

    public void WriteAstNode(PropertyValueAst node)
    {
        switch (node)
        {
            case PrimitiveTypeValueAst ast:
                this.WriteAstNode(ast);
                break;
            case ComplexTypeValueAst ast:
                this.WriteAstNode(ast);
                break;
            //case ReferenceTypeValueAst ast:
            case EnumTypeValueAst ast:
                this.WriteAstNode(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

}
