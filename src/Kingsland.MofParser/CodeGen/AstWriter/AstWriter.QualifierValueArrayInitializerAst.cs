using Kingsland.MofParser.Ast;

// Resharper disable once CheckNamespace
namespace Kingsland.MofParser.CodeGen;

public sealed partial class AstWriter
{

    #region 7.4.1 QualifierList

    public void WriteAstNode(QualifierValueArrayInitializerAst node)
    {

        // OCL{"-- the key property cannot be NULL", "inv: InstanceId.size() = 10"}
        //    ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        // {
        this.WriteString('{');

        // "-- the key property cannot be NULL", "inv: InstanceId.size() = 10"
        this.WriteDelimitedList(
            node.Values,
            this.WriteAstNode,
            ", "
        );

        // }
        this.WriteString('}');

    }

    #endregion

}
