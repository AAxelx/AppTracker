using AppStoreTracker.Infrastructure.Repositories.Abstractions;
using AppStoreTracker.Services.Dtos;
using AppStoreTracker.Services.Models;
using AppStoreTracker.Services.Services.Abstractions;
using AutoMapper;

namespace AppStoreTracker.Services.Services;

public class ApplicationTrackingService(IApplicationRepository repository, IMapper mapper) : IApplicationTrackingService
{
  public async Task<bool> AddApplicationAsync(AddApplicationRequestDto app)
  {
    var model = mapper.Map<ApplicationModel>(app);
    model.LastUpdatedAt = DateTime.UtcNow;
    
    await repository.AddAsync(model);
    return true;
  }

  public async Task<bool> RemoveApplicationAsync(string id)
  {
    var applicationId = Guid.Parse(id);
    
    await repository.RemoveAsync(applicationId);
    return true;
  }
}