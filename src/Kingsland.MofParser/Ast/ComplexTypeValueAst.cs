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
    ///     complexTypeValue = complexValue / complexValueArray
    ///
    /// </remarks>
    public abstract class ComplexTypeValueAst : PropertyValueAst
    {

        #region Constructors

        internal ComplexTypeValueAst()
        {
        }

        #endregion

    }

}
