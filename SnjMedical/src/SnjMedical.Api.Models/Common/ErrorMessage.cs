using System.CodeDom.Compiler;
using Newtonsoft.Json;

namespace SnjMedical.Api.Models.Common;

/// <summary>
/// Error message
/// </summary>
public class ErrorMessage : WarningMessage
{
    /// <summary>HTTP status code associated</summary>
    [JsonProperty("status", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string? Status { get; set; }
}
