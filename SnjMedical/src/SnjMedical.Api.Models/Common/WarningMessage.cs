using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SnjMedical.Api.Models.Common;

/// <summary>
/// Warning message
/// </summary>
public class WarningMessage
{
    /// <summary>An application-specific error code</summary>
    [JsonProperty("code", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string? Code { get; set; }

    /// <summary>A short summary of the error</summary>
    [JsonProperty("title", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string? Title { get; set; }

    /// <summary>Explanation of the error</summary>
    [JsonProperty("detail", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string? Detail { get; set; }
}
