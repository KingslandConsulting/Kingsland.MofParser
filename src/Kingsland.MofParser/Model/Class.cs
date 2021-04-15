using System;

namespace Kingsland.MofParser.Model
{

    public sealed record Class
    {

        #region Builder

        public sealed class Builder
        {

            public string ClassName
            {
                get;
                set;
            }

            public string SuperClass
            {
                get;
                set;
            }

            public Class Build()
            {
                return new Class(
                    this.ClassName,
                    this.SuperClass
                );
            }

        }

        #endregion

        #region Constructors

        internal Class(string className, string superClass)
        {
            this.ClassName = className ?? throw new ArgumentNullException(nameof(className));
            this.SuperClass = superClass;
        }

        #endregion

        #region Properties

        public string ClassName
        {
            get;
            private init;
        }

        public string SuperClass
        {
            get;
            private init;
        }

        #endregion

    }

}
