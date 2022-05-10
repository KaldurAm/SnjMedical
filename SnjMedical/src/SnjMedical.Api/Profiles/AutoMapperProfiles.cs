using AutoMapper;

using SnjMedical.Api.Models.Common;
using SnjMedical.Api.Models.Test.Dtos;

namespace SnjMedical.Api.Profiles;

/// <summary>
/// AutoMapper profiles
/// </summary>
public class AutoMapperProfiles : Profile
{
    /// <summary>
    /// constructor to declare profiles
    /// </summary>
    public AutoMapperProfiles()
    {
        CreateMap<CommonReply, Domain.Common.CommonReply>().ReverseMap();
        CreateMap<ErrorMessage, Domain.Common.ErrorMessage>().ReverseMap();
        CreateMap<WarningMessage, Domain.Common.WarningMessage>().ReverseMap();
        CreateMap<WeatherForecast, Domain.Models.WeatherForecast>().ReverseMap();
        CreateMap<Reply, Domain.Common.Reply>().ReverseMap();
    }
}
