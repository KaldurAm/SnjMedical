using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SnjMedical.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected ISender Mediator =>
        HttpContext.RequestServices.GetService<ISender>() ??
        throw new ArgumentNullException(nameof(ISender));

    protected IMapper Mapper =>
        HttpContext.RequestServices.GetService<IMapper>() ??
        throw new ArgumentNullException(nameof(IMapper));
}
