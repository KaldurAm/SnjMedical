using Newtonsoft.Json;

namespace SnjMedical.Api.Models.Common;

/// <summary>
/// Main reply class
/// </summary>
public class Reply : CommonReply
{
    /// <summary>Response data</summary>
    [JsonProperty("data", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public object? Data { get; private set; }

    /// <summary>
    /// init null data
    /// </summary>
    public Reply()
    {
        Data = null;
    }

    /// <summary>
    /// init data
    /// </summary>
    /// <param name="data"></param>
    public Reply(object data)
    {
        Data = data;
    }

    /// <summary>
    /// set data
    /// </summary>
    /// <param name="data"></param>
    public void Set(object data)
        => Data = data;

    /// <summary>
    /// update data
    /// </summary>
    /// <param name="data"></param>
    public void Update(object data)
        => Data = data;

    /// <summary>
    /// clear data
    /// </summary>
    public void Clear()
        => Data = null;
}
