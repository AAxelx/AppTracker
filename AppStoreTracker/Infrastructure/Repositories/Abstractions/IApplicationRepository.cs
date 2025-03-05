using AppStoreTracker.Services.Dtos;
using AppStoreTracker.Services.Models;

namespace AppStoreTracker.Infrastructure.Repositories.Abstractions;

public interface IApplicationRepository
{
  Task<List<ApplicationStatusDto>> GetAllApplicationsStatusAsync(CancellationToken stoppingToken);
  Task<bool> AddAsync(ApplicationModel application);
  Task UpdateApplicationsStatusAsync(List<UpdateApplicationsStatusDto> updateDtos, CancellationToken stoppingToken);
  Task<bool> RemoveAsync(Guid id);
}
