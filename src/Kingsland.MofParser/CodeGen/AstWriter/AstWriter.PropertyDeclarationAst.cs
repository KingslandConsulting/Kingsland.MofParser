﻿using Kingsland.MofParser.Ast;
using System.Diagnostics.CodeAnalysis;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.5.5 Property declaration

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void WriteAstNode(PropertyDeclarationAst node)
    {

        // class GOLF_Base
        // {
        //     Integer REF Severity[] = { 1, 2, 3 };
        //     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        // };

        if (node.QualifierList.QualifierValues.Count > 0)
        {
            this.WriteAstNode(
                node.QualifierList
            );
            this.WriteString(' ');
        }

        // Integer
        this.WriteString(node.ReturnType.Name);

        this.WriteString(' ');
        if (node.ReturnTypeIsRef)
        {
            // REF
            var returnTypeRef = node.ReturnTypeRef?.Name
                ?? throw new NullReferenceException();
            this.WriteString(returnTypeRef);
            this.WriteString(' ');
        }

        // Severity
        this.WriteString(node.PropertyName.Name);

        if (node.ReturnTypeIsArray)
        {
            // []
            this.WriteString("[]");
        }

        if (node.Initializer is not null)
        {
            // =
            this.WriteString(" = ");
            // { 1, 2, 3 }
            this.WriteAstNode(
                node.Initializer
            );
        }

        // ;
        this.WriteString(';');

    }

    #endregion

}
