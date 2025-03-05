using AppStorageService.Infrastructure.Repositories.Abstractions;
using AppStorageService.Services.DTOs;
using AppStorageService.Services.Handlers.Abstractions;

namespace AppStorageService.Services.Handlers;

public class ApplicationStatusHandler(IApplicationRepository applicationRepository)
  : IApplicationStatusHandler
{
  public async Task HandleAsync(List<UpdateApplicationsStatusDto> dtos)
  {
    await applicationRepository.UpdateApplicationStatusAsync(dtos);
  }
}