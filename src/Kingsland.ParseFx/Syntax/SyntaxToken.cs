using Kingsland.ParseFx.Text;
using System;

namespace Kingsland.ParseFx.Syntax
{

    public abstract record SyntaxToken
    {

        #region Constructors

        protected SyntaxToken(SourceExtent extent)
        {
            if (extent == null)
            {
                throw new ArgumentNullException(nameof(extent));
            }
            this.Extent = extent;
        }

        #endregion

        #region Properties

        public SourceExtent Extent
        {
            get;
            private init;
        }

        public string Text
        {
            get
            {
                return this.Extent.Text;
            }
        }

        #endregion

        #region Methods

        public virtual string GetDebugString()
        {
            return $"{this.GetType().Name} (\"{this?.Extent.Text}\")";
        }

        public virtual string GetSourceString()
        {
            return this.Extent.Text;
        }

        #endregion

        #region Object Interface

        public override string ToString()
        {
            return this.GetDebugString();
        }

        #endregion

    }

}
