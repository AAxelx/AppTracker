using AppStoreGrpc;
using AppStoreTracker.Infrastructure.Entities;
using AppStoreTracker.Services.Dtos;
using AppStoreTracker.Services.Models;
using AutoMapper;

namespace AppStoreTracker.Mappings;

public class ApplicationMappingProfile : Profile
{
  public ApplicationMappingProfile()
  {
    CreateMap<AddApplicationRequest, AddApplicationRequestDto>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
    
    CreateMap<AddApplicationRequestDto, ApplicationModel>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
    
    CreateMap<ApplicationEntity, ApplicationStatusDto>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
  }
}