using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.3 Enum type value
///
///     enumValueArray = "{" [ enumName *( "," enumName ) ] "}"
///
/// </remarks>
public sealed record EnumValueArrayAst : EnumTypeValueAst
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.Values = [];
        }

        public List<EnumValueAst> Values
        {
            get;
            private set;
        }

        public EnumValueArrayAst Build()
        {
            return new(
                this.Values
            );
        }

    }

    #endregion

    #region Constructors

    internal EnumValueArrayAst()
        : this([])
    {
    }

    internal EnumValueArrayAst(
        IEnumerable<EnumValueAst> values
    )
    {
        this.Values = (values ?? throw new ArgumentNullException(nameof(values)))
            .ToList().AsReadOnly();
    }

    #endregion

    #region Properties

    public ReadOnlyCollection<EnumValueAst> Values
    {
        get;
    }

    #endregion

}
