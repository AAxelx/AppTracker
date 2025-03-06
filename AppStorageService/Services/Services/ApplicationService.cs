using AppStorageService.Infrastructure.Repositories.Abstractions;
using AppStorageService.Services.DTOs;
using AppStorageService.Services.Enums;
using AppStorageService.Services.Grpc.GrpcServices.Abstractions;
using AppStorageService.Services.Helpers;
using AppStorageService.Services.Models;
using AppStorageService.Services.Services.Abstractions;
using AutoMapper;

namespace AppStorageService.Services.Services;

public class ApplicationService(
  IApplicationRepository applicationRepository,
  IMapper mapper,
  IAppStoreGrpcService appStoreGrpcService,
  IGooglePlayGrpcService googlePlayGrpcService,
  ILogger<ApplicationService> logger) : IApplicationService
{
  public async Task<List<ApplicationDto>> GetByStatusAsync(ApplicationStatus status)
  {
    var applications = await applicationRepository.GetByStatusAsync(status);
    
    return mapper.Map<List<ApplicationDto>>(applications);
  }

  public async Task AddAsync(AddApplicationDto dto)
  {
    var model = mapper.Map<ApplicationModel>(dto);//TODO: add check existing Url to skip flow or validate it
    
    model.StoreType = StoreTypeHelper.GetStoreTypeFromUrl(model.Url);
    if (model.StoreType == StoreType.Unknown)
    {
      logger.LogError($"Failed to determine store type for application with URL: {model.Url}");
      throw new InvalidOperationException($"The store type for the application with URL {model.Url} could not be determined.");
    }
    
    model.Id = Guid.NewGuid();

    var currentUtcTime = DateTime.UtcNow;
    model.CreatedAt = currentUtcTime;
    model.LastUpdatedAt = currentUtcTime;
    
    await applicationRepository.AddAsync(model);

    switch (model.StoreType)
    {
      case StoreType.AppStore:
        await appStoreGrpcService.AddTrackingApplicationAsync(model);
        break;
      case StoreType.GooglePlay:
        await googlePlayGrpcService.AddTrackingApplicationAsync(model);
        break;
    }
  }

  public async Task DeleteAsync(Guid id)
  {
    var application = await applicationRepository.GetByIdAsync(id);

    if (application == null)
      return;

    await applicationRepository.DeleteAsync(application.Id);

    switch (application.StoreType)
    {
      case StoreType.AppStore:
        await appStoreGrpcService.RemoveApplicationTrackingAsync(id.ToString());
        break;
      case StoreType.GooglePlay:
        // await googlePlayGrpcService.RemoveApplicationTrackingAsync(id.ToString()); TODO: implement it
        break;
      default:
        throw new InvalidOperationException("Unsupported store type");
    }
  }

}