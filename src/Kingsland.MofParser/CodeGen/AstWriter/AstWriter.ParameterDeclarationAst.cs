using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.7 Parameter declaration

    public void WriteAstNode(ParameterDeclarationAst node)
    {

        // class GOLF_Base
        // {
        //     Integer GetMembersWithOutstandingFees(Integer REF Severity[] = { 1, 2, 3 });
        //                                           ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        // };

        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteAstNode(
                node.QualifierList
            );
            this.WriteString(' ');
        }

        // Integer
        this.WriteString(node.ParameterType.Name);

        this.WriteString(' ');
        if (node.ParameterIsRef)
        {
            // REF
            var parameterRef = node.ParameterRef?.Name
                ?? throw new NullReferenceException();
            this.WriteString(parameterRef);
            this.WriteString(' ');
        }

        // Severity
        this.WriteString(node.ParameterName.Name);

        if (node.ParameterIsArray)
        {
            // []
            this.WriteString("[]");
        }

        if (node.DefaultValue is not null)
        {
            // =
            this.WriteString(" = ");
            // { 1, 2, 3 }
            this.WriteAstNode(
                node.DefaultValue
            );
        }

    }

    #endregion

}
