using System.Threading;
using System.Threading.Tasks;

namespace SnjMedical.Application.Interfaces.Sessions;

/// <summary>
///     The session management service.
/// </summary>
public interface ISessionService
{
    /// <summary>
    ///     Gets the current session id.
    /// </summary>
    string GetCurrentSessionId { get; }

    /// <summary>
    ///     Adds data to the session.
    /// </summary>
    Task SetAsync<T>(string key, T data, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets data from the session by key.
    /// </summary>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
}