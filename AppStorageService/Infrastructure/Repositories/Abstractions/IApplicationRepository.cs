using AppStorageService.Services.Enums;
using AppStorageService.Services.Models;

namespace AppStorageService.Infrastructure.Repositories.Abstractions;

public interface IApplicationRepository
{
  Task<List<ApplicationModel>> GetByStatusAsync(ApplicationStatus status);
  Task AddAsync(ApplicationModel model);
  Task DeleteAsync(Guid id);
}