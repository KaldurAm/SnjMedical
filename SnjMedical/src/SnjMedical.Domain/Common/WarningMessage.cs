using Newtonsoft.Json;

namespace SnjMedical.Domain.Common;

public class WarningMessage
{
    /// <summary>The request guid id</summary>
    [JsonProperty("requestGuid", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string? RequestGuid { get; private set; }

    /// <summary>An application-specific error code</summary>
    [JsonProperty("code", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string? Code { get; private set; }

    /// <summary>A short summary of the error</summary>
    [JsonProperty("title", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string? Title { get; private set; }

    /// <summary>Explanation of the error</summary>
    [JsonProperty("detail", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string? Detail { get; private set; }

    public WarningMessage()
    {

    }

    public WarningMessage(string? requestGuid)
    {
        RequestGuid = requestGuid;
    }

    public WarningMessage(string? requestGuid, string? code)
    {
        RequestGuid = requestGuid;
        Code = code;
    }

    public WarningMessage(string? requestGuid, string? code, string? title)
    {
        RequestGuid = requestGuid;
        Code = code;
        Title = title;
    }

    public WarningMessage(string? requestGuid, string? code, string? title, string? detail)
    {
        RequestGuid = requestGuid;
        Code = code;
        Title = title;
        Detail = detail;
    }

    public void SetRequestGuid(string? requestGuid)
        => this.RequestGuid = requestGuid;

    public void SetCode(string? code)
        => this.Code = code;

    public void SetTitle(string? title)
        => this.Title = title;

    public void SetDetail(string? detail)
        => this.Detail = detail;
}
