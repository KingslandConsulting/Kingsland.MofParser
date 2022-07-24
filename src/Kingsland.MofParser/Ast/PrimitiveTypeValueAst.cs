namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.1 Primitive type value
///
///     primitiveTypeValue = literalValue / literalValueArray
///
/// </remarks>
public abstract record PrimitiveTypeValueAst : PropertyValueAst
{

    #region Constructors

    internal PrimitiveTypeValueAst()
    {
    }

    #endregion

}
