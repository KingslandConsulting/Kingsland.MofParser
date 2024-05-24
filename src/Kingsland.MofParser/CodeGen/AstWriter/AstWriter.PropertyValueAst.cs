using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.9 Complex type value

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
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
