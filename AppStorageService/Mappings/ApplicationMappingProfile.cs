using AppStorageService.Infrastructure.Entities;
using AppStorageService.Services.DTOs;
using AppStorageService.Services.Enums;
using AppStorageService.Services.Models;
using AutoMapper;

namespace AppStorageService.Mappings;

public class ApplicationMappingProfile : Profile
{
  public ApplicationMappingProfile()
  {
    CreateMap<ApplicationEntity, ApplicationModel>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (ApplicationStatus)src.Status))
      .ForMember(dest => dest.StoreType, opt => opt.MapFrom(src => (StoreType)src.StoreType))
      .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
      .ForMember(dest => dest.LastUpdatedAt, opt => opt.MapFrom(src => src.LastUpdatedAt));

    // Model -> Entity
    CreateMap<ApplicationModel, ApplicationEntity>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
      .ForMember(dest => dest.StoreType, opt => opt.MapFrom(src => (int)src.StoreType))
      .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
      .ForMember(dest => dest.LastUpdatedAt, opt => opt.MapFrom(src => src.LastUpdatedAt));

    CreateMap<ApplicationModel, ApplicationDto>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
      .ForMember(dest => dest.StoreType, opt => opt.MapFrom(src => src.StoreType))
      .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
      .ForMember(dest => dest.LastUpdatedAt, opt => opt.MapFrom(src => src.LastUpdatedAt));

    CreateMap<AddApplicationDto, ApplicationModel>()
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));

    CreateMap<ApplicationModel, AppStoreGrpc.ApplicationRequest>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));
    
    CreateMap<ApplicationModel, GooglePlayGrpc.ApplicationRequest>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));
  }
}
