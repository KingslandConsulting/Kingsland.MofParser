using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.6 Method declaration

    public void WriteAstNode(MethodDeclarationAst node)
    {

        // class GOLF_Professional : GOLF_ClubMember
        // {
        //     GOLF_ResultCodeEnum GetNumberOfProfessionals(Integer NoOfPros, GOLF_Club Club, ProfessionalStatusEnum Status = Professional);
        //     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        // };

        var prefixQuirkEnabled = this.Options.Quirks.HasFlag(
            MofQuirks.PrefixSpaceBeforeQualifierlessMethodDeclarations
        );
        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteAstNode(
                node.QualifierList
            );
        }
        if (prefixQuirkEnabled || node.QualifierList.QualifierValues.Any())
        {
            this.WriteString(' ');
        }

        // GOLF_ResultCodeEnum
        this.WriteString(node.ReturnType.Name);
        if (node.ReturnTypeIsArray)
        {
            this.WriteString("[]");
        }

        // GetNumberOfProfessionals
        this.WriteString(' ');
        this.WriteString(node.Name.Name);

        // (
        this.WriteString('(');

        // Integer NoOfPros, GOLF_Club Club, ProfessionalStatusEnum Status = Professional
        this.WriteDelimitedList(
            node.Parameters,
            this.WriteAstNode,
            ", "
        );

        // );
        this.WriteString(");");

    }

    #endregion

}
