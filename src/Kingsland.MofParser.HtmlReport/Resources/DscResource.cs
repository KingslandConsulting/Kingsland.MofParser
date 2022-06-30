using Kingsland.MofParser.Objects;

namespace Kingsland.MofParser.HtmlReport.Resources;

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
    }

    public string ComputerName
    {
        get;
    }

    public Instance Instance
    {
        get;
    }

    public string ResourceId =>
        this.GetStringProperty("ResourceID");

    public string ClassName =>
        this.Instance.ClassName;

    public string ResourceType =>
        // ResourceID = "[ResourceType]ResourceName"
        DscResource.GetResourceTypeFromResourceId(this.ResourceId);

    public string ResourceName =>
        // ResourceID = "[ResourceType]ResourceName"
        DscResource.GetResourceNameFromResourceId(this.ResourceId);

    public string[] DependsOn =>
        (string[])this.Instance.Properties["ResourceID"];

    public string ModuleName =>
        this.GetStringProperty(nameof(this.ModuleName));

    public string ModuleVersion =>
        this.GetStringProperty(nameof(this.ModuleVersion));

    #endregion

    #region Methods

    public static DscResource FromInstance(string filename, string computerName, Instance instance)
    {
        return instance.ClassName switch
        {
            "MSFT_ScriptResource" =>
                new ScriptResource(filename, computerName, instance),
            _ =>
                new DscResource(filename, computerName, instance),
        };
    }

    private static string GetResourceTypeFromResourceId(string resourceId)
    {
        // ResourceID = "[ResourceType]ResourceName"
        if (string.IsNullOrEmpty(resourceId)) { return null; }
        return resourceId.Split(']')[0][1..];
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
