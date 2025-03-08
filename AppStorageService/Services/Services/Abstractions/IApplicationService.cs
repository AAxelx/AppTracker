using AppStorageService.Services.DTOs;
using AppStorageService.Services.Enums;

namespace AppStorageService.Services.Services.Abstractions;

public interface IApplicationService
{
  Task<List<ApplicationDto>> GetByStatusAsync(ApplicationStatus status);
  Task AddAsync(AddApplicationDto model);
  Task DeleteAsync(Guid id);
}