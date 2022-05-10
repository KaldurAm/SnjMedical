using Newtonsoft.Json;

namespace SnjMedical.Domain.Common;

public class ErrorMessage
{
    /// <summary>HTTP status code associated</summary>
    [JsonProperty("status", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; private set; }

    public ErrorMessage()
    {

    }

    public ErrorMessage(string? status, string? requestGuid, string? code, string? title, string? detail)
    {
        Status = status;
    }

    public void SetStatus(string? status)
        => this.Status = status;
}
