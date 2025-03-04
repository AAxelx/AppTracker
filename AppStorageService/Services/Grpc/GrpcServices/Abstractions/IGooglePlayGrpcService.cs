using AppStorageService.Services.Models;

namespace AppStorageService.Services.Grpc.GrpcServices.Abstractions;

public interface IGooglePlayGrpcService
{
  Task SendApplicationAsync(ApplicationModel model);
}