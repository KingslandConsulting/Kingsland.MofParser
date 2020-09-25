﻿using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class StatementEndToken : SyntaxToken
    {

        public StatementEndToken(SourceExtent extent)
            : base(extent)
        {
        }

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ?? ";";
        }

        #endregion

    }

}
