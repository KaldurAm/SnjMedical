using System.Collections.Immutable;

namespace SnjMedical.Domain.Exceptions;

public class BadRequestException : Exception
{
    public string? Detail { get; }
    public IReadOnlyDictionary<string, object>? AdditionalProblems { get; }

    public BadRequestException()
    {
    }

    public BadRequestException(string? message, string? detail = null,
        IDictionary<string, object>? additionalProblems = null) : base(message)
    {
        Detail = detail;
        AdditionalProblems = additionalProblems?.ToImmutableDictionary();
    }

    public BadRequestException(string? message, string? detail = null,
        IDictionary<string, object>? additionalProblems = null, Exception? innerException = null) : base(message,
        innerException)
    {
        Detail = detail;
        AdditionalProblems = additionalProblems?.ToImmutableDictionary();
    }
}
