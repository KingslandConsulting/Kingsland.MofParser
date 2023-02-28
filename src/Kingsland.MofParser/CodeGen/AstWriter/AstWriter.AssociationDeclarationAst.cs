﻿using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.3 Association declaration

    public void WriteAstNode(AssociationDeclarationAst node)
    {

        // association GOLF_MemberLocker : GOLF_Base
        // {
        //     GOLF_ClubMember REF Member;
        //     GOLF_Locker REF Locker;
        //     GOLF_Date AssignedOnDate;
        // };

        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteAstNode(
                node.QualifierList
            );
            this.WriteLine();
            this.WriteIndent();
        }

        // association GOLF_MemberLocker : GOLF_Base
        this.WriteString(Constants.ASSOCIATION, " ", node.AssociationName.Name);
        if (node.SuperAssociation is not null)
        {
            this.WriteString(" : ", node.SuperAssociation.Name);
        }

        // {
        this.WriteLine();
        this.WriteIndent();
        this.WriteString("{");

        //     GOLF_ClubMember REF Member;
        //     GOLF_Locker REF Locker;
        //     GOLF_Date AssignedOnDate;
        this.WriteLine();
        this.Indent();
        foreach (var classFeature in node.ClassFeatures)
        {
            this.WriteIndent();
            this.WriteAstNode(
                classFeature
            );
            this.WriteLine();
        }
        this.Unindent();

        // };
        this.WriteIndent();
        this.WriteString("};");
    }

    #endregion

}
