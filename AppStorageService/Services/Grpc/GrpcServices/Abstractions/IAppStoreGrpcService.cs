using AppStorageService.Services.Models;

namespace AppStorageService.Services.Grpc.GrpcServices.Abstractions;

public interface IAppStoreGrpcService
{
  Task SendApplicationAsync(ApplicationModel model);
}