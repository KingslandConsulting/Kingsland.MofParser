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
    /// 7.6.1 Primitive type value
    ///
    ///     primitiveTypeValue = literalValue / literalValueArray
    ///
    /// 7.5.9 Complex type value
    ///
    ///     complexTypeValue = complexValue / complexValueArray
    ///
    /// 7.6.4 Reference type value
    ///
    ///     referenceTypeValue = objectPathValue / objectPathValueArray
    ///
    /// 7.6.3 Enum type value
    ///
    ///     enumTypeValue = enumValue / enumValueArray
    ///
    /// </remarks>
    public abstract class PropertyValueAst : AstNode
    {

    }

}
