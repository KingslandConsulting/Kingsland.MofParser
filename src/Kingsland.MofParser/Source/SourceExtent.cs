using System.Collections.Generic;
using System.Linq;

namespace Kingsland.MofParser.Source
{

    /// <summary>
    /// Denotes the start point, end point and text of a section of source code.
    /// </summary>
    public sealed class SourceExtent
    {

        #region Builder

        public sealed class Builder
        {

            public SourcePosition StartPosition
            {
                get;
                set;
            }

            public SourcePosition EndPosition
            {
                get;
                set;
            }

            public string Text
            {
                get;
                set;
            }

            public SourceExtent Build()
            {
                return new SourceExtent
                {
                    StartPosition = this.StartPosition,
                    EndPosition = this.EndPosition,
                    Text = this.Text
                };
            }

        }

        #endregion

        #region Fields

        public readonly static SourceExtent Empty = new SourceExtent.Builder
        {
            StartPosition = SourcePosition.Empty,
            EndPosition = SourcePosition.Empty,
            Text = string.Empty
        }.Build();

        #endregion

        #region Constructors

        private SourceExtent()
        {
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

        public static SourceExtent FromChar(SourceChar @char)
        {
            return new Builder
            {
                StartPosition = @char.Position,
                EndPosition = @char.Position,
                Text = new string(@char.Value, 1)
            }.Build();
        }

        public static SourceExtent FromChars(IList<SourceChar> chars)
        {
            return ((chars == null) || (chars.Count == 0)) ?
                SourceExtent.Empty :
                new SourceExtent.Builder
                {
                    StartPosition = chars.First().Position,
                    EndPosition = chars.Last().Position,
                    Text = new string(chars.Select(n => n.Value).ToArray())
                }.Build();
        }

        #endregion

        #region Methods

        public bool EqualTo(SourceExtent obj)
        {
            return object.ReferenceEquals(obj, this) ||
                   ((obj != null) &&
                    obj.StartPosition.EqualTo(this.StartPosition) &&
                    obj.EndPosition.EqualTo(this.EndPosition) &&
                    (obj.Text == this.Text));
        }

        #endregion

    }

}
