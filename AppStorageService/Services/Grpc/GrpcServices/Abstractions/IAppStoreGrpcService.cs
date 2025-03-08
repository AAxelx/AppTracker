using AppStorageService.Services.Models;

namespace AppStorageService.Services.Grpc.GrpcServices.Abstractions;

public interface IAppStoreGrpcService
{
  Task AddTrackingApplicationAsync(ApplicationModel model);
  Task RemoveApplicationTrackingAsync(string id);
}