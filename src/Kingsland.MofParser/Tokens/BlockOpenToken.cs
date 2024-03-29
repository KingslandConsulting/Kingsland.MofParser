﻿using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens;

public sealed record BlockOpenToken : SyntaxToken
{

    #region Constructors

    public BlockOpenToken()
        : this((SourceExtent?)null)
    {
    }

    public BlockOpenToken(SourcePosition? start, SourcePosition? end, string text)
        : this(new SourceExtent(start, end, text))
    {
    }

    public BlockOpenToken(SourceExtent? extent)
        : base(extent)
    {
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return this.Text
            ?? "{";
    }

    #endregion

}
