namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.3 Enum type value
    ///
    ///     enumTypeValue = enumValue / enumValueArray
    ///
    /// </remarks>
    public abstract class EnumTypeValueAst : PropertyValueAst
    {

        #region Constructors

        internal EnumTypeValueAst()
        {
        }

        #endregion

    }

}
