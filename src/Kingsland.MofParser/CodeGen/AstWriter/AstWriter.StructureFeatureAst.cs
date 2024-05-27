using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.1 Structure declaration

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
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
