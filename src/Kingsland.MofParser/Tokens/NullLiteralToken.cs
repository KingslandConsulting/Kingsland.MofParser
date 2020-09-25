﻿using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class NullLiteralToken : SyntaxToken
    {

        public NullLiteralToken(SourceExtent extent)
            : base(extent)
        {
        }

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ?? Constants.NULL;
        }

        #endregion

    }

}
