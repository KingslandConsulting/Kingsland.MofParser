using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.4.1 QualifierList

    public void WriteAstNode(QualifierListAst node)
    {

        // [Abstract, OCL]

        var omitSpaceQuirkEnabled = this.Options.Quirks.HasFlag(
            MofQuirks.OmitSpaceBetweenInOutQualifiersForParameterDeclarations
        );

        var prevQualifierValue = default(QualifierValueAst);

        // [
        this.WriteString('[');

        // Abstract, OCL
        foreach (var thisQualifierValue in node.QualifierValues)
        {
            if (prevQualifierValue is not null)
            {
                this.WriteString(',');
                if (!omitSpaceQuirkEnabled || !prevQualifierValue.QualifierName!.IsKeyword("in") || !thisQualifierValue.QualifierName.IsKeyword("out"))
                {
                    this.WriteString(' ');
                }
            }
            this.WriteAstNode(
                thisQualifierValue
            );
            prevQualifierValue = thisQualifierValue;
        }

        // ]
        this.WriteString(']');

    }

    #endregion

}
