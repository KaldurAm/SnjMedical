namespace SnjMedical.SelfHost.Features.Options;

internal class ProjectOptions
{
    internal const string SectionName = "ProjectOptions";

    internal string InstanceName { get; }
    internal string WebAddress { get; }

    public ProjectOptions(string instanceName, string webAddress)
    {
        InstanceName = instanceName;
        WebAddress = webAddress;
    }
}
