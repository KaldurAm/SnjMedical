using Newtonsoft.Json;

namespace SnjMedical.Shared.Extensions;

public static class TypeExtension
{
    public static string ToJson<T>(this T source)
    {
        return JsonConvert.SerializeObject(source);
    }

    public static T? ToObject<T>(this string json)
    {
        if (json is null)
            throw new ArgumentNullException(nameof(json));
        return JsonConvert.DeserializeObject<T>(json);
    }
}
