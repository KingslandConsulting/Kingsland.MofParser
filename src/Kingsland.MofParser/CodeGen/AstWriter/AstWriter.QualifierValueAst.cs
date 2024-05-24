using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.4.1 QualifierList

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(QualifierValueAst node)
    {

        this.WriteString(node.QualifierName.Name);

        if (node.Initializer is not null)
        {
            this.WriteAstNode(
                node.Initializer
            );
        }

        //
        // 7.4 Qualifiers
        //
        // NOTE A MOF v2 qualifier declaration has to be converted to MOF v3 qualifierTypeDeclaration because the
        // MOF v2 qualifier flavor has been replaced by the MOF v3 qualifierPolicy.
        //
        // These aren't part of the MOF 3.0.1 spec, but we'll include them anyway for backward compatibility.
        //
        if (node.Flavors.Any())
        {
            this.WriteString(": ");
            this.WriteString(
                string.Join(
                    " ",
                    node.Flavors.Select(f => f.Name)
                )
            );
        }

    }

    #endregion

}
