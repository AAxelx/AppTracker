using AppStorageService.Services.Models;

namespace AppStorageService.Infrastructure.Repositories.Abstractions;

public interface IApplicationRepository
{
  Task<List<ApplicationModel>> GetAllAsync();
  Task AddAsync(ApplicationModel model);
  Task DeleteAsync(Guid id);
}