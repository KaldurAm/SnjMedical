using Newtonsoft.Json;

namespace SnjMedical.Domain.Common;

public abstract class CommonReply
{
    /// <summary>Array of error messages</summary>
    [JsonProperty("errors", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public List<ErrorMessage>? Errors { get; private set; }

    /// <summary>Array of warning messages</summary>
    [JsonProperty("warnings", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public List<WarningMessage>? Warnings { get; private set; }

    public bool IsSuccess => !Errors?.Any() ?? true;

    public void AddError(ErrorMessage error)
    {
        if (Errors is null)
            Errors = new List<ErrorMessage>();
        Errors.Add(error);
    }

    public void AddErrors(ICollection<ErrorMessage> errors)
    {
        if (Errors is null)
            Errors = new List<ErrorMessage>();
        Errors.AddRange(errors);
    }

    public void AddError(Exception ex)
    {
        var errors = GetStackTrace(ex);
        foreach (var error in errors)
            Errors?.Add(error);
    }

    public void AddWarning(WarningMessage warning)
    {
        if (Warnings is null)
            Warnings = new List<WarningMessage>();
        Warnings.Add(warning);
    }

    public void AddWarnings(ICollection<WarningMessage> warnings)
    {
        if (Warnings is null)
            Warnings = new List<WarningMessage>();
        Warnings.AddRange(warnings);
    }

    private List<ErrorMessage> GetStackTrace(Exception? ex, List<ErrorMessage>? errors = null)
    {
        if (errors is null)
            errors = new List<ErrorMessage>();
        if (ex is null)
            return errors;
        errors.Add(new ErrorMessage(null, null, ex.Source, ex.GetType().Name, ex.Message));
        return GetStackTrace(ex.InnerException, errors);
    }
}
