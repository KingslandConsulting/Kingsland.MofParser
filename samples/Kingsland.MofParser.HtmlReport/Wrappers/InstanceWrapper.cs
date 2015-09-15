using Kingsland.MofParser.Objects;
using System;

namespace Kingsland.MofParser.HtmlReport.Wrappers
{

    public class InstanceWrapper
    {

        #region Constructors

        protected InstanceWrapper(string filename, string computerName, Instance instance)
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
                return InstanceWrapper.GetResourceTypeFromResourceId(this.ResourceId);
            }
        }

        public string ResourceName
        {
            get
            {
                // ResourceID = "[ResourceType]ResourceName"
                return InstanceWrapper.GetResourceNameFromResourceId(this.ResourceId);
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
                return this.GetStringProperty("ModuleName");
            }
        }

        public string ModuleVersion
        {
            get
            {
                return this.GetStringProperty("ModuleVersion");
            }
        }

        #endregion

        #region Methods

        public static InstanceWrapper FromInstance(string filename, string computerName, Instance instance)
        {
            switch (instance.ClassName)
            {
                case "HDLE_xAcl":
                case "HDLE_xHostsEntry":
                case "HDLE_xServiceResource":
                case "HDLE_xVolume":
                case "MSFT_ArchiveResource":
                case "MSFT_Credential":
                case "MSFT_DSCMetaConfiguration":
                case "MSFT_EnvironmentResource":
                case "MSFT_GroupResource":
                case "MSFT_FileDirectoryConfiguration":
                case "MSFT_PackageResource":
                case "MSFT_RoleResource":
                case "MSFT_RegistryResource":
                case "MSFT_UserResource":
                case "OMI_ConfigurationDocument":
                    return new InstanceWrapper(filename, computerName, instance);
                case "MSFT_ScriptResource":
                    return new ScriptResourceWrapper(filename, computerName, instance);
                default:
                    throw new NotImplementedException();
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

        protected static string GetStringProperty(Instance instance, string propertyName)
        {
            if (instance.Properties.ContainsKey(propertyName))
            {
                return instance.Properties[propertyName].ToString();
            }
            else
            {
                return null;
            }
        }

        protected string GetStringProperty(string propertyName)
        {
            return InstanceWrapper.GetStringProperty(this.Instance, propertyName);
        }

        #endregion

    }

}
