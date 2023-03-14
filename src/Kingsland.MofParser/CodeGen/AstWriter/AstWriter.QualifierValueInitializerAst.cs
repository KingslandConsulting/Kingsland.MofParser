using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.4.1 QualifierList

    public void WriteAstNode(QualifierValueInitializerAst node)
    {

        // Description("an instance of a class that derives from the GOLF_Base class. ")
        //            ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        // (
        this.WriteString('(');

        // "an instance of a class that derives from the GOLF_Base class. "
        this.WriteAstNode(
            node.Value
        );

        // )
        this.WriteString(')');

    }

    #endregion

}
