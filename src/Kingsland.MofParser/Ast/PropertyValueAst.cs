using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.9 Complex type value
    ///
    ///     propertyValue = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
    ///
    /// </remarks>
    public abstract class PropertyValueAst : AstNode
    {

    }

}
