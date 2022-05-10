namespace SnjMedical.SelfHost.Features.Swagger.Auth;

/// <summary>
/// swagger credentials options
/// </summary>
public class SwaggerCredentialsOptions
{
    /// <summary>
    /// constant section name
    /// </summary>
    public const string SectionName = "SwaggerCredentials";

    /// <summary>
    /// swagger user name
    /// </summary>
    public string UserName { get; }

    /// <summary>
    /// swagger user password
    /// </summary>
    public string Password { get; }

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    public SwaggerCredentialsOptions(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}
