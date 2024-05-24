using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.4.1 QualifierList

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(IQualifierInitializerAst node)
    {
        switch (node)
        {
            case QualifierValueInitializerAst ast:
                this.WriteAstNode(ast);
                break;
            case QualifierValueArrayInitializerAst ast:
                this.WriteAstNode(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

}
