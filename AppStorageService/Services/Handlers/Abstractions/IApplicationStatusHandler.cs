using AppStorageService.Services.DTOs;

namespace AppStorageService.Services.Handlers.Abstractions;

public interface IApplicationStatusHandler
{
  Task HandleAsync(List<UpdateApplicationsStatusDto> updates);
}