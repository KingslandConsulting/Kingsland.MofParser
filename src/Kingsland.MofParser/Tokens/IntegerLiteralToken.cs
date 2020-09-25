using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using System;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IntegerLiteralToken : SyntaxToken
    {

        public IntegerLiteralToken(SourceExtent extent, IntegerKind kind, long value)
            : base(extent)
        {
            this.Kind = kind;
            this.Value = value;
        }

        public IntegerKind Kind
        {
            get;
            private set;
        }

        public long Value
        {
            get;
            private set;
        }

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ??
                 this.Kind switch
                 {
                     IntegerKind.BinaryValue => $"{Convert.ToString(this.Value, 2)}b",
                     IntegerKind.DecimalValue => Convert.ToString(this.Value, 10),
                     IntegerKind.HexValue => $"0x{Convert.ToString(this.Value, 16)}",
                     IntegerKind.OctalValue => $"0{Convert.ToString(this.Value, 8)}",
                     _ => throw new InvalidOperationException()
                 };
        }

        #endregion

    }

}
