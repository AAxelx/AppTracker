using AppStoreTracker.Services.Dtos;
using AppStoreTracker.Services.Models;

namespace AppStoreTracker.Services.Services.Abstractions;

public interface IApplicationTrackingService
{
  Task<bool> AddApplicationAsync(AddApplicationRequestDto app);
  Task<bool> RemoveApplicationAsync(string id);
}