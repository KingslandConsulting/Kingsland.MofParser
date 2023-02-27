﻿using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// 7.6.1.6 Null value
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
///     nullValue = NULL
///
///     NULL      = "null" ; keyword: case insensitive
///                        ; second
///
/// </remarks>
public sealed record NullValueAst : LiteralValueAst
{

    #region Builder

    public sealed class Builder
    {

        public NullLiteralToken? Token
        {
            get;
            set;
        }

        public NullValueAst Build()
        {
            return new(
                this.Token ?? throw new InvalidOperationException(
                    $"{nameof(this.Token)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal NullValueAst(
        NullLiteralToken token
    )
    {
        this.Token = token ?? throw new ArgumentNullException(nameof(token));
    }

    #endregion

    #region Properties

    public NullLiteralToken Token
    {
        get;
    }

    #endregion

}
