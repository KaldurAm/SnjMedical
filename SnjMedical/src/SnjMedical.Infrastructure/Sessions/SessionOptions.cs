using System.ComponentModel.DataAnnotations;

namespace SnjMedical.Infrastructure.Sessions;

/// <summary>
///     Настройки сессии.
/// </summary>
public sealed class SessionOptions
{
    /// <summary>
    ///     Название секции.
    /// </summary>
    public const string SectionName = "SessionSettings";

    /// <summary>
    ///     Конструктор.
    /// </summary>
    /// <param name="name">Имя куки.</param>
    /// <param name="domain">Домен, для которого устаналиваются куки.</param>
    /// <param name="httpOnly">Доступны ли куки только при передаче через HTTP-запрос.</param>
    /// <param name="expirationInMinutes">Время действия куки.</param>
    /// <param name="isEssential">При значении true указывает, что куки критичны и необходимы для работы этого приложения.</param>
    /// <param name="idleTimeoutInMinutes">Время действия сессии при неактивности пользователя. При каждом новом запросе таймаут сбрасывается. Этот параметр не зависит от expiration.</param>
    public SessionOptions(
        string name,
        string domain,
        bool httpOnly,
        int expirationInMinutes,
        bool isEssential,
        int idleTimeoutInMinutes)
    {
        Name = name;
        Domain = domain;
        HttpOnly = httpOnly;
        ExpirationInMinutes = expirationInMinutes;
        IsEssential = isEssential;
        IdleTimeoutInMinutes = idleTimeoutInMinutes;
    }

    /// <summary>
    ///     Имя куки.
    /// </summary>
    [Required]
    public string Name { get; }

    /// <summary>
    ///     Домен, для которого устаналиваются куки.
    /// </summary>
    [Required]
    public string Domain { get; }

    /// <summary>
    ///     Доступны ли куки только при передаче через HTTP-запрос.
    /// </summary>
    [Required]
    public bool HttpOnly { get; }

    /// <summary>
    ///     Время действия куки.
    /// </summary>
    [Required]
    public int ExpirationInMinutes { get; }

    /// <summary>
    ///     При значении true указывает, что куки критичны и необходимы для работы этого приложения.
    /// </summary>
    [Required]
    public bool IsEssential { get; }

    /// <summary>
    ///     Время действия сессии при неактивности пользователя. При каждом новом запросе таймаут сбрасывается. Этот параметр не зависит от expiration.
    /// </summary>
    [Required]
    public int IdleTimeoutInMinutes { get; }

    /// <summary>
    ///     Получить время действия куки.
    /// </summary>
    /// <remarks>В виде объекта System.TimeSpan.</remarks>
    public TimeSpan GetExpiration()
    {
        return TimeSpan.FromMinutes(ExpirationInMinutes);
    }

    /// <summary>
    ///     Получить время действия сессии при неактивности пользователя.
    /// </summary>
    /// <remarks>В виде объекта System.TimeSpan.</remarks>
    public TimeSpan GetIdleTimeout()
    {
        return TimeSpan.FromMinutes(IdleTimeoutInMinutes);
    }
}
