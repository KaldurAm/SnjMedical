using Newtonsoft.Json;

namespace SnjMedical.Domain.Common;

public class Reply : CommonReply
{
    /// <summary>Response data</summary>
    [JsonProperty("data", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public object? Data { get; private set; }

    public Reply()
    {
        Data = null;
    }

    public Reply(object data)
    {
        Data = data;
    }

    public void Set(object data)
        => Data = data;

    public void Update(object data)
        => Data = data;

    public void Clear()
        => Data = null;
}
