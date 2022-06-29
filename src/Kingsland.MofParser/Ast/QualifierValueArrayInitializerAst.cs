using Kingsland.MofParser.CodeGen;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.4.1 QualifierList
///
///     qualiferValueArrayInitializer = "{" literalValue *( "," literalValue ) "}"
///
/// </remarks>

public sealed record QualifierValueArrayInitializerAst : IQualifierInitializerAst
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.Values = new List<LiteralValueAst>();
        }

        public List<LiteralValueAst> Values
        {
            get;
            private set;
        }

        public QualifierValueArrayInitializerAst Build()
        {
            return new QualifierValueArrayInitializerAst(
                this.Values
            );
        }

    }

    #endregion

    #region Constructors

    internal QualifierValueArrayInitializerAst()
        : this(new List<LiteralValueAst>())
    {
    }

    internal QualifierValueArrayInitializerAst(
        IEnumerable<LiteralValueAst> values
    )
    {
        this.Values = new ReadOnlyCollection<LiteralValueAst>(
            values.ToList()
        );
    }

    #endregion

    #region Properties

    public ReadOnlyCollection<LiteralValueAst> Values
    {
        get;
        private init;
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return AstMofGenerator.ConvertQualifierValueArrayInitializerAst(this);
    }

    #endregion

}
