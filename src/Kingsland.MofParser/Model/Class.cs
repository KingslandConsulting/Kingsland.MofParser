namespace Kingsland.MofParser.Model
{

    public sealed record Class
    {

        #region BUilder

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
                return new Class
                {
                    ClassName = this.ClassName,
                    SuperClass = this.SuperClass
                };
            }

        }

        #endregion

        #region Constructors

        private Class()
        {
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
