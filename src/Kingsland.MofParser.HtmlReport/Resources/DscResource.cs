using Kingsland.MofParser.Objects;

namespace Kingsland.MofParser.HtmlReport.Resources
{

    public class DscResource
    {

        #region Constructors

        protected DscResource(string filename, string computerName, Instance instance)
        {
            this.Filename = filename;
            this.ComputerName = computerName;
            this.Instance = instance;
        }

        #endregion

        #region Properties

        public string Filename
        {
            get;
            private set;
        }

        public string ComputerName
        {
            get;
            private set;
        }

        public Instance Instance
        {
            get;
            private set;
        }

        public string ResourceId
        {
            get
            {
                return this.GetStringProperty("ResourceID");
            }
        }

        public string ClassName
        {
            get
            {
                return this.Instance.ClassName;
            }
        }

        public string ResourceType
        {
            get
            {
                // ResourceID = "[ResourceType]ResourceName"
                return DscResource.GetResourceTypeFromResourceId(this.ResourceId);
            }
        }

        public string ResourceName
        {
            get
            {
                // ResourceID = "[ResourceType]ResourceName"
                return DscResource.GetResourceNameFromResourceId(this.ResourceId);
            }
        }

        public string[] DependsOn
        {
            get
            {
                return (string[])this.Instance.Properties["ResourceID"];
            }
        }

        public string ModuleName
        {
            get
            {
                return this.GetStringProperty(nameof(this.ModuleName));
            }
        }

        public string ModuleVersion
        {
            get
            {
                return this.GetStringProperty(nameof(this.ModuleVersion}));
            }
        }

        #endregion

        #region Methods

        public static DscResource FromInstance(string filename, string computerName, Instance instance)
        {
            switch (instance.ClassName)
            {
                case "MSFT_ScriptResource":
                    return new ScriptResource(filename, computerName, instance);
                default:
                    return new DscResource(filename, computerName, instance);
            }
        }

        private static string GetResourceTypeFromResourceId(string resourceId)
        {
            // ResourceID = "[ResourceType]ResourceName"
            if (string.IsNullOrEmpty(resourceId)) { return null; }
            return resourceId.Split(']')[0].Substring(1);
        }

        private static string GetResourceNameFromResourceId(string resourceId)
        {
            // ResourceID = "[ResourceType]ResourceName"
            if (string.IsNullOrEmpty(resourceId)) { return null; }
            return resourceId.Split(']')[1];
        }

        protected string GetStringProperty(string propertyName)
        {
            if (!this.Instance.Properties.TryGetValue(propertyName, out var propertyValue))
            {
                return null;
            }
            return propertyValue as string;
        }

        #endregion

    }

}
