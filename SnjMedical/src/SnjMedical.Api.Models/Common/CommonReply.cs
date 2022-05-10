using Newtonsoft.Json;

namespace SnjMedical.Api.Models.Common;

/// <summary>
/// Base common reply
/// </summary>
public class CommonReply
{
    /// <summary>Array of error messages</summary>
    [JsonProperty("errors", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<ErrorMessage>? Errors { get; set; }

    /// <summary>Array of warning messages</summary>
    [JsonProperty("warnings", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<WarningMessage>? Warning { get; set; }

    /// <summary>
    /// Check error is null or empty
    /// </summary>
    /// <returns></returns>
    public bool IsSuccess => !Errors?.Any() ?? true;
}
