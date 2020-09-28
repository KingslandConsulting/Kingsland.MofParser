using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using System;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IntegerLiteralToken : SyntaxToken
    {

        #region Constructors

        public IntegerLiteralToken(IntegerKind kind, long value)
            : this(SourceExtent.Empty, kind, value)
        {
        }

        public IntegerLiteralToken(SourcePosition start, SourcePosition end, string text, IntegerKind kind, long value)
            : this(new SourceExtent(start, end, text), kind, value)
        {
        }

        public IntegerLiteralToken(SourceExtent extent, IntegerKind kind, long value)
            : base(extent)
        {
            this.Kind = kind;
            this.Value = value;
        }

        #endregion

        #region Properties

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

        #endregion

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return (this.Extent != SourceExtent.Empty) ?
                this.Extent.Text :
                this.Kind switch
                {
                    IntegerKind.BinaryValue =>
                        $"{(this.Value < 0 ? "-" : "")}{Convert.ToString(Math.Abs(this.Value), 2)}b",
                    IntegerKind.DecimalValue =>
                        Convert.ToString(this.Value, 10),
                    IntegerKind.HexValue =>
                        $"{(this.Value < 0 ? "-" : "")}0x{Convert.ToString(Math.Abs(this.Value), 16).ToUpperInvariant()}",
                    IntegerKind.OctalValue =>
                        $"{(this.Value < 0 ? "-" : "")}0{Convert.ToString(Math.Abs(this.Value), 8)}",
                    _ => throw new InvalidOperationException()
                };
        }

        #endregion

    }

}
