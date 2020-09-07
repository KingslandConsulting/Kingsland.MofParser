using System;
using System.Collections.Generic;
using System.Linq;

namespace Kingsland.ParseFx.Text
{

    /// <summary>
    /// Denotes the start position, end position and text of a section of source code.
    /// </summary>
    public sealed class SourceExtent
    {

        #region Fields

        public readonly static SourceExtent Empty = new SourceExtent(
            SourcePosition.Empty,
            SourcePosition.Empty,
            string.Empty
        );

        #endregion

        #region Constructors

        public SourceExtent(SourcePosition startPosition, SourcePosition endPosition, string text)
        {
            this.StartPosition = startPosition;
            this.EndPosition = endPosition;
            this.Text = text;
        }

        #endregion

        #region Properties

        public SourcePosition StartPosition
        {
            get;
            private set;
        }

        public SourcePosition EndPosition
        {
            get;
            private set;
        }

        public string Text
        {
            get;
            private set;
        }

        #endregion

        #region Factory Methods

        public static SourceExtent From(SourceChar @char)
        {
            return (@char == null ) ?
                throw new ArgumentNullException(nameof(@char)) :
                new SourceExtent(
                    startPosition: @char.Position,
                    endPosition: @char.Position,
                    text: new string(@char.Value, 1)
                );
        }

        public static SourceExtent From(IList<SourceChar> chars)
        {
            var text = (chars == null) ?
                throw new ArgumentNullException(nameof(chars)) :
                chars.Select(n => n.Value).ToArray();
            return text.Any() ?
                new SourceExtent(
                    startPosition: chars.First().Position,
                    endPosition: chars.Last().Position,
                    text: new string(text)
                ) :
                SourceExtent.Empty;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            var start = this.StartPosition;
            var end = this.EndPosition;
            return $"StartPosition=[{start.Position},{start.LineNumber},{start.ColumnNumber}]," +
                   $"EndPosition=[{end.Position},{end.LineNumber},{end.ColumnNumber}]," +
                   $"Text=\"{this.Text}\"";
        }

        #endregion

    }

}
