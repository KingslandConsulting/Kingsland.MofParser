using Kingsland.MofParser.Objects;

namespace Kingsland.MofParser.HtmlReport.Wrappers
{

    public sealed class ScriptResourceWrapper : InstanceWrapper
    {

        public ScriptResourceWrapper(string filename, string computerName, Instance instance)
            : base(filename, computerName, instance)
        {
        }

        public string GetScript
        {
            get
            {
                return InstanceWrapper.GetStringProperty(this.Instance, "GetScript");
            }
        }

        public string TestScript
        {
            get
            {
                return InstanceWrapper.GetStringProperty(this.Instance, "TestScript");
            }
        }

        public string SetScript
        {
            get
            {
                return InstanceWrapper.GetStringProperty(this.Instance, "SetScript");
            }
        }

    }

}
