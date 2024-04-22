using AutoMapper;
using UrlShortener.DataAccess.Entities;
using UrlShortener.DTOs.Request;
using UrlShortener.DTOs.Response;

namespace UrlShortener.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Url, UrlListResponse>();
        CreateMap<Url, UrlResponse>()
            .ForMember(dest => dest.OwnerName, 
                opt => opt.MapFrom(src => src.User.Username));
        CreateMap<UrlRequest, Url>();
    }
}