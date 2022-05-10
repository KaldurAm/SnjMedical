using System.Text.Json;
using SnjMedical.Application.Interfaces.Sessions;
using Microsoft.AspNetCore.Http;

namespace SnjMedical.Infrastructure.Sessions;

public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string GetCurrentSessionId => _httpContextAccessor.HttpContext.Session.Id;

    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            var value = Session.GetString(BuildKey(key));
            return value is null ? default : JsonSerializer.Deserialize<T?>(value);
        }, cancellationToken);
    }

    public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
        return Task.Run(() => Session.SetString(BuildKey(key), JsonSerializer.Serialize(value)), cancellationToken);
    }

    private ISession Session => _httpContextAccessor.HttpContext.Session;

    private string BuildKey(string key)
    {
        return $"{GetCurrentSessionId}:{key}";
    }
}