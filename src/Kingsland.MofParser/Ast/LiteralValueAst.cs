namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1 Primitive type value
    ///
    ///     literalValue = integerValue /
    ///                    realValue /
    ///                    booleanValue /
    ///                    nullValue /
    ///                    stringValue
    ///                      ; NOTE stringValue covers octetStringValue and
    ///                      ; dateTimeValue
    ///
    /// </remarks>
    public abstract class LiteralValueAst : PrimitiveTypeValueAst
    {

        #region Constructors

        protected LiteralValueAst()
        {
        }

        #endregion

    }

}