﻿using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.9 Complex type value
///
///     complexValueArray = "{" [ complexValue *( "," complexValue) ] "}"
///
/// </remarks>
public sealed record ComplexValueArrayAst : ComplexTypeValueAst
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.Values = new();
        }

        public List<ComplexValueAst> Values
        {
            get;
            private set;
        }

        public ComplexValueArrayAst Build()
        {
            return new(
                this.Values
            );
        }

    }

    #endregion

    #region Constructors

    internal ComplexValueArrayAst(
        IEnumerable<ComplexValueAst> values
    )
    {
        this.Values = new(
            (values ?? throw new ArgumentNullException(nameof(values)))
                .ToList()
        );
    }

    #endregion

    #region Properties

    public ReadOnlyCollection<ComplexValueAst> Values
    {
        get;
    }

    #endregion

}
