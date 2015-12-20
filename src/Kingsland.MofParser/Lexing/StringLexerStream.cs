using System;

namespace Kingsland.MofParser.Lexing
{

    public sealed class StringLexerStream : LexerStreamBase
    {

        #region Constructors

        public StringLexerStream(string source)
            : base()
        {
            this.Source = source;
        }

        #endregion

        #region Properties

        private string Source
        {
            get;
            set;
        }

        public override int Length
        {
            get
            {
                return (this.Source == null) ? 0 : this.Source.Length;
            }
        }

        #endregion

        #region Peek Methods

        /// <summary>
        /// Reads the next character off of the input stream, but does not advance the current position.
        /// </summary>
        /// <returns></returns>
        public override SourceChar Peek()
        {
            if (this.Eof)
            {
                throw new UnexpectedEndOfStreamException();
            }
            return new SourceChar(this.Source[this.Position], this.Position, this.LineNumber, this.ColumnNumber);
        }

        #endregion

    }

}
