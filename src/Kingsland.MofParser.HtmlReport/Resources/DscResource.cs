using Kingsland.MofParser.Models;
using System.Collections.ObjectModel;

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

    public string? ResourceId =>
        this.GetStringProperty("ResourceID");

    public string TypeName =>
        this.Instance.TypeName;

    public string? ResourceType =>
        // ResourceID = "[ResourceType]ResourceName"
        this.ResourceId == null
            ? null
            : DscResource.GetResourceTypeFromResourceId(this.ResourceId);

    public string? ResourceName =>
        // ResourceID = "[ResourceType]ResourceName"
        this.ResourceId == null
            ? null
            : DscResource.GetResourceNameFromResourceId(this.ResourceId);

    public ReadOnlyCollection<string> DependsOn =>
        new(
            new List<string>(
                this.Instance.Properties.Single(
                        property => property.Name == "ResourceID"
                    ).Value as string[] ?? Enumerable.Empty<string>()
            )
        );

    public string? ModuleName =>
        this.GetStringProperty(nameof(this.ModuleName));

    public string? ModuleVersion =>
        this.GetStringProperty(nameof(this.ModuleVersion));

    #endregion

    #region Methods

    public static DscResource FromInstance(string filename, string computerName, Instance instance)
    {
        return instance.TypeName switch
        {
            "MSFT_ScriptResource" =>
                new ScriptResource(filename, computerName, instance),
            _ =>
                new DscResource(filename, computerName, instance),
        };
    }

    private static string? GetResourceTypeFromResourceId(string resourceId)
    {
        // ResourceID = "[ResourceType]ResourceName"
        if (string.IsNullOrEmpty(resourceId)) { return null; }
        return resourceId.Split(']')[0][1..];
    }

    private static string? GetResourceNameFromResourceId(string resourceId)
    {
        // ResourceID = "[ResourceType]ResourceName"
        if (string.IsNullOrEmpty(resourceId)) { return null; }
        return resourceId.Split(']')[1];
    }

    protected string? GetStringProperty(string propertyName)
    {
        return this.Instance.Properties.SingleOrDefault(
                property => property.Name == propertyName
            )?.Value as string;
    }

    #endregion

}
