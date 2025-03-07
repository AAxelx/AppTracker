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
    var isExist = await applicationRepository.ExistsByUrlAsync(dto.Url);

    if (isExist)
      throw new ArgumentException("Application with this URL already exists.");
    
    var model = mapper.Map<ApplicationModel>(dto);
    
    model.StoreType = StoreTypeHelper.GetStoreTypeFromUrl(model.Url);
    if (model.StoreType == StoreType.Unknown)
      throw new NotSupportedException($"The store type for the application with URL {model.Url} is not supported.");
    
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