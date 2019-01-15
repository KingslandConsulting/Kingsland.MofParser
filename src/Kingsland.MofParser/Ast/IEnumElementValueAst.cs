namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.4 Enumeration declaration
    ///
    ///     integerEnumElement     = [ qualifierList ] enumLiteral "=" integerValue
    ///     stringEnumElement      = [ qualifierList ] enumLiteral [ "=" stringValue ]
    ///
    /// </remarks>
    public interface IEnumElementValueAst : IAstNode
    {

    }

}
