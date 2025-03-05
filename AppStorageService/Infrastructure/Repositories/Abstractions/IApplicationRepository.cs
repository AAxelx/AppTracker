using AppStorageService.Services.DTOs;
using AppStorageService.Services.Enums;
using AppStorageService.Services.Models;

namespace AppStorageService.Infrastructure.Repositories.Abstractions;

public interface IApplicationRepository
{
  Task<ApplicationModel?> GetByIdAsync(Guid id);
  Task<List<ApplicationModel>> GetByStatusAsync(ApplicationStatus status);
  Task AddAsync(ApplicationModel model);
  Task UpdateApplicationStatusAsync(List<UpdateApplicationsStatusDto> dtos);
  Task DeleteAsync(Guid id);
}