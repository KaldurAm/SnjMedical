using MediatR;
using Microsoft.Extensions.Logging;
using SnjMedical.Application.Interfaces.Test;
using SnjMedical.Domain.Common;

namespace SnjMedical.Application.Queries.Test;

public class RetrieveWeatherInfoQueryHandler : IRequestHandler<RetrieveWeatherInfoQuery, Reply>
{
    private readonly IWeatherService _service;
    private readonly ILogger<RetrieveWeatherInfoQuery> _logger;

    public RetrieveWeatherInfoQueryHandler(IWeatherService service,
        ILogger<RetrieveWeatherInfoQuery> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Reply> Handle(RetrieveWeatherInfoQuery request,
        CancellationToken cancellationToken)
    {
        Reply reply = new();
        try
        {
            var response = await _service.GetWeatherInfoAsync();
            reply.Set(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("{msg} {error}", "Some problem", ex.Message);
            reply.AddError(ex);
        }

        return reply;
    }
}
