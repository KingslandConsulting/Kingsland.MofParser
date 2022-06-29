using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.1.5 Boolean value
///
///     booleanValue = TRUE / FALSE
///
///     FALSE        = "false" ; keyword: case insensitive
///     TRUE         = "true"  ; keyword: case insensitive
///
/// </remarks>
public sealed record BooleanValueAst : LiteralValueAst
{

    #region Builder

    public sealed class Builder
    {

        public BooleanLiteralToken? Token
        {
            get;
            set;
        }

        public BooleanValueAst Build()
        {
            return new BooleanValueAst(
                this.Token ?? throw new InvalidOperationException(
                    $"{nameof(this.Token)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal BooleanValueAst(BooleanLiteralToken token)
    {
        this.Token = token ?? throw new ArgumentNullException(nameof(token));
    }

    #endregion

    #region Properties

    public BooleanLiteralToken Token
    {
        get;
    }

    public bool Value
    {
        get
        {
            return this.Token.Value;
        }
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return AstMofGenerator.ConvertBooleanValueAst(this);
    }

    #endregion

}
