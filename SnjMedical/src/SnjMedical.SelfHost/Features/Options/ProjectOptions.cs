namespace SnjMedical.SelfHost.Features.Options;

internal class ProjectOptions
{
    public const string SectionName = "ProjectOptions";

    public string InstanceName { get; }
    public string WebAddress { get; }

    public ProjectOptions(string instanceName, string webAddress)
    {
        InstanceName = instanceName;
        WebAddress = webAddress;
    }
}
