namespace Kingsland.MofParser.CodeGen
{

    public sealed class MofWriterSettings
    {

        #region Properties

        public string NewLine
        {
            get;
            init;
        } = Environment.NewLine;

        public string IndentStep
        {
            get;
            init;
        } = "\t";

        public MofQuirks Quirks
        {
            get;
            init;
        } = MofQuirks.None;

    #endregion

    }

}
